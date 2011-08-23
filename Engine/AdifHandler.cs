using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class AdifHandler
    {
        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                foreach (Contact c in contacts)
                    ExportContact(c, writer);
            }
        }

        private static void ExportContact(Contact contact, StreamWriter writer)
        {
            WriteField("call", contact.Callsign, writer);
            WriteField("band", BandHelper.ToString(contact.Band), writer);
            WriteField("mode", ModeHelper.ToString(contact.Mode), writer);
            WriteField("qso_date", contact.StartTime.ToString("yyyyMMdd"), writer);
            WriteField("time_on", contact.StartTime.ToString("HHmm"), writer);
            writer.WriteLine("<eor>");
        }

        private static void WriteField(string fieldName, string val, StreamWriter writer)
        {
            writer.Write(string.Format("<{0}:{1}>{2}", fieldName, val.Length, val));
        }
    }
}
