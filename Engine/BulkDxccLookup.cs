using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Engine
{
    public class BulkDxccLookup
    {
        private string m_ApiKey;

        public BulkDxccLookup()
        {
            m_ApiKey = RegistryHelper.GetString(RegistryValue.ClublogApiKey, string.Empty);
        }

        public Dictionary<string, int> Lookup(List<string> callsigns)
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.clublog.org/bulkdxcc?api=" + m_ApiKey);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var requestStream = request.GetRequestStream();
            using (StreamWriter writer = new StreamWriter(requestStream))
            {
                writer.Write("json=");
                var serializedCallsigns = JsonConvert.SerializeObject(callsigns.ConvertAll(c => new LookupCallsignRequest { Callsign = c, Date = DateTime.UtcNow }), Formatting.None);
                writer.Write(Uri.EscapeDataString(serializedCallsigns));
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream))
            {
                string responseString = reader.ReadToEnd();
            }

            return results;
        }

        private class LookupCallsignRequest
        {
            [JsonProperty("C")]
            public string Callsign { get; set; }
            [JsonProperty("T", ItemConverterType=typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
            public DateTime Date { get; set; }
        }

        private class LookupCallsignResult
        {
            [JsonProperty("C")]
            public string Callsign { get; set; }
            [JsonProperty("A")]
            public int AdifNumber { get; set; }
        }
    }
}
