using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public enum Band
    {
        Unknown = 0,
        B160m,
        B80m,
        B60m,
        B40m,
        B30m,
        B20m,
        B17m,
        B15m,
        B12m,
        B10m,
        B6m,
        B4m,
        B2m,
        B70cm,
        B23cm
    }

    public static class BandHelper
    {
        public static Band Parse(string val)
        {
            if (val == null) return Band.Unknown;
            val = val.Trim();
            if (val.EndsWith("m", StringComparison.InvariantCultureIgnoreCase))
                val = val.Substring(0, val.Length - 1);

            switch (val.ToLowerInvariant().Trim())
            {
                case "160": return Band.B160m;
                case "80": return Band.B80m;
                case "60": return Band.B60m;
                case "40": return Band.B40m;
                case "30": return Band.B30m;
                case "20": return Band.B20m;
                case "17": return Band.B17m;
                case "15": return Band.B15m;
                case "12": return Band.B12m;
                case "10": return Band.B10m;
                case "6": return Band.B6m;
                case "4": return Band.B4m;
                case "2": return Band.B2m;
                case "70":
                case "70c": return Band.B70cm;
                case "23":
                case "23c": return Band.B23cm;
                default: return Band.Unknown;
            }
        }

        public static string ToString(Band band)
        {
            switch (band)
            {
                case Band.B160m: return "160m";
                case Band.B80m: return "80m";
                case Band.B60m: return "60m";
                case Band.B40m: return "40m";
                case Band.B30m: return "30m";
                case Band.B20m: return "20m";
                case Band.B17m: return "17m";
                case Band.B15m: return "15m";
                case Band.B12m: return "12m";
                case Band.B10m: return "10m";
                case Band.B6m: return "6m";
                case Band.B4m: return "4m";
                case Band.B2m: return "2m";
                case Band.B70cm: return "70cm";
                case Band.B23cm: return "23cm";
                default: return "Unknown";
            }
        }

        public static string ToMHzString(Band band)
        {
            switch (band)
            {
                case Band.B160m: return "1.8";
                case Band.B80m: return "3.5";
                case Band.B60m: return "5";
                case Band.B40m: return "7";
                case Band.B30m: return "10";
                case Band.B20m: return "14";
                case Band.B17m: return "18";
                case Band.B15m: return "21";
                case Band.B12m: return "24";
                case Band.B10m: return "28";
                case Band.B6m: return "50";
                case Band.B4m: return "70";
                case Band.B2m: return "144";
                case Band.B70cm: return "430";
                case Band.B23cm: return "1296";
                default: return "?";
            }
        }

        public static int ToKHz(Band band)
        {
            switch (band)
            {
                case Band.B160m: return 1800;
                case Band.B80m: return 3500;
                case Band.B40m: return 7000;
                case Band.B20m: return 14000;
                case Band.B15m: return 21000;
                case Band.B10m: return 28000;
                default: throw new ArgumentOutOfRangeException ("Unknown band -> KHz conversion");
            }
        }

        public static Band FromFrequency(long frequency)
        {
            double frequencyInMhz = (double)frequency / 1000000;
            if (frequencyInMhz < 1.5)
                return Band.Unknown;
            else if (frequencyInMhz < 2)
                return Band.B160m;
            else if (frequencyInMhz < 4)
                return Band.B80m;
            else if (frequencyInMhz < 6)
                return Band.B60m;
            else if (frequencyInMhz < 8)
                return Band.B40m;
            else if (frequencyInMhz < 12)
                return Band.B30m;
            else if (frequencyInMhz < 15)
                return Band.B20m;
            else if (frequencyInMhz < 19)
                return Band.B17m;
            else if (frequencyInMhz < 22)
                return Band.B15m;
            else if (frequencyInMhz < 26)
                return Band.B12m;
            else if (frequencyInMhz < 30)
                return Band.B10m;
            else if (frequencyInMhz < 55)
                return Band.B6m;
            else if (frequencyInMhz < 80)
                return Band.B4m;
            else if (frequencyInMhz < 150)
                return Band.B2m;
            else if (frequencyInMhz < 450)
                return Band.B70cm;
            else if (frequencyInMhz < 1350)
                return Band.B23cm;
            else
                return Band.Unknown;
        }
    }
}
