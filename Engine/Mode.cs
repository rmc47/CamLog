using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public enum Mode
    {
        Unknown =0,
        CW,
        SSB,
        FM,
        JT6m,
        JT65b,
        JT4Other,
        FSK441,
        PSK31,
        RTTY,
        SSTV,
        DStar
    }

    public static class ModeHelper
    {
        public static string ToString(Mode m)
        {
            switch (m)
            {
                case Mode.CW: return "CW";
                case Mode.SSB: return "SSB";
                case Mode.FM: return "FM";
                case Mode.JT6m: return "JT6m";
                case Mode.JT65b: return "JT65b";
                case Mode.JT4Other: return "JT4*";
                case Mode.PSK31: return "PSK31";
                case Mode.FSK441: return "FSK441";
                case Mode.RTTY: return "RTTY";
                case Mode.DStar: return "D-STAR";
                case Mode.SSTV: return "SSTV";
                default: return "Unknown";
            }
        }

        public static string ToOfficialString(Mode m)
        {
            switch (m)
            {
                case Mode.CW: return "A1A";
                case Mode.SSB: return "J3E";
                case Mode.FM: return "F3E";
                default: return "???";
            }
        }
        public static Mode Parse(string val)
        {
            if (val == null) return Mode.Unknown;

            switch (val.Trim().ToUpperInvariant())
            {
                case "CW": return Mode.CW;
                case "SSB": return Mode.SSB;
                case "FM": return Mode.FM;
                case "JT6M": return Mode.JT6m;
                case "JT65B": return Mode.JT65b;
                case "JT4*": return Mode.JT4Other;
                case "PSK31": return Mode.PSK31;
                case "FSK441": return Mode.FSK441;
                case "RTTY": return Mode.RTTY;
                case "DSTAR": return Mode.DStar;
                case "SSTV": return Mode.SSTV;
                default: return Mode.Unknown;
            }
        }

        public static string GetDefaultReport(Mode m)
        {
            switch (m)
            {
                case Mode.CW:
                    return "599";
                default:
                    return "59";
            }
        }
    }
}
