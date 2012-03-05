using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class AdifHandler
    {
        const string c_CallUsed = "G6UW";    	    // Callsign sent
        const string c_LocatorSent = "JO02af";	    // Locator sent
        const string c_Contest = "RSGB-UKAC";	    // Contest name
        const string c_Section = "AO";              // Contest section
        const string c_Operators = "G3ZAY M0ZXA";	// Operators
        
        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                writer.WriteLine("ADIF export from M0VFC CamLog");
                writer.WriteLine("");
                writer.WriteLine("---");
                writer.WriteLine("Callsign : " + c_CallUsed.ToUpper() + "      SECTION/ZONE: " + c_Section.ToUpper() + "      Grid square : " + c_LocatorSent.ToUpper() );
                writer.WriteLine("Operator(s) : " + c_Operators.ToUpper());
                writer.WriteLine("---");
                writer.WriteLine();
                if (string.IsNullOrEmpty(c_Contest))
                {
                    writer.WriteLine("<CONTEST_ID:" + c_Contest.Length + ">" + c_Contest.ToUpper());
                }
                writer.WriteLine("<EOH>");
                writer.WriteLine();
                
                foreach (Contact c in contacts)
                    ExportContact(c, writer);
            }
        }

        private static void ExportContact(Contact contact, StreamWriter writer)
        {
            WriteField("call", contact.Callsign, writer);
            WriteField("qso_date", contact.StartTime.ToString("yyyyMMdd"), writer);
            WriteField("time_on", contact.StartTime.ToString("HHmm"), writer);
            WriteField("mode", ModeHelper.ToString(contact.Mode), writer);
            WriteField("band", BandHelper.ToString(contact.Band), writer);
            WriteField("rst_sent", contact.ReportSent, writer);
            WriteField("stx", contact.ReportSent, writer);
            WriteField("rst_rcvd", contact.ReportReceived, writer);
            WriteField("srx", contact.SerialReceived.ToString(), writer);
            WriteField("gridsquare", contact.LocatorReceived.ToString(), writer);
            writer.WriteLine("<EOR>");
            writer.WriteLine("");
        }

        private static void WriteField(string fieldName, string val, StreamWriter writer)
        {
            writer.Write(string.Format("<{0}:{1}>{2}", fieldName.ToUpper(), val.Length, val.ToUpper() + " "));
        }
    }
}