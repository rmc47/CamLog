using System;
using System.Collections.Generic;
using System.Text;
using RigCAT.NET;

namespace Engine
{
    public enum Mode
    {
        Unknown =0,
        CW,
        SSB,
        FM,
        AM,
        JT6m,
        JT65b,
        JT65c,
        JT4Other,
        JTMS,
        FSK441,
        PSK31,
        PSK63,
        PSK125,
        RTTY,
        SSTV,
        DStar,
        ISCAT,
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
                case Mode.AM: return "AM";
                case Mode.JT6m: return "JT6m";
                case Mode.JT65b: return "JT65b";
                case Mode.JT65c: return "JT65c";
                case Mode.JT4Other: return "JT4*";
                case Mode.PSK31: return "PSK31";
                case Mode.PSK63: return "PSK63";
                case Mode.PSK125: return "PSK125";
                case Mode.FSK441: return "FSK441";
                case Mode.RTTY: return "RTTY";
                case Mode.DStar: return "D-STAR";
                case Mode.SSTV: return "SSTV";
                case Mode.ISCAT: return "ISCAT";
                default: return "Unknown";
            }
        }

        public static string ToString(OperatingMode m)
        {
            switch (m)
            {
                case OperatingMode.AM: return "AM";
                case OperatingMode.CW: return "CW";
                case OperatingMode.CWRev: return "CW";
                case OperatingMode.Data: return "Data";
                case OperatingMode.DataRev: return "Data";
                case OperatingMode.FM: return "FM";
                case OperatingMode.LSB: return "SSB";
                case OperatingMode.USB: return "SSB";
                default: return "Unknown";
            }
        }

        public static string ToOfficialString(Mode m)
        {
            switch (m)
            {
                case Mode.CW: return "A1A";
                case Mode.SSB: 
                case Mode.AM: return "J3E";
                case Mode.FM: return "F3E";
                default: return "???";
            }
        }
        
        public static string ToCabrilloString(Mode m)
        {
            switch (m)
            {
                case Mode.CW: return "CW";
                case Mode.SSB: return "PH";
                case Mode.FM: return "PH";
                case Mode.AM: return "PH";
                default: return "??";
            }
        }
        
        public static Mode Parse(string val)
        {
            if (val == null) return Mode.Unknown;

            switch (val.Trim().ToUpperInvariant())
            {
                case "CW": return Mode.CW;
                case "SSB": return Mode.SSB;
                case "AM": return Mode.AM;
                case "FM": return Mode.FM;
                case "JTMS": return Mode.JTMS;
                case "JT6M": return Mode.JT6m;
                case "JT65B": return Mode.JT65b;
                case "JT65C": return Mode.JT65c;
                case "JT4*": return Mode.JT4Other;
                case "PSK31": return Mode.PSK31;
                case "PSK63": return Mode.PSK63;
                case "PSK125": return Mode.PSK125;
                case "FSK441": return Mode.FSK441;
                case "RTTY": return Mode.RTTY;
                case "DSTAR": return Mode.DStar;
                case "SSTV": return Mode.SSTV;
                case "ISCAT": return Mode.ISCAT;
                default: return Mode.Unknown;
            }
        }

        public static string GetDefaultReport(Mode m)
        {
            switch (m)
            {
                case Mode.CW:
                    return "599";
                case Mode.JT6m:
                case Mode.FSK441:
                case Mode.JTMS:
                    return "26";
                case Mode.JT4Other:
                case Mode.JT65b:
                case Mode.JT65c:
                    return "00";
                case Mode.ISCAT:
                    return "-15";
                default:
                    return "59";
            }
        }
    }
}
