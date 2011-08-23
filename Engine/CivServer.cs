using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Engine
{
    public sealed class CivServer : IDisposable
    {
        private volatile bool m_Stopping;
        private Thread m_MonitorThread;
        private SerialPort m_Port;
        private long m_Frequency;
        private Mode m_Mode;

        public event EventHandler<EventArgs> FrequencyChanged;
        public event EventHandler<EventArgs> ModeChanged;

        public CivServer(string portName, bool useDtr, bool useRts)
        {
            string[] ports = SerialPort.GetPortNames();

            m_Port = new SerialPort(portName);
            m_Port.BaudRate = 19200;
            m_Port.DtrEnable = useDtr;
            m_Port.RtsEnable = useRts;
            m_Port.Open();

            m_MonitorThread = new Thread(ReadStream);
            m_MonitorThread.IsBackground = true;
            m_MonitorThread.Start();
        }

        public long Frequency
        {
            get { return m_Frequency; }
        }

        public Mode Mode
        {
            get { return m_Mode; }
        }

        public void QueryFrequency()
        {
            byte[] buff = new byte []{ 0xFE, 0xFE, 0x00, 0xE0, 0x03, 0xFD};
            m_Port.Write (buff, 0, buff.Length);
            buff[4] = 0x04;
            // Wait for the buffer to empty or we get a collision on the wire
            //while (m_Port.BytesToRead > 0)
            //    Thread.Sleep(100);
            m_Port.Write(buff, 0, buff.Length);
        }

        private void ReadStream()
        {
            try
            {
                byte[] buff = new byte[1024];
                while (!m_Stopping)
                {
                    int i;
                    for (i = 0; i < buff.Length; i++)
                    {
                        while (m_Port.BytesToRead == 0)
                        {
                            Thread.Sleep(100);
                            if (m_Stopping)
                                return;
                        }

                        m_Port.Read(buff, i, 1);
                        if (buff[i] == 0xFD)
                            break; // End of a command
                    }
                    ProcessCommand(buff, i);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ProcessCommand(byte[] buff, int cb)
        {
            try
            {
                byte to = buff[2];
                byte from = buff[3];
                byte cmd = buff[4];
                switch (cmd)
                {
                    case 0x00: // Set frequency
                    case 0x03:
                        if (cb > 5)
                        {
                            long freq = ParseBcd(buff, 5, cb - 5);
                            if (freq > 0 && freq < 10000000000)
                            {
                                m_Frequency = freq;
                                if (FrequencyChanged != null)
                                    FrequencyChanged(this, new EventArgs());
                            }
                        }
                        break;
                    case 0x01:
                    case 0x04:
                        if (cb > 5)
                        {
                            Mode mode;
                            switch (buff[5])
                            {
                                case 0x00:
                                case 0x01:
                                    mode = Mode.SSB;
                                    break;
                                case 0x03:
                                case 0x07:
                                    mode = Mode.CW;
                                    break;
                                case 0x05:
                                case 0x06:
                                    mode = Mode.FM;
                                    break;
                                default:
                                    mode = Mode.Unknown;
                                    break;
                            }
                            if (mode != Mode.Unknown)
                            {
                                m_Mode = mode;
                                if (ModeChanged != null)
                                    ModeChanged(this, new EventArgs());
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.IO.IOException)
            {
                throw;
            }
            catch
            {
            }
        }

        private long ParseBcd(byte[] buff, int offset, int count)
        {
            long result = 0;
            for (int i = offset+count-1; i >= offset; i--)
            {
                result *= 100;
                result += buff[i] & 0x0F;
                result += ((buff[i] & 0xF0) >> 4) * 10;
            }
            return result;
        }

        public void Dispose()
        {
            m_Stopping = true;
            m_MonitorThread.Join();
        }
    }
}
