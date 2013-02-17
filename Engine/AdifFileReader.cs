using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Engine
{
    public sealed class AdifFileReader
    {
        private string m_Content;
        private int m_Offset;

        public static AdifFileReader LoadFromContent(string content)
        {
            return new AdifFileReader { m_Content = content };
        }

        private AdifFileReader()
        {
        }

        private Field ReadField(int maxOffset)
        {
            int indexOfNextTag = m_Content.IndexOf('<', m_Offset);
            if (indexOfNextTag < 0)
                return null;
            else if (indexOfNextTag >= maxOffset)
                return null;

            int tagClosePos = m_Content.IndexOf('>', indexOfNextTag);
            string[] tagParts = m_Content.Substring(indexOfNextTag + 1, tagClosePos - indexOfNextTag - 1).Split(':');
            string tagName = tagParts[0];
            if (tagParts.Length < 2)
                throw new InvalidDataException(string.Format("ADIF tag {0} at position {1} doesn't contain data length", tagName, indexOfNextTag));
            int dataLength = int.Parse(tagParts[1]);
            string dataType;
            if (tagParts.Length > 2)
                dataType = tagParts[2];
            else
                dataType = null;

            string tagData = m_Content.Substring(tagClosePos + 1, dataLength);
            m_Offset = tagClosePos + dataLength + 1;
            return new Field { Tag = tagName, DataLength = dataLength, DataType = dataType, Data = tagData };
        }

        public Record ReadRecord()
        {
            if (m_Offset >= m_Content.Length)
                return null;
            int eorPosition = m_Content.IndexOf("<eor>", m_Offset, StringComparison.InvariantCultureIgnoreCase);
            bool endOfFile = false;
            if (eorPosition < 0)
            {
                eorPosition = m_Content.Length;
                endOfFile = true;
            }

            List<Field> fields = new List<Field>();
            while (m_Offset < eorPosition)
            {
                Field field = ReadField(eorPosition);
                if (field != null)
                    fields.Add(field);
                else
                    break;
            }
            m_Offset = eorPosition + "<eor>".Length;

            // If we're at the end of the file and have no fields, return null.
            // If not the end of the file, it could well be an empty record (i.e. <eor><eor>)
            if (endOfFile && fields.Count == 0)
                return null;
            else
                return new Record { Fields = fields };
        }

        public Header ReadHeader()
        {
            int eohPosition = m_Content.IndexOf("<eoh>", m_Offset, StringComparison.InvariantCultureIgnoreCase);
            if (eohPosition < m_Offset)
                return null;

            string headerData = m_Content.Substring(m_Offset, eohPosition - m_Offset);
            m_Offset = eohPosition + "<eoh>".Length;

            return new Header { HeaderData = headerData };
        }

        public static DateTime ParseAdifDate(string dateField, string timeField)
        {
            if (timeField != null)
                return new DateTime(int.Parse(dateField.Substring(0, 4)), int.Parse(dateField.Substring(4, 2)), int.Parse(dateField.Substring(6, 2)), int.Parse(timeField.Substring(0, 2)), int.Parse(timeField.Substring(2, 2)), 0);
            else
                return new DateTime(int.Parse(dateField.Substring(0, 4)), int.Parse(dateField.Substring(4, 2)), int.Parse(dateField.Substring(6, 2)));
        }

        public class Field
        {
            public string Tag { get; set; }
            public int DataLength { get; set; }
            public string DataType { get; set; }
            public string Data { get; set; }
        }

        public class Record
        {
            public List<Field> Fields { get; set; }

            public string this[string key]
            {
                get
                {
                    Field field = Fields.Find(f => string.Equals(f.Tag, key, StringComparison.InvariantCultureIgnoreCase));
                    if (field == null)
                        return null;
                    else
                        return field.Data;
                }
            }
        }

        public class Header
        {
            public string HeaderData { get; set; }
        }
    }
}
