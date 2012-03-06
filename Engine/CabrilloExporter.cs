using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class CabrilloExporter
    {
        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation, string c_LocatorSent, string c_CallUsed, string c_Operators, string c_Contest, string c_ClaimedScore)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                writer.WriteLine("START-OF-LOG: 2.0");
                writer.WriteLine("CREATED-BY: M0VFC CamLog");
                if (!string.IsNullOrEmpty(c_Contest))
                {
                    writer.WriteLine("CONTEST: " + c_Contest.ToUpper());
                }
                writer.WriteLine("CALLSIGN: " + c_CallUsed.ToUpper());
                if (!string.IsNullOrEmpty(c_ClaimedScore))
                {
                    writer.WriteLine("CLAIMED SCORE: " + c_ClaimedScore);
                }
                if (!string.IsNullOrEmpty(c_Operators))
                {
                    writer.WriteLine("OPERATORS: " + c_Operators.ToUpper());
                }
                else
                {
                    writer.WriteLine("OPERATORS: " + c_CallUsed.ToUpper());
                }
                writer.WriteLine();
                foreach (Contact c in contacts)
                    ExportContact(c, writer, c_LocatorSent, c_CallUsed);
            }
        }

        private static void ExportContact(Contact c, StreamWriter writer, string c_LocatorSent, string c_CallUsed)
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
                 c.LocatorReceived.ToString().ToUpper(),
                 c.ReportReceived.PadRight(3),
                 c.SerialReceived.ToString().PadLeft(3));
        }
    }
}