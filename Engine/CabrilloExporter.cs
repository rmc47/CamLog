using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class CabrilloExporter
    {
        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                foreach (Contact c in contacts)
                    ExportContact(c, writer);
            }
        }

        private static void ExportContact(Contact c, StreamWriter writer)
        {
            writer.WriteLine("QSO: {0} {1} {2} {3} {4} {5} {6} {7} {8}",
                BandHelper.ToKHz(c.Band),
                "PH",
                c.StartTime.ToString("yyyy-MM-dd HHmm"),
                "G2XV/P",
                c.ReportSent,
                c.SerialSent,
                c.Callsign,
                c.ReportReceived,
                c.SerialReceived);
        }
    }
}
