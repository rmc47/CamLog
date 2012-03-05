using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class CabrilloExporter
    {
        const string c_CallUsed = "G6UW"; // Callsign sent
        const string c_LocatorSent = "JO02af"; // Locator sent
        const string c_Contest = "RSGB-UKAC"; // Contest name (optional)
        const string c_Operators = "G3ZAY M0ZXA"; // Operators
        const string c_ClaimedScore = ""; // Claimed score (optional)

        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                writer.WriteLine("START-OF-LOG: 2.0");
                writer.WriteLine("CREATED-BY: M0VFC CamLog");
                if (string.IsNullOrEmpty(c_Contest))
                {
                    writer.WriteLine("CONTEST: " + c_Contest.ToUpper());
                }
                writer.WriteLine("CALLSIGN: " + c_CallUsed.ToUpper());
                if (string.IsNullOrEmpty(c_ClaimedScore))
                {
                    writer.WriteLine("CLAIMED SCORE: " + c_ClaimedScore);
                }
                writer.WriteLine("OPERATORS: " + c_Operators.ToUpper());
                writer.WriteLine();
                foreach (Contact c in contacts)
                    ExportContact(c, writer);
            }
        }

        private static void ExportContact(Contact c, StreamWriter writer)
        {
            writer.WriteLine("QSO: {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                 BandHelper.ToMHzString(c.Band).PadRight(4),
                 ModeHelper.ToCabrilloString(c.Mode),
                 c.StartTime.ToString("yyyy-MM-dd HHmm"),
                 c_CallUsed.PadRight(15).ToUpper(),
                 c_LocatorSent.ToUpper(),
                 c.ReportSent.PadRight(3),
                 c.SerialSent.ToString().PadRight(3),
                 c.Callsign.PadRight(15).ToUpper(),
                 c.LocatorReceived.ToString().ToUpper(),
                 c.ReportReceived.PadRight(3),
                 c.SerialReceived.ToString().PadRight(3));
        }
    }
}