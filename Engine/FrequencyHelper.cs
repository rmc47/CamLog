using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public static class FrequencyHelper
    {
        public static string ToString(long frequency)
        {
            return string.Format("{0}.{1}.{2}", 
                ((int)Math.Floor((decimal)frequency / 1000000)).ToString(),
                ((int)Math.Floor(((decimal)frequency % 1000000) / 1000)).ToString().PadLeft (3, '0'), 
                ((int)(frequency % 1000)).ToString().PadLeft (3, '0')
                );
        }

        public static long Parse(string frequencyString)
        {
            try
            {
                string[] bits = frequencyString.Split('.');
                int mhz = int.Parse(bits[0]);
                int khz = 0;
                int hz = 0;
                if (bits.Length > 1)
                {
                    bits[1] = bits[1].PadRight(3, '0');
                    khz = int.Parse(bits[1].Substring(0, Math.Min(3, bits[1].Length)));
                    if (bits[1].Length > 3)
                    {
                        string hzBit = bits[1].Substring(3, bits[1].Length - 3);
                        hzBit = hzBit.PadRight(3, '0');
                        hz = int.Parse(hzBit);
                    }
                }
                if (bits.Length > 2)
                    hz = int.Parse(bits[2]);

                return ((long)mhz * 1000000) + (khz * 1000) + hz;
            }
            catch
            {
                return 0;
            }
        }
    }
}
