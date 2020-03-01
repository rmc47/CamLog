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
            WriteField("gridsquare", contact.LocatorReceivedString, writer);
            writer.WriteLine("<EOR>");
            writer.WriteLine("");
        }

        private static void WriteField(string fieldName, string val, StreamWriter writer)
        {
            writer.Write(string.Format("<{0}:{1}>{2}", fieldName.ToUpper(), val.Length, val.ToUpper() + " "));
        }

        public static List<Contact> ImportAdif(string source, string station, int sourceId, string defaultOperator)
        {
            AdifFileReader adifReader = AdifFileReader.LoadFromContent(source);

            //int offset = 0;
            AdifFileReader.Header header = adifReader.ReadHeader();

            List<Contact> contacts = new List<Contact>();

            AdifFileReader.Record currentRecord;
            while ((currentRecord = adifReader.ReadRecord()) != null)
            {
                Contact c = GetContact(currentRecord);
                c.SourceId = sourceId;
                c.Operator = c.Operator ?? defaultOperator;
                c.Station = station;
                contacts.Add(c);
            }

            return contacts;
        }

        public static Contact GetContact(AdifFileReader.Record record)
        {
            Contact c = new Contact();
            c.Callsign = record["call"];

            // This parsing is horrid. TODO: Figure out how to use IFormatProvider properly.
            string dateStr = record["qso_date"];
            string timeOnStr = record["time_on"];
            DateTime? date = AdifFileReader.ParseAdifDate(dateStr, timeOnStr);
            c.StartTime = c.EndTime = date.Value;

            c.Band = BandHelper.Parse(record["band"]);
            if (record["freq"] != null)
                c.Frequency = (long)(decimal.Parse(record["freq"]) * 1000000);
            c.ReportReceived = record["rst_rcvd"];
            c.ReportSent = record["rst_sent"];
            c.Operator = record["operator"];
            c.Mode = ModeHelper.Parse(record["mode"]);
            c.LastModified = DateTime.UtcNow;

            if (record["gridsquare"] != null)
            {
                try
                {
                    c.LocatorReceived = new Locator(record["gridsquare"]);
                }
                catch { }
            }

            // QSL info...
            if (record["qslrdate"] != null)
            {
                c.QslRxDate = AdifFileReader.ParseAdifDate(record["qslrdate"], null);
            }
            if (record["qsl_rcvd_via"] != null && c.QslRxDate != null)
            {
                if (record["qsl_rcvd_via"].Trim() == "D")
                    c.QslMethod = "Direct";
                else if (record["qsl_rcvd_via"].Trim() == "B")
                    c.QslMethod = "Bureau";
            }
            if (record["qslsdate"] != null)
            {
                c.QslTxDate = AdifFileReader.ParseAdifDate(record["qslsdate"], null);
            }

            return c;
        }
    }
}