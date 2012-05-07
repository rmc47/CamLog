using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Engine;

namespace EngineTests
{
    [TestFixture]
    public class AdifTests
    {
        [Test]
        public void TryImportAdif()
        {
            List<Contact> contacts = AdifHandler.ImportAdif(@"C:\temp\test.adi");
            ContactStore store = new ContactStore("localhost", "fp2011", "root", "aopen");
            contacts.ForEach(store.SaveContact);
        }

        [Test]
        public void FixAdifDates()
        {
            List<Contact> adifContacts = AdifHandler.ImportAdif(@"C:\temp\test.adi");
            ContactStore store = new ContactStore("localhost", "fp2011", "root", "aopen");

            foreach (Contact adifContact in adifContacts)
            {
                // Find the matching one in the DB
                List<Contact> existingContacts = store.GetPreviousContacts(adifContact.Callsign);
                List<Contact> probableMatches = existingContacts.FindAll(c => c.EndTime.TimeOfDay == adifContact.EndTime.TimeOfDay && c.Band == adifContact.Band);
                if (probableMatches.Count == 1)
                {
                    probableMatches[0].StartTime = probableMatches[0].EndTime = adifContact.EndTime;
                    //store.SaveContact(probableMatches[0]);
                }
                else
                {
                    Assert.Fail("Failed to find single matching QSO: " + adifContact);
                }
            }
        }
    }
}
