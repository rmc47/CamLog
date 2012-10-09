using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Engine
{
    public class CallsignLookup
    {
        private Dictionary<string, PrefixRecord> m_PrefixRecords;
        private static readonly Regex s_IgnoredPartRegex = new Regex("^([0-9]|A|M|P|AM|MM|QRP)$");

        public CallsignLookup(string countryXmlFile)
        {
            if (!File.Exists(countryXmlFile))
                throw new ArgumentException("File '" + countryXmlFile + "' not found");
            
            // Parse the cty.xml file
            XmlDocument doc = new XmlDocument();
            using (FileStream stream = File.OpenRead(countryXmlFile))
            {
                using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    doc.Load(zipStream);
                }
            }

            m_PrefixRecords = new Dictionary<string,PrefixRecord> ();
            XmlNodeList prefixes = doc.DocumentElement["prefixes"].ChildNodes;
            DateTime now = DateTime.UtcNow;
            foreach (XmlElement prefixElement in prefixes)
            {
                PrefixRecord record = new PrefixRecord(prefixElement);
                if (record.StartDate != null && record.StartDate > now)
                    continue;
                if (record.EndDate != null && record.EndDate < now)
                    continue;
                m_PrefixRecords[record.Call] = record;
            }
        }

        public PrefixRecord LookupPrefix(string callsign)
        {
            if (callsign == null)
                throw new ArgumentNullException("callsign");

            string[] callsignParts = callsign.Split('/');

            // Ignore mobile suffixes etc if they're the last part
            bool ignoreLastPart = s_IgnoredPartRegex.IsMatch(callsignParts[callsignParts.Length - 1].ToUpperInvariant());

            // Get the sorted list of indices - starting with the shortest part
            List<int> partOrder = new List<int>();
            
            // Find the shortest prefix
            int shortestPart = 0;
            for (int i = 0; i < (ignoreLastPart ? callsignParts.Length - 1 : callsignParts.Length); i++)
            {
                if (callsignParts[i].Length < callsignParts[shortestPart].Length)
                    shortestPart = i;
            }

            // Find the longest prefix record that matches it
            string countryPrefixPart = callsignParts[shortestPart].ToUpperInvariant ();
            for (int charsToMatch = countryPrefixPart.Length; charsToMatch >= 0; charsToMatch--)
            {
                PrefixRecord result;
                if (m_PrefixRecords.TryGetValue(countryPrefixPart.Substring(0, charsToMatch), out result))
                    return result;
            }
            return null;
        }
    }

    public class PrefixRecord
    {
        private string m_Call;
        private string m_Entity;
        private int m_Adif;
        private int m_CqZone;
        private int m_ItuZone;
        private string m_Continent;
        private double m_Latitude;
        private double m_Longitude;
        private DateTime? m_StartDate;
        private DateTime? m_EndDate;

        public PrefixRecord(XmlElement prefixRecord)
        {
            m_Call = prefixRecord["call"].InnerText;
            if (prefixRecord["entity"] != null)
                m_Entity = prefixRecord["entity"].InnerText;
            if (prefixRecord["adif"] != null)
                m_Adif = int.Parse(prefixRecord["adif"].InnerText);
            if (prefixRecord["cqz"] != null)
                m_CqZone = int.Parse(prefixRecord["cqz"].InnerText);
            if (prefixRecord["ituz"] != null)
                m_ItuZone = int.Parse(prefixRecord["ituz"].InnerText);
            if (prefixRecord["cont"] != null)
                m_Continent = prefixRecord["cont"].InnerText;
            if (prefixRecord["long"] != null)
                m_Longitude = double.Parse(prefixRecord["long"].InnerText);
            if (prefixRecord["lat"] != null)
                m_Latitude = double.Parse(prefixRecord["lat"].InnerText);
            if (prefixRecord["start"] != null)
                m_StartDate = DateTime.Parse(prefixRecord["start"].InnerText);
            if (prefixRecord["end"] != null)
                m_EndDate = DateTime.Parse(prefixRecord["end"].InnerText);
        }

        public string Call { get { return m_Call; } }
        public string Entity { get { return m_Entity; } }
        public int Adif { get { return m_Adif; } }
        public int CqZone { get { return m_CqZone; } }
        public int ItuZone { get { return m_ItuZone; } }
        public string Continent { get { return m_Continent; } }
        public double Latitude { get { return m_Latitude; } }
        public double Longitude { get { return m_Longitude; } }
        public DateTime? StartDate { get { return m_StartDate; } }
        public DateTime? EndDate { get { return m_EndDate; } }
    }
}
