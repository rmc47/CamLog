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
                    throw new InvalidDataException("QSO not found in log: " + importedContact);

                Contact matchedContact = allContacts[matchedContactIndex];

                // Have we already QSLed them?
                if (matchedContact.QslTxDate != null && matchedContact.QslMethod == "Direct")
                    continue;

                // Check if we've already got an address label lined up for that callsign
                if (!addressesToPrint.ContainsKey(matchedContact.Callsign))
                {
                    string addressText = currentRecord["ADDRESS"];
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


                // Mark the QSO as QSLed
                matchedContact.QslRxDate = processTime; // Guarantee everything in this batch has the same time
                matchedContact.QslMethod = "Direct";
                m_ContactStore.SaveContact(matchedContact);
            }

            // Sort the address labels according to first contact IDs for each callsign
            List<Address> addresses = addressesToPrint.Values.ToList();
            addresses.Sort((a1, a2) => (a1.FirstContactID.CompareTo(a2.FirstContactID)));

            foreach (Address a in addresses)
                m_PdfEngine.AddAddressLabel(a);

            return addresses.Count;
        }
    }
}
