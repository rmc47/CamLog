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

        /// <summary>
        /// Tests the bureau sort order is correct. Looking for:
        /// - DL/2E1EVJ is sorted with G at the start, not DL
        /// - 7J is sorted with JA
        /// - Callsigns with same DXCC are sorted alphabetically (e.g. G1ABC before G3DEF)
        /// </summary>
        [Test]
        public void BureauSort()
        {
            List<string> calls = new List<string> { "M0VFC",  "JA1ABC", "DL1ABC", "7J1ABC", "P5ABC", "ZZ1A", "G1ABC", "G3DEF", "DL/2E1EVJ", "GW1ABC" };
            List<string> expectedOrder = new List<string> { "DL/2E1EVJ", "G1ABC", "G3DEF", "M0VFC", "GW1ABC", "DL1ABC", "7J1ABC", "JA1ABC", "P5ABC", "ZZ1A" };
            var sorter = new BureauSorter(calls);
            List<List<Contact>> contacts = calls.Select(c => new List<Contact> { new Contact { Callsign = c } }).ToList();
            contacts.Sort(sorter.Sort);
            var sortedCalls = contacts.Select(c => c[0].Callsign).ToList();
            Assert.AreEqual(string.Join(",", expectedOrder), string.Join(",", sortedCalls));
        }
    }
}
