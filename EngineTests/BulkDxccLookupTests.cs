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

        [Test]
        public void GermanSWLLookup()
        {
            var lookup = new BulkDxccLookup();
            var results = lookup.Lookup(new List<string> { "DE1ABC" });
            Assert.That(results["DE1ABC"] == 1000, "Expected DE1ABC to map to INVALID (SWL)");
        }

        [Test]
        public void DXCCByOperator()
        {
            var cs = new ContactStore("server.dx", "2017_08_yota", "g3pye", "g3pyeflossie");
            var contacts = cs.GetAllContacts(null);
            var contactsByOps = contacts.GroupBy(c => c.Operator);
            Dictionary<string, int> opDxccCounts = new Dictionary<string, int>();
            foreach (var contactsByOp in contactsByOps)
            {
                var op = contactsByOp.Key;
                var uniqueCallsigns = contactsByOp.Select(c => c.Callsign).Distinct();
                var dxccs = new BulkDxccLookup().Lookup(uniqueCallsigns);
                var dxccCount = dxccs.Values.Distinct().Count();
                opDxccCounts[op] = dxccCount;
            }
            foreach (var countEntry in opDxccCounts.OrderByDescending(o => o.Value))
            {
                Console.WriteLine("{0}\t\t{1}", countEntry.Key, countEntry.Value);
            }
        }
    }
}
