using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace QslEngine
{
    public sealed class ClubLogApi
    {
        private CookieContainer m_CookieJar = new CookieContainer();

        public void Login(string username, string password)
        {
            string loginPostBody = string.Format("fEmail={0}&fPassword={1}", Uri.EscapeDataString(username), Uri.EscapeDataString(password));
            DoPostRequest("https://secure.clublog.org/login.php", loginPostBody);
        }

        public string DownloadLog(string callsign)
        {
            string downloadPostBody = string.Format("call={0}&Get+ADIF=Download", Uri.EscapeDataString(callsign));
            return DoPostRequest("https://secure.clublog.org/getadif.php", downloadPostBody);
        }

        public string DownloadOqrsAdif(string callsign)
        {
            string downloadPostBody = string.Format("submit=Download+QSL+ADIF&call={0}&type=dxqsl&startyear=0&startmonth=0&startday=0&endyear=0&endmonth=0&endday=0&adifmode=0", Uri.EscapeDataString(callsign));
            return DoPostRequest("https://secure.clublog.org/getadif.php", downloadPostBody);
        }

        private string DoPostRequest(string url, string body)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = m_CookieJar;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ProtocolVersion = new Version(1, 0);
            using (StreamWriter writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(body);
            }
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
