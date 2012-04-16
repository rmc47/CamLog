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

        public static List<Contact> ImportAdif(string sourceFile)
        {
            string adifFile = File.ReadAllText(sourceFile);

            int offset = 0;
            AdifHeader header = ReadHeader(adifFile, ref offset);

            List<Contact> contacts = new List<Contact>();

            AdifRecord currentRecord;
            while ((currentRecord = ReadRecord(adifFile, ref offset)) != null)
            {
                Contact c = new Contact();
                c.Callsign = currentRecord["call"];

                // This parsing is horrid. TODO: Figure out how to use IFormatProvider properly.
                string dateStr = currentRecord["qso_date"];
                string timeOnStr = currentRecord["time_on"];
                DateTime date = new DateTime(int.Parse(dateStr.Substring(0, 4)), int.Parse(dateStr.Substring(4, 2)), int.Parse(dateStr.Substring(6, 2)), int.Parse(timeOnStr.Substring(0, 2)), int.Parse(timeOnStr.Substring(2, 2)), 0);
                c.StartTime = date;

                c.Band = BandHelper.Parse(currentRecord["band"]);
                c.Frequency = (long)(decimal.Parse(currentRecord["freq"]) * 1000000);
                c.ReportReceived = currentRecord["rst_rcvd"];
                c.ReportSent = currentRecord["rst_sent"];
                c.Operator = currentRecord["operator"];
                c.Mode = ModeHelper.Parse(currentRecord["mode"]);
                c.Station = string.Empty;
                contacts.Add(c);
            }

            return contacts;
        }

        private static AdifField ReadField(string str, int maxOffset, ref int offset)
        {
            int indexOfNextTag = str.IndexOf('<', offset);
            if (indexOfNextTag < 0)
                return null;
            else if (indexOfNextTag >= maxOffset)
                return null;

            int tagClosePos = str.IndexOf('>', indexOfNextTag);
            string[] tagParts = str.Substring(indexOfNextTag + 1, tagClosePos - indexOfNextTag - 1).Split(':');
            string tagName = tagParts[0];
            if (tagParts.Length < 2)
                throw new InvalidDataException(string.Format("ADIF tag {0} at position {1} doesn't contain data length", tagName, indexOfNextTag));
            int dataLength = int.Parse(tagParts[1]);
            string dataType;
            if (tagParts.Length > 2)
                dataType = tagParts[2];
            else
                dataType = null;

            string tagData = str.Substring(tagClosePos + 1, dataLength);
            offset = tagClosePos + dataLength + 1;
            return new AdifField { Tag = tagName, DataLength = dataLength, DataType = dataType, Data = tagData };
        }

        private static AdifRecord ReadRecord(string str, ref int offset)
        {
            if (offset >= str.Length)
                return null;
            int eorPosition = str.IndexOf("<eor>", offset, StringComparison.InvariantCultureIgnoreCase);
            bool endOfFile = false;
            if (eorPosition < 0)
            {
                eorPosition = str.Length;
                endOfFile = true;
            }

            List<AdifField> fields = new List<AdifField>();
            while (offset < eorPosition)
            {
                AdifField field = ReadField(str, eorPosition, ref offset);
                if (field != null)
                    fields.Add(field);
                else
                    break;
            }
            offset = eorPosition + "<eor>".Length;

            // If we're at the end of the file and have no fields, return null.
            // If not the end of the file, it could well be an empty record (i.e. <eor><eor>)
            if (endOfFile && fields.Count == 0)
                return null;
            else
                return new AdifRecord { Fields = fields };
        }

        private static AdifHeader ReadHeader(string str, ref int offset)
        {
            int eohPosition = str.IndexOf("<eoh>", offset, StringComparison.InvariantCultureIgnoreCase);
            if (eohPosition < offset)
                return null;

            string headerData = str.Substring(offset, eohPosition - offset);
            offset = eohPosition + "<eoh>".Length;

            return new AdifHeader { HeaderData = headerData };
        }

        private class AdifField
        {
            public string Tag { get; set; }
            public int DataLength { get; set; }
            public string DataType { get; set; }
            public string Data { get; set; }
        }

        private class AdifRecord
        {
            public List<AdifField> Fields { get; set; }

            public string this[string key]
            {
                get
                {
                    AdifField field = Fields.Find(f => string.Equals(f.Tag, key, StringComparison.InvariantCultureIgnoreCase));
                    if (field == null)
                        return null;
                    else
                        return field.Data;
                }
            }
        }

        private class AdifHeader
        {
            public string HeaderData { get; set; }
        }
    }
}