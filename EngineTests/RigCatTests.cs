using NUnit.Framework;
using RigCAT.NET.Icom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineTests
{
    [TestFixture]
    public class RigCatTests
    {
        [Test]
        public void TestBcdFrequency()
        {
            byte[] bcdBytes = GenericIcom.FrequencyToBcd(144300000);
            // 01 44 30 00 00
            long freq = GenericIcom.ParseBcd(bcdBytes, 0, bcdBytes.Length);
            }
    }
}
