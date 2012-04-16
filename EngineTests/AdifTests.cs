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
    }
}
