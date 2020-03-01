using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BellDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.Sockets.UdpClient c = new System.Net.Sockets.UdpClient("192.168.1.173", 8888);
            int lastSerial = 0;
            using (ContactStore cs = new ContactStore("flossie01", "2018_09_144MHz_Trophy", "g3pye", "g3pye"))
            {
                while (true)
                {
                    int nextSerial = cs.GetSerial(Band.B2m);
                    Console.WriteLine("{0}: last serial {1}", DateTime.UtcNow, nextSerial);
                    if (nextSerial > lastSerial)
                    {
                        c.Send(Encoding.ASCII.GetBytes("bell"), 4);
                        lastSerial = nextSerial;
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
