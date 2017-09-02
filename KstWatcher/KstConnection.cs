using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CamLog.KstWatcher
{
    class KstConnection
    {
        public event EventHandler<ChatMessage> ChatMessageReceived;

        private TcpClient m_KstClient;
        
        public KstConnection()
        {

        }

        public void Connect()
        {
            m_KstClient = new TcpClient();
            m_KstClient.Connect("on4kst.info", 23001);
            new Thread(StreamConsumer).Start();
        }

        private void StreamConsumer(object state)
        {
            using (NetworkStream kstStream = m_KstClient.GetStream())
            {
                using (StreamReader sr = new StreamReader(kstStream))
                {
                    using (StreamWriter sw = new StreamWriter(kstStream, Encoding.ASCII, 1024, true))
                    {
                        // Read the login line
                        sr.ReadLine();
                        sw.WriteLine("LOGINC|G3PYE/P|<PASSWORD>|2|KST2Me 1.0.6.0|20|20|1|0|0|");
                        sw.Flush();
                        sr.ReadLine(); // Read the LOGSTAT line

                        // Say we're done with init
                        sw.WriteLine("SDONE|2|");
                        sw.Flush();
                    }
                    while (true)
                    {
                        string line = sr.ReadLine();
                        ProcessLine(line);
                    }
                }
            }
        }

        private void ProcessLine(string line)
        {
            if (line == null)
                return;
            string[] lineParts = line.Split('|');
            switch(lineParts[0].ToUpperInvariant())
            {
                case "CH":
                    ProcessChatLine(lineParts);
                    break;
            }
        }

        private void ProcessChatLine(string[] lineParts)
        {
            if (lineParts.Length < 8)
            {
                Debug.Assert(false, "Expected 8 parts to CH line; got " + lineParts.Length);
                return;
            }

            DateTime timestamp = new DateTime(1970, 1, 1).AddSeconds(long.Parse(lineParts[2]));
            string senderCall = lineParts[3];
            string senderName = lineParts[4];
            string message = lineParts[6];
            string recipient = lineParts[7];
            if (recipient == "0")
                recipient = null;

            ChatMessage chatMessage = new ChatMessage { SenderCall = senderCall, RecipientCall = recipient, Timestamp = timestamp, Message = message};
            ChatMessageReceived?.Invoke(this, chatMessage);
        }
    }
}
