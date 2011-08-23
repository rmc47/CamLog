using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Engine;
using System.Threading;

namespace EngineTests
{
    [TestFixture]
    class CivTests
    {
        [Test, Explicit]
        public void ConstructServer()
        {
            using (CivServer cs = new CivServer("COM3", true, true))
            {
                cs.FrequencyChanged += new EventHandler<EventArgs>(cs_FrequencyChanged);
                cs.ModeChanged += new EventHandler<EventArgs>(cs_ModeChanged);
                Thread.Sleep(10000);
            }
        }

        void cs_ModeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("New mode: " + ((CivServer)sender).Mode);
        }

        void cs_FrequencyChanged(object sender, EventArgs e)
        {
            Console.WriteLine("New frequency: " + ((CivServer)sender).Frequency);
        }
    }
}
