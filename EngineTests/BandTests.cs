using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Engine;

namespace EngineTests
{
    [TestFixture]
    public class BandTests
    {
        [Test]
        public void BandParsing()
        {
            int bandsParsed = 0;
            foreach (Band b in Enum.GetValues(typeof(Band)))
            {
                bandsParsed++;
                string bandName = BandHelper.ToString(b);
                Assert.IsNotNullOrEmpty(bandName);
                Band parsedBand = BandHelper.Parse(bandName);
                Assert.AreEqual(b, parsedBand);
                Console.WriteLine("Band " + b + ": " + bandName);
            }
            Assert.Less(0, bandsParsed);
        }
    }
}
