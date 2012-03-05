using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class AdifHandler
    {
        const string zxa_callUsed = "G6UW";    	    // Callsign sent
        const string zxa_locatorSent = "JO02af";	    // Locator sent
        const string zxa_contest = "RSGB-UKAC";	    // Contest name
        const string zxa_section = "AO";              // Contest section
        const string zxa_operators = "G3ZAY M0ZXA";	// Operators
        
        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                writer.WriteLine("ADIF export from M0VFC CamLog");
                writer.WriteLine("");
                writer.WriteLine("---");
                writer.WriteLine("Callsign : " + zxa_callUsed.ToUpper() + "      SECTION/ZONE: " + zxa_section.ToUpper() + "      Grid square : " + zxa_locatorSent.ToUpper() );
                writer.WriteLine("Operator(s) : " + zxa_operators.ToUpper());
                writer.WriteLine("---");
                writer.WriteLine("");
                if (string.IsNullOrEmpty(zxa_contest))
                {
                    writer.WriteLine("<CONTEST_ID:" + zxa_contest.Length + ">" + zxa_contest.ToUpper());
                }
                writer.WriteLine("<EOH>");
                writer.WriteLine("");
                
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