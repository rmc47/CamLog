using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Engine;

namespace EngineTests
{
    [TestFixture]
    public class KstTests
    {
        [Test]
        public void BasicConnect()
        {
            KstServer kst = new KstServer();
            Console.WriteLine("FFS");
        }
    }
}
