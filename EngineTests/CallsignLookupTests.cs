using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Engine;

namespace EngineTests
{
    [TestFixture]
    public class CallsignLookupTests
    {
        [Test]
        public void LoadCtyXml()
        {
            new CallsignLookup("cty.xml.gz");
        }

        [Test]
        [TestCase("M0VFC", "ENGLAND")]
        [TestCase("G3VFC", "ENGLAND")]
        [TestCase("F1ABC", "FRANCE")]
        [TestCase("2W1EVJ", "WALES")]
        [TestCase("MM/F1ABC", "SCOTLAND")]
        [TestCase("M0VFC/DE", "GERMANY")]
        [TestCase("M0VFC/MM", "ENGLAND")]
        [TestCase("HB9/M4A", "SWITZERLAND")]
        [TestCase("5J/M4A", "COLOMBIA")]
        [TestCase("ms0sdc/p", "SCOTLAND")]
        [TestCase("g3vfc/qrp", "ENGLAND")]
        public void TestCallsigns(string callsign, string entity)
        {
            CallsignLookup lookup = new CallsignLookup ("cty.xml");
            PrefixRecord record = lookup.LookupPrefix(callsign);
            Assert.AreEqual(entity, record.Entity);
        }
    }
}
