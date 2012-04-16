using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Engine
{
    public class WinKey : IDisposable
    {
        private readonly string m_PortName;
        private readonly object m_Lock = new object ();

        private SerialPort m_Port;
        
        public WinKey(string port)
        {
            m_PortName = port;
            BeginOpenPort();
        }

        public void SendString(string str)
        {
            // Should be pretty much async
            if (!EnsurePortOpen())
                throw new InvalidOperationException("Cannot open WinKey on port " + m_PortName);

            byte[] bytes = Encoding.ASCII.GetBytes(str);
            m_Port.Write(bytes,0, bytes.Length);
        }

        public void StopSending()
        {
            EnsurePortOpen(); // Don't care too much if this fails
            m_Port.Write(new byte[] { 0x0A }, 0, 1);
        }

        private void BeginOpenPort()
        {
            ThreadPool.QueueUserWorkItem(_ => EnsurePortOpen());
        }

        private bool EnsurePortOpen()
        {
            try
            {
                if (m_Port != null && m_Port.IsOpen)
                    return true;

                lock (m_Lock)
                {
                    // Re-check after acquiring lock
                    if (m_Port != null && m_Port.IsOpen)
                        return true;

                    // Get rid of any old closed ports
                    if (m_Port != null)
                        m_Port.Dispose();

                    m_Port = new SerialPort(m_PortName);
                    m_Port.BaudRate = 1200;
                    m_Port.DtrEnable = true;
                    m_Port.Open();

                    // A quick echo test to make sure it exists
                    m_Port.DiscardInBuffer();
                    m_Port.Write(new byte[] { 0x00, 0x04, 0x12 }, 0, 3);
                    int echo = m_Port.ReadChar();
                    if (echo != 0x12)
                        return false; // Something went wrong opening the port - not a WinKey?

                    // Open host mode
                    m_Port.Write(new byte[] { 0x00, 0x02 }, 0, 2);
                    int version = m_Port.ReadChar();
                    if (version < 20 || version > 30)
                        return false; // Weird version code - not a WinKey?

                    m_Port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                    
                    // Use the speed pot value
                    m_Port.Write(new byte[] { 0x02, 0x00 }, 0, 2);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            m_Port.DiscardInBuffer();
        }

        public void Dispose()
        {
            if (m_Port != null)
            {
                if (m_Port.IsOpen)
                {
                    // Close host mode if we're still open
                    m_Port.Write(new byte[] { 0x00, 0x03 }, 0, 2);
                }

                m_Port.Dispose();
                m_Port = null;
            }
        }
    }
}
