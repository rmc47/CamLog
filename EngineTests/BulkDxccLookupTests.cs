using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineTests
{
    [TestFixture]
    public class BulkDxccLookupTests
    {
        [Test]
        public void BasicLookup()
        {
            var lookup = new BulkDxccLookup();
            var results = lookup.Lookup(new List<string> { "M0VFC" });
            Assert.That(results["M0VFC"] == 223, "Expected M0VFC to map to England");
        }
    }
}
