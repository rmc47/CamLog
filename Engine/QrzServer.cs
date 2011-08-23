using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace Engine
{
    public sealed class QrzServer
    {
        private const string c_ServerUrl = "http://www.qrz.com/xml";
        private const string c_Agent = "CamLog1.0";

        private string m_Session;
        private string m_Username;
        private string m_Password;

        private readonly WebClient m_WebClient = new WebClient();

        public QrzServer(string username, string password)
        {
            m_Username = username;
            m_Password = password;
        }

        public void Login()
        {
            QrzDataResult result = new QrzDataResult(string.Format("{0}?username={1}&password={2}&agent={3}", c_ServerUrl, m_Username, m_Password, c_Agent), m_WebClient);
            XmlElement sessionElement = result.GetElement("/qrz:QRZDatabase/qrz:Session/qrz:Key");
            if (sessionElement != null)
            {
                m_Session = sessionElement.InnerText;
                Debug.WriteLine("Session key: " + m_Session);
            }
            else
            {
                XmlElement errorElement = result.GetElement("/qrz:QRZDatabase/qrz:Session/qrz:Error");
                if (errorElement != null)
                    throw new InvalidDataException("Login to QRZ.com failed: " + errorElement.InnerText);
                else
                    throw new InvalidDataException("Login to QRZ.com failed");
            }
        }

        public QrzEntry LookupCallsign(string callsign)
        {
            return LookupCallsign(callsign, true);
        }

        private QrzEntry LookupCallsign(string callsign, bool firstTry)
        {
            if (m_Session == null)
                Login();
            QrzDataResult result = new QrzDataResult(string.Format ("{0}?s={1}&callsign={2}", c_ServerUrl, m_Session, callsign), m_WebClient);
            XmlElement callsignElement = result.GetElement("/qrz:QRZDatabase/qrz:Callsign");
            if (callsignElement != null)
            {
                QrzEntry entry = new QrzEntry();
                entry.Callsign = callsignElement["call"].InnerText;
                if (callsignElement["grid"] != null)
                    entry.Locator = new Locator(callsignElement["grid"].InnerText);
                if (callsignElement["fname"] != null && callsignElement["name"] != null)
                    entry.Name = callsignElement["fname"].InnerText + " " + callsignElement["name"].InnerText;
                return entry;
            }
            else
            {
                XmlElement errorElement = result.GetElement("/qrz:QRZDatabase/qrz:Session/qrz:Error");
                if (errorElement != null)
                {
                    // If our session key has expired, nuke it and try again - once!
                    if (errorElement.InnerText == "Invalid session key" && firstTry)
                    {
                        m_Session = null;
                        return LookupCallsign(callsign, false);
                    }

                    throw new InvalidDataException("QRZ.com lookup failed: " + errorElement.InnerText);
                }
                else
                {
                    throw new InvalidDataException("QRZ.com lookup failed");
                }
            }
        }

        private class QrzDataResult
        {
            private readonly XmlDocument m_Doc;
            private readonly XmlNamespaceManager m_Nsm;

            public QrzDataResult(string url, WebClient webClient)
            {
                m_Doc = ReadDocument(url, webClient);
                m_Nsm = new XmlNamespaceManager(m_Doc.NameTable);
                m_Nsm.AddNamespace("qrz", "http://www.qrz.com");
            }

            public XmlElement GetElement(string xpath)
            {
                return (XmlElement)m_Doc.SelectSingleNode(xpath, m_Nsm);
            }

            private XmlDocument ReadDocument(string url, WebClient wc)
            {
                using (Stream stream = wc.OpenRead(url))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(reader.ReadToEnd());
                        return doc;
                    }
                }
            }
        }
        
    }
}
