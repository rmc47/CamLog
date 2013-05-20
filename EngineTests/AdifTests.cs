using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Engine;
using System.Diagnostics;
using System.IO;

namespace EngineTests
{
    [TestFixture]
    public class AdifTests
    {
        [Test]
        public void TryImportAdif()
        {
            List<Contact> contacts = AdifHandler.ImportAdif(File.ReadAllText(@"c:\users\rob\Documents\win-test\DXPED-HF-ALL_2012@ZD9UW\ZD9-2012-10-04-1230.ADI"), "LUNGA", 2, "GS6PYE/P");
            ContactStore store = new ContactStore("localhost", "zd92012", "root", "g3pyeflossie");
            contacts.ForEach(store.SaveContact);
        }

        [Test]
        public void FixAdifDates()
        {
            List<Contact> adifContacts = AdifHandler.ImportAdif(File.ReadAllText(@"C:\temp\test.adi"), string.Empty, 0, "M0VFC");
            ContactStore store = new ContactStore("localhost", "fp2011", "root", "aopen");

            foreach (Contact adifContact in adifContacts)
            {
                // Find the matching one in the DB
                List<Contact> existingContacts = store.GetPreviousContacts(adifContact.Callsign);
                List<Contact> probableMatches = existingContacts.FindAll(c => c.StartTime.TimeOfDay == adifContact.StartTime.TimeOfDay && c.Band == adifContact.Band);
                if (probableMatches.Count == 1)
                {
                    probableMatches[0].StartTime = probableMatches[0].EndTime = adifContact.StartTime;
                    store.SaveContact(probableMatches[0]);
                }
                else
                {
                    Assert.Fail("Failed to find single matching QSO: " + adifContact);
                }
            }
        }

        [Test]
        public void CountCountries()
        {
            ContactStore store = new ContactStore("localhost", "zd92012", "root", "g3pyeflossie");
            List<Contact> contacts = store.GetAllContacts(null);
            CallsignLookup cl = new CallsignLookup("cty.xml.gz");
            Dictionary<string, int> countries = new Dictionary<string,int>();
            foreach (Contact c in contacts)
            {
                try
                {
                    PrefixRecord pr = cl.LookupPrefix(c.Callsign);
                    int count;
                    if (countries.TryGetValue(pr.Entity, out count))
                    {
                        countries[pr.Entity] = count + 1;
                    }
                    else
                    {
                        countries[pr.Entity] = 1;
                    }
                }
                catch
                {
                    Debug.WriteLine("Unable to get entity: " + c.Callsign);
                }
            }
            List<KeyValuePair<string, int>> countryList = new List<KeyValuePair<string,int>>();
            foreach (var kvp in countries)
                countryList.Add(kvp);
            countryList.Sort((k1, k2) => {
                if (k1.Value == k2.Value)
                    return string.CompareOrdinal(k1.Key, k2.Key);
                else
                    return k2.Value - k1.Value;
            });
            foreach (var kvp in countryList)
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
        }

    }
}
