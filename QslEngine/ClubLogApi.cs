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
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://secure.clublog.org/login.php");
            req.CookieContainer = m_CookieJar;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ProtocolVersion = new Version(1,0);
            using (StreamWriter writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(string.Format("fEmail={0}&fPassword={1}", Uri.EscapeDataString(username), Uri.EscapeDataString(password)));
            }
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        }

        public string DownloadAdif(string callsign)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://secure.clublog.org/getadif.php");
            req.CookieContainer = m_CookieJar;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ProtocolVersion = new Version(1, 0);
            using (StreamWriter writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(string.Format("submit=Download+QSL+ADIF&call={0}&type=dxqsl&startyear=0&startmonth=0&startday=0&endyear=0&endmonth=0&endday=0&adifmode=0", Uri.EscapeDataString(callsign)));
            }
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
