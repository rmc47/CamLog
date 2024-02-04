using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class QARSender
    {
        private UdpClient socket;
        public QARSender()
        {
            socket = new UdpClient("192.168.77.255", 9458);
        }

        public void SendQso(Contact c)
        {
            try
            {
                var xml = $@"<?xml version=""1.0""?><contactinfo>
     <logger>CamLog</logger>
     <contestname></contestname>
     <timestamp>{c.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}</timestamp>
     <mycall>G3PYE</mycall>
     <band>{c.Frequency / 1000000}</band>
     <txfreq>{c.Frequency}</txfreq>
     <operator></operator>
     <mode>USB</mode>
     <call>{c.Callsign}</call>
     <countryprefix></countryprefix>
     <wpxprefix></wpxprefix>
     <snt>{c.ReportSent}</snt>
     <rcv>{c.ReportReceived}</rcv>
     <nr>{c.SerialSent}</nr>
     <exch1>{c.SerialReceived}</exch1>
     <exch2>{c.LocatorReceived}</exch2>
     <exch3></exch3>
     <duplicate>False</duplicate>
     <stationname></stationname>
     <points>{c.Points}</points>
     </contactinfo>";
                var bytes = Encoding.ASCII.GetBytes(xml);
                socket.Send(bytes, bytes.Length);
            }
            catch
            { }
        }
    }
}
