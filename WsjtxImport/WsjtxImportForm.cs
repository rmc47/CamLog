using Engine;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WsjtxImport
{
    public partial class WsjtxImportForm : Form
    {
        private ContactStore ContactStore { get; set; }

        private Timer UploadTimer { get; set; }

        private Dictionary<string, object> ExistingContactCache = new Dictionary<string, object>();

        public WsjtxImportForm()
        {
            InitializeComponent();

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC\CamLog"))
            {
                string station = key.GetValue("Station") as string;
                if (!string.IsNullOrWhiteSpace(station))
                    m_Station.Text = station;

                string logFileLocation = key.GetValue("WsjtxLogFileLocation") as string;
                if (!string.IsNullOrWhiteSpace(logFileLocation))
                    m_LogFileLocation.Text = logFileLocation;
            }

            using (LogonForm lf = new LogonForm())
            {
                var dr = lf.ShowDialog();
                if (dr != DialogResult.OK)
                    Close();

                ContactStore = new ContactStore(lf.Server, lf.Database, lf.Username, lf.Password);
            }

            UploadTimer = new Timer();
            UploadTimer.Interval = 10 * 1000;
            UploadTimer.Tick += UploadTimer_Tick;
            UploadTimer.Enabled = true;
        }

        private void UploadTimer_Tick(object sender, EventArgs e)
        {
            if (Disposing || IsDisposed || !IsHandleCreated)
                return;

            string logFileLocation = m_LogFileLocation.Text;
            if (string.IsNullOrWhiteSpace(logFileLocation))
            {
                m_StatusLabel.Text = "No log file path set";
                return;
            }
            if (!File.Exists(logFileLocation))
            {
                m_StatusLabel.Text = "Log file not found";
                return;
            }

            bool addedAnyQSOs = false;

            AdifFileReader adifReader = AdifFileReader.LoadFromContent(File.ReadAllText(logFileLocation));
            adifReader.ReadHeader();
            AdifFileReader.Record record;
            while ((record = adifReader.ReadRecord()) != null)
            {
                if (CheckExistingContact(record))
                    continue;

                Contact contact = new Contact
                {
                    SourceId = ContactStore.SourceId,
                    Band = BandHelper.Parse(record["band"]),
                    Callsign = record["call"],
                    StartTime = AdifFileReader.ParseAdifDate(record["qso_date"], record["time_on"]).Value,
                    EndTime = AdifFileReader.ParseAdifDate(record["qso_date_off"], record["time_off"]).Value,
                    Frequency = (long)(double.Parse(record["freq"]) * 1000000f),
                    LocatorReceived = new Locator(record["gridsquare"]),
                    Mode = ModeHelper.Parse(record["mode"]),
                    Operator = record["operator"].ToUpperInvariant(),
                    ReportReceived = record["rst_rcvd"],
                    ReportSent = record["rst_sent"],
                    Station = m_Station.Text,
                };

                var previousContacts = ContactStore.GetPreviousContacts(contact.Callsign);
                bool foundContact = false;
                foreach (var c in previousContacts)
                {
                    if (c.Band == contact.Band && c.Mode == contact.Mode && c.Callsign == contact.Callsign && c.SourceId == contact.SourceId &&
                        Math.Abs((c.EndTime - contact.EndTime).TotalSeconds) < 300)
                    {
                        // Already present in the DB
                        foundContact = true;
                        break;
                    }
                }
                if (!foundContact)
                {
                    ContactStore.SaveContact(contact);
                    m_StatusLabel.Text = "Added QSO: " + contact.Callsign;
                    addedAnyQSOs = true;
                }
            }

            if (!addedAnyQSOs)
            {
                m_StatusLabel.Text = "All QSOs uploaded at " + DateTime.Now;
            }
        }

        private bool CheckExistingContact(AdifFileReader.Record record)
        {
            string checkKey = string.Format("{0}-{1}-{2}", record["call"], record["qso_date"], record["time_on"]);
            if (ExistingContactCache.ContainsKey(checkKey))
            {
                return true;
            }
            else
            {
                ExistingContactCache[checkKey] = new object();
                return false;
            }
        }

        private void m_BrowseFileLocation_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.FileName = m_LogFileLocation.Text;
                ofd.CheckFileExists = true;
                ofd.Filter = "ADIF Files (*.adi)|*.adi";
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_LogFileLocation.Text = ofd.FileName;
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC\CamLog"))
                    {
                        key.SetValue("WsjtxLogFileLocation", ofd.FileName);
                    }
                }
            }
        }

        private void m_Station_TextChanged(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC\CamLog"))
            {
                key.SetValue("Station", m_Station.Text);
            }
        }
    }
}
