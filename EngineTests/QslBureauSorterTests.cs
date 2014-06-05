using Engine;
using NUnit.Framework;
using QslEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineTests
{
    [TestFixture]
    public class QslBureauSorterTests
    {

        [Test]
        public void BureauSort()
        {
            List<string> calls = new List<string> { "M0VFC", "DL1ABC", "P5ABC", "ZZ1A", "G1ABC", "G3DEF", "DL/2E1EVJ", "GW1ABC" };
            List<string> expectedOrder = new List<string> { "DL/2E1EVJ", "G1ABC", "G3DEF", "M0VFC", "GW1ABC", "ZZ1A", "DL1ABC", "P5ABC" };
            var sorter = new BureauSorter(calls);
            List<List<Contact>> contacts = calls.Select(c => new List<Contact> { new Contact { Callsign = c } }).ToList();
            contacts.Sort(sorter.Sort);
            var sortedCalls = contacts.Select(c => c[0].Callsign).ToList();
            Assert.AreEqual(string.Join(",", expectedOrder), string.Join(",", sortedCalls));
        }
    }
}
