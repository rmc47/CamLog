using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace Engine
{
    public sealed class KstServer
    {
        private event EventHandler<KstMessageEventArgs> MessageReceived;

        public KstServer()
        {
            TcpClient client = new TcpClient();
            client.Connect("www.on4kst.info", 23000);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            string read;
            while (!(read = reader.ReadLine()).StartsWith("Login"))
                Console.WriteLine(read);
            writer.WriteLine("M0VFC");
            read = reader.ReadLine();
            Console.WriteLine(read);
            writer.WriteLine("B87CCC3334D2");
            while (!(read = reader.ReadLine()).StartsWith("Your choice"))
                Console.WriteLine(read);
            writer.WriteLine("3");
            while (!(read = reader.ReadLine()).EndsWith("chat>"))
                Console.WriteLine(read);

            Thread t = new Thread(new ThreadStart(() => Reader(reader)));
            t.IsBackground = true;
            t.Start();
            //Reader(reader);
        }

        private void Reader(StreamReader reader)
        {
            try
            {
                Regex lineRegex = new Regex("^([0-9]*Z)\\s+(\\w+)\\s+([^>]+)>\\s*(.*)$", RegexOptions.IgnoreCase);
                while (true)
                {
                    string line = reader.ReadLine();
                    if (lineRegex.IsMatch(line))
                    {
                        Match match = lineRegex.Match(line);
                        OnMessageReceived(new KstMessageEventArgs
                        {
                            Time = match.Groups[1].Value,
                            From = match.Groups[2].Value,
                            Message = match.Groups[3].Value
                        });
                    }                    
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }

        private void OnMessageReceived(KstMessageEventArgs kstEvent)
        {
            if (MessageReceived != null)
                MessageReceived(this, kstEvent);
        }
    }
}
