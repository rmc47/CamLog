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
            List<Contact> contacts = AdifHandler.ImportAdif(@"\\server.dx\share\Lunga2012\lunga2012.adi", "LUNGA", 2, "GS6PYE/P");
            ContactStore store = new ContactStore("server.dx", "mull2012", "g3pye", "flossie");
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

        public void CountCountries()
        {
            ContactStore store = new ContactStore("server.dx", "mull2012", "g3pye", "flossie");
            List<Contact> contacts = store.GetAllContacts(null);
            CallsignLookup cl = new CallsignLookup("cty.xml");
            Dictionary<string, object> countries = new Dictionary<string,object>();
            foreach (Contact c in contacts)
            {
                try
                {
                    PrefixRecord pr = cl.LookupPrefix(c.Callsign);
                    countries[pr.Entity] = new object();
                }
                catch
                {
                    ;
                }
            }
            List<string> countryList = new List<string>();
            countryList.AddRange(countries.Keys);
            countryList.Sort();
            foreach (string country in countryList)
                Console.WriteLine(country);
        }

    }
}
