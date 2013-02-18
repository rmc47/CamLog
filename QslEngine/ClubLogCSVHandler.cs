using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Engine;

namespace QslEngine
{
    public sealed class ClubLogCSVHandler
    {
        private readonly ContactStore m_ContactStore;
        private readonly PdfEngine m_PdfEngine;

        public ClubLogCSVHandler(ContactStore contactStore, PdfEngine engine)
        {
            m_ContactStore = contactStore;
            m_PdfEngine = engine;
        }

        public int ProcessFile(string filename)
        {
            int qsosToPrint = 0;

            List<Address> addresses = ParseFile(filename);

            // First pass to check all the QSOs are actually in the log!
            Dictionary<Address, List<Contact>> contacts = new Dictionary<Address, List<Contact>>();
            foreach (Address a in addresses)
            {
                List<Contact> cs = m_ContactStore.GetPreviousContacts(a.Callsign);
                if (cs.Count < 1)
                    throw new InvalidDataException("File contains callsign not in log: " + a.Callsign);
                contacts[a] = cs;
            }

            DateTime processTime = DateTime.UtcNow;

            foreach (Address a in addresses)
            {
                List<Contact> cs = contacts[a];
                foreach (Contact c in cs)
                {
                    if (c.QslTxDate == null)
                    {
                        c.QslRxDate = processTime;
                        c.QslMethod = "Direct";
                        m_ContactStore.SaveContact(c);
                        // Record the first contact that we need to print - this way we can sort on it later, and thus guarantee the same label print order as the contact labels will be later
                        if (a.FirstContactID == 0)
                            a.FirstContactID = c.Id;
                    }
                }
            }

            addresses.Sort((a1, a2) => a1.FirstContactID.CompareTo(a2.FirstContactID));

            foreach (Address a in addresses)
            {
                if (a.FirstContactID > 0)
                {
                    qsosToPrint++;
                    m_PdfEngine.AddAddressLabel(a);
                }
            }

            return qsosToPrint;
        }

        private static List<Address> ParseFile(string filename)
        {
            string[] entries = File.ReadAllLines(filename);
            List<Address> addresses = new List<Address>();
            foreach (string entry in entries)
            {
                string[] parts = entry.Split(',');
                if (parts.Length <= 0)
                    continue;
                else if (parts.Length < 3)
                    throw new InvalidDataException("Line does not contain address data: " + entry);

                parts = Array.ConvertAll(parts, part => part.Trim(' ', '\'', '"'));

                Address address = new Address
                {
                    Callsign = parts[0],
                    Name = parts[1],
                    AddressLines = parts.Skip(2).ToArray()
                };

                addresses.Add(address);
            }
            return addresses;
        }
    }
}
