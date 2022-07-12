using DymoSDK.Implementations;
using Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public sealed class ClubLogQslAdifHandler
    {
        private readonly ContactStore m_ContactStore;
        private readonly PdfEngine m_PdfEngine;

        public ClubLogQslAdifHandler(ContactStore contactStore, PdfEngine engine)
        {
            m_ContactStore = contactStore;
            m_PdfEngine = engine;
        }

        public int ProcessFile(string filename)
        {
            AdifFileReader adifReader = AdifFileReader.LoadFromContent(File.ReadAllText(filename));
            var header = adifReader.ReadHeader();
            AdifFileReader.Record currentRecord;

            List<Contact> allContacts = m_ContactStore.GetAllContacts(null);
            allContacts.Sort(Contact.QsoMatchCompare);

            IComparer<Contact> matchComparer = new Contact.QsoMatchComparer();

            DateTime processTime = DateTime.UtcNow;
            Dictionary<string, Address> addressesToPrint = new Dictionary<string, Address>();

            while ((currentRecord = adifReader.ReadRecord()) != null)
            {
                Contact importedContact = AdifHandler.GetContact(currentRecord);

                int matchedContactIndex = allContacts.BinarySearch(importedContact, matchComparer);
                if (matchedContactIndex < 0)
                {
                    //continue;
                    matchedContactIndex = allContacts.FindIndex(c => matchComparer.Compare(importedContact, c) == 0);
                    if (matchedContactIndex < 0)
                        continue;
                    //throw new InvalidDataException("QSO not found in log: " + importedContact);
                }

                Contact matchedContact = allContacts[matchedContactIndex];

                QslMethod requestedMethod;
                switch (currentRecord["QSL_SENT_VIA"])
                {
                    case "D":
                        requestedMethod = QslMethod.Direct;
                        break;
                    case "B":
                        requestedMethod = QslMethod.Bureau;
                        break;
                    default:
                        requestedMethod = QslMethod.Unknown;
                        break;
                }

                // Have we already QSLed them?
                if (matchedContact.QslTxDate != null)
                {
                    if (matchedContact.QslMethod == "Direct") // If we've sent direct, skip (no point doing direct + bureau)
                        continue;
                    if (requestedMethod == QslMethod.Bureau) // If this request is for bureau and we've sent via bureau, skip
                        continue;
                    matchedContact.QslTxDate = null; // Reset the QSL TX date, because this is an "upgrade" request
                }

                //// Do we have a location?
                //if (matchedContact.LocationID <= 0)
                //    continue;

                if (requestedMethod == QslMethod.Direct) // Only if we need to do the address labels
                {
                    // Is it a dead checkout? (Shouldn't happen for direct!)
                    if (currentRecord["ADDRESS"] == null)
                        continue;

                    // Check if we've already got an address label lined up for that callsign
                    if (!addressesToPrint.ContainsKey(matchedContact.Callsign))
                    {
                        string addressText = currentRecord["ADDRESS"];

                        // Work around UTF8 character vs byte count
                        if (addressText.IndexOf("<") > 0)
                        {
                            string probablyExtraneousText = addressText.Substring(addressText.IndexOf("<"));
                            if ("<NOTES".StartsWith(probablyExtraneousText.Substring(0, Math.Min(probablyExtraneousText.Length, "<NOTES".Length))))
                            {
                                addressText = addressText.Substring(0, addressText.IndexOf("<"));
                            }
                            else
                            {
                                // We've got a < but it's not the notes field. Where did it come from? Make it XML-safe just in case
                                addressText = addressText.Replace("<", "(").Replace(">", ")");
                            }
                        }
                        
                        string[] addressLines = addressText.Split(',');

                        Address address = new Address
                        {
                            Callsign = matchedContact.Callsign,
                            Name = addressLines[0],
                            AddressLines = addressLines.Skip(1).ToArray(),
                            FirstContactID = matchedContact.Id
                        };

                        addressesToPrint[matchedContact.Callsign] = address;
                    }
                    else
                    {
                        // If necessary, decrement the minimum contact ID for this address label
                        addressesToPrint[matchedContact.Callsign].FirstContactID = Math.Min(matchedContact.Id, addressesToPrint[matchedContact.Callsign].FirstContactID);
                    }
                }

                // Mark the QSO as QSLed
                matchedContact.QslRxDate = processTime; // Guarantee everything in this batch has the same time
                switch (requestedMethod)
                {
                    case QslMethod.Direct:
                        matchedContact.QslMethod = "Direct";
                        break;
                    case QslMethod.Bureau:
                        matchedContact.QslMethod = "Bureau";
                        break;
                }
                m_ContactStore.SaveContact(matchedContact);
            }

            // Sort the address labels according to first contact IDs for each callsign
            List<Address> addresses = addressesToPrint.Values.ToList();
            addresses.Sort((a1, a2) => (a1.FirstContactID.CompareTo(a2.FirstContactID)));

            if (addresses.Count > 0)
            {
                DymoEngine dymo = new DymoEngine();
                foreach (Address a in addresses)
                {
                    dymo.PrintAddress(a);
                }
            }

            return addresses.Count;
        }
    }
}
