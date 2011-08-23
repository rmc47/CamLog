using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Engine;

namespace EngineTests
{
    [TestFixture]
    public sealed class FrequencyTests
    {
        [Test]
        [TestCase ("145.550.000", 145550000)]
        [TestCase ("3.690", 3690000)]
        [TestCase ("144.7625", 144762500)]
        [TestCase ("144.762.500", 144762500)]
        [TestCase ("144.5", 144500000)]
        [TestCase ("14", 14000000)]
        public void TestFrequencyParse(string str, long freq)
        {
            Assert.AreEqual(freq, FrequencyHelper.Parse(str));
        }
    }
}
