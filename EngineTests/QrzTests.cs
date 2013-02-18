using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Engine;
using System.IO;

namespace EngineTests
{
    [TestFixture]
    public sealed class QrzTests
    {
        [Test]
        public void Login()
        {
            QrzServer server = new QrzServer("M0VFC", "g3pyeflossie");
            server.Login();
        }

        [Test]
        public void FailedLogin()
        {
            try
            {
                QrzServer server = new QrzServer("M0VFC", "random");
                server.Login();
                Assert.Fail("Expected login to fail with random password");
            }
            catch (InvalidDataException ex)
            {
                Assert.That(ex.Message.Contains("Login"), "Expected message to indicate login failure");
            }
        }

        [Test]
        public void LookupBxf()
        {
            QrzServer server = new QrzServer("M0VFC", "g3pyeflossie");
            QrzEntry entry = server.LookupCallsign("M1bxf");
            Console.WriteLine("{0}, {1}, {2}", entry.Callsign, entry.Name, entry.Locator);
            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.Callsign);
            Assert.IsNotNull(entry.Name);
            Assert.IsNotNull(entry.Locator);
        }

        [Test]
        public void LookupTar()
        {
            QrzServer server = new QrzServer("M0VFC", "g3pyeflossie");
            QrzEntry entry = server.LookupCallsign("g0tar");
            Console.WriteLine("{0}, {1}, {2}", entry.Callsign, entry.Name, entry.Locator);
            Assert.IsNotNull(entry);
            Assert.IsNotNull(entry.Callsign);
            Assert.IsNotNull(entry.Name);
            Assert.IsNull(entry.Locator);
        }
    }
}
