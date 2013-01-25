using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class CabrilloExporter
    {
        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation, string locatorSent, string callUsed, string operators, string contest, string claimedScore)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                writer.WriteLine("START-OF-LOG: 2.0");
                writer.WriteLine("CREATED-BY: M0VFC CamLog");
                if (!string.IsNullOrEmpty(contest))
                {
                    writer.WriteLine("CONTEST: " + contest.ToUpper());
                }
                writer.WriteLine("CALLSIGN: " + callUsed.ToUpper());
                if (!string.IsNullOrEmpty(claimedScore))
                {
                    writer.WriteLine("CLAIMED SCORE: " + claimedScore);
                }
                if (!string.IsNullOrEmpty(operators))
                {
                    writer.WriteLine("OPERATORS: " + operators.ToUpper());
                }
                else
                {
                    writer.WriteLine("OPERATORS: " + callUsed.ToUpper());
                }
                writer.WriteLine();
                if (contest == "RSGB AFS - Club Calls")
                {
                    foreach (Contact c in contacts)
                        Export_ClubCalls(c, writer, locatorSent, callUsed);
                }
                else if (contest == "RSGB AFS - 80m SSB/CW")
                {
                    foreach (Contact c in contacts)
                        Export_80mAFS(c, writer, locatorSent, callUsed);
                }
                else
                {
                    foreach (Contact c in contacts)
                        Export_UKAC(c, writer, locatorSent, callUsed);
                }
            }
        }

        private static void Export_ClubCalls(Contact c, StreamWriter writer, string c_LocatorSent, string c_CallUsed)
        {
            // Munge the comments field for Club Calls
            string[] notesParts = c.Notes.ToUpper().Split(' ');
            string notes;
            if (notesParts[0] == "CS")
                notes = "CLUB   " + notesParts[1];
            else if (notesParts[0] == "CM")
                notes = "MEMBER " + notesParts[1];
            else
                notes = "NONE   ----";

            writer.WriteLine("QSO: {0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                 BandHelper.ToMHzString(c.Band).PadRight(4),
                 ModeHelper.ToCabrilloString(c.Mode),
                 c.StartTime.ToString("yyyy-MM-dd HHmm"),
                 c_CallUsed.PadRight(15).ToUpper(),
                 c.ReportSent.PadRight(3),
                 c.SerialSent.ToString().PadLeft(3),
                 c.Callsign.PadRight(15).ToUpper(),
                 c.ReportReceived.PadRight(3),
                 c.SerialReceived.ToString().PadLeft(3),
                 notes);
        }

        private static void Export_80mAFS(Contact c, StreamWriter writer, string c_LocatorSent, string c_CallUsed)
        {
            writer.WriteLine("QSO: {0} {1} {2} {3} {4} {5} {6} {7} {8}",
                 c.Frequency.ToString().Substring(0, 4),
                 ModeHelper.ToCabrilloString(c.Mode),
                 c.StartTime.ToString("yyyy-MM-dd HHmm"),
                 c_CallUsed.PadRight(15).ToUpper(),
                 c.ReportSent.PadRight(3),
                 c.SerialSent.ToString().PadLeft(3),
                 c.Callsign.PadRight(15).ToUpper(),
                 c.ReportReceived.PadRight(3),
                 c.SerialReceived.ToString().PadLeft(3));
        }
        
        private static void Export_UKAC(Contact c, StreamWriter writer, string c_LocatorSent, string c_CallUsed)
        {
            writer.WriteLine("QSO: {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                 BandHelper.ToMHzString(c.Band).PadRight(4),
                 ModeHelper.ToCabrilloString(c.Mode),
                 c.StartTime.ToString("yyyy-MM-dd HHmm"),
                 c_CallUsed.PadRight(15).ToUpper(),
                 c_LocatorSent.ToUpper(),
                 c.ReportSent.PadRight(3),
                 c.SerialSent.ToString().PadLeft(3),
                 c.Callsign.PadRight(15).ToUpper(),
                 c.LocatorReceivedString,
                 c.ReportReceived.PadRight(3),
                 c.SerialReceived.ToString().PadLeft(3));
        }
    }
}