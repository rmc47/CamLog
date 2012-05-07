using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Engine;
using System.Threading;
using System.IO;

namespace HrdImport
{
    public partial class StatusForm : Form
    {
        private string m_Server;
        private string m_Username;
        private string m_Database;
        private string m_Password;
        private string m_ConnString;
        private string m_Station;
        private bool m_RunSync;
        private DateTime m_LastQsoSeen = DateTime.MinValue;

        public StatusForm()
        {
            InitializeComponent();
        }

        private void SyncLoop()
        {
            while (m_RunSync)
            {
                if (Disposing || IsDisposed || !IsHandleCreated)
                    return;
                Invoke(new MethodInvoker(() => { 
                    m_StatusLabel.Text = "Running synchronisation...";
                }));
                try
                {
                    if (File.Exists(m_ConnString))
                        RunMixWSync();
                    else
                        RunHrdSync();
                    Invoke(new MethodInvoker(() => m_StatusLabel.Text = "Completed synchronisation"));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(() => m_StatusLabel.Text = "Error: " + ex.Message));
                }
                Thread.Sleep(10000);
            }
        }

        private void RunMixWSync()
        {
            DateTime lastQsoThisRun = m_LastQsoSeen;

            using (ContactStore cs = new ContactStore(m_Server, m_Database, m_Username, m_Password))
            {
                Exception storedEx = null;

                string[] logLines = File.ReadAllLines(m_ConnString);
                foreach (string line in logLines)
                {
                    string[] lineBits = line.Split(';');
                    Contact c = new Contact();
                    long freq = long.Parse(lineBits[6]) / 1000 * 1000;
                    string dateString = lineBits[2];

                    c.Band = BandHelper.FromFrequency(freq);
                    c.Callsign = lineBits[1].Trim().ToUpperInvariant();
                    c.EndTime = c.StartTime = new DateTime(
                        int.Parse(dateString.Substring(0, 4)),
                        int.Parse(dateString.Substring(4, 2)),
                        int.Parse(dateString.Substring(6, 2)),
                        int.Parse(dateString.Substring(8, 2)),
                        int.Parse(dateString.Substring(10, 2)),
                        int.Parse(dateString.Substring(12, 2)));
                    c.Frequency = freq;
                    c.Mode = ModeHelper.Parse(lineBits[9]);
                    c.Operator = m_DefaultOperator.Text;
                    c.ReportReceived = lineBits[10];
                    c.ReportSent = lineBits[11];
                    c.Station = m_Station;

                    // If we've already uploaded something more recent, don't bother hitting the DB...
                    if (m_LastQsoSeen >= c.StartTime)
                    {
                        continue;
                    }
                    if (c.StartTime > lastQsoThisRun)
                        lastQsoThisRun = c.StartTime;

                    Contact previousQso = cs.GetPreviousContacts(c.Callsign).Find(previousContact => previousContact.StartTime == c.StartTime);
                    if (previousQso == null)
                    {
                        cs.SaveContact(c);

                        Invoke(new MethodInvoker(() => m_LastQsoLabel.Text = c.Callsign));
                    }
                }

                m_LastQsoSeen = lastQsoThisRun;

                if (storedEx != null)
                    throw storedEx;
            }
        }

        private void RunHrdSync()
        {
            using (ContactStore cs = new ContactStore(m_Server, m_Database, m_Username, m_Password))
            {
                Exception storedEx = null;

                using (OdbcConnection localConn = new OdbcConnection(m_ConnString))
                {
                    localConn.Open();
                    List<int> keysToMarkUploaded = new List<int>();
                    using (OdbcCommand localCmd = localConn.CreateCommand())
                    {
                        try
                        {
                            localCmd.CommandText = @"SELECT * FROM TABLE_HRD_CONTACTS_V01 WHERE COL_USER_DEFINED_1 IS NULL OR COL_USER_DEFINED_1 <> 'CamLogUploadDone'";
                            using (OdbcDataReader reader = localCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Contact c = new Contact();
                                    c.SourceId = cs.SourceId;
                                    c.Callsign = (string)reader["COL_CALL"];
                                    c.StartTime = (DateTime)reader["COL_TIME_ON"];
                                    c.EndTime = (DateTime)reader["COL_TIME_OFF"];
                                    c.Band = BandHelper.Parse(reader["COL_BAND"] as string);
                                    c.Frequency = GetFrequency(reader.GetInt32(reader.GetOrdinal("COL_FREQ")).ToString());
                                    c.Mode = ModeHelper.Parse(reader["COL_MODE"] as string);
                                    c.Operator = (reader["COL_OPERATOR"] as string) ?? "GS3PYE/P";
                                    c.Station = m_Station;
                                    c.LocatorReceived = new Locator(0, 0);
                                    c.ReportReceived = c.ReportSent = "599";
                                    cs.SaveContact(c);
                                    Invoke(new MethodInvoker(() => m_LastQsoLabel.Text = c.Callsign));
                                    keysToMarkUploaded.Add(reader.GetInt32(reader.GetOrdinal("COL_PRIMARY_KEY")));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            storedEx = ex;
                        }
                    }

                    using (OdbcCommand updateCommand = localConn.CreateCommand())
                    {
                        foreach (int key in keysToMarkUploaded)
                        {
                            updateCommand.CommandText =
                                "UPDATE TABLE_HRD_CONTACTS_V01 SET COL_USER_DEFINED_1='CamLogUploadDone' WHERE COL_PRIMARY_KEY=" + key;
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                    if (storedEx != null)
                        throw storedEx;
                }
            }
        }

        private static int GetFrequency(string frequencyString)
        {
            if (string.IsNullOrEmpty(frequencyString))
                return 0;
            frequencyString = frequencyString.Trim();
            string[] parts = frequencyString.Split('.');
            string freqinhz = string.Join("", parts);
            return int.Parse(freqinhz);
        }

        private void m_StartSync_Click(object sender, EventArgs e)
        {
            m_RunSync = true;
            new Thread(SyncLoop).Start();
        }

        private void m_StopSync_Click(object sender, EventArgs e)
        {
            m_RunSync = false;
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {
            using (LogonForm lf = new LogonForm())
            {
                if (lf.ShowDialog() == DialogResult.OK)
                {
                    m_Server = lf.Server;
                    m_Database = lf.Database;
                    m_Username = lf.Username;
                    m_Password = lf.Password;
                    m_Station = lf.Station;
                    m_ConnString = lf.ConnString;
                }
                else
                    Close();
            }
        }
    }
}
