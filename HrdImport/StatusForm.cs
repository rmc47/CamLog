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

namespace HrdImport
{
    public partial class StatusForm : Form
    {
        private string m_Server;
        private string m_Username;
        private string m_Database;
        private string m_Password;

        private bool m_RunSync;

        public StatusForm()
        {
            InitializeComponent();
        }

        private void SyncLoop()
        {
            while (m_RunSync)
            {
                Invoke(new MethodInvoker(() => m_StatusLabel.Text = "Running synchronisation..."));
                try
                {
                    RunSync();
                    Invoke(new MethodInvoker(() => m_StatusLabel.Text = "Completed synchronisation"));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(() => m_StatusLabel.Text = "Error: " + ex.Message));
                }
                Thread.Sleep(10000);
            }
        }

        private void RunSync()
        {
            using (ContactStore cs = new ContactStore(m_Server, m_Database, m_Username, m_Password))
            {
                Exception storedEx = null;

                using (OdbcConnection localConn = new OdbcConnection("DSN=HrdLocal"))
                {
                    localConn.Open();
                    List<int> keysToMarkUploaded = new List<int>();
                    using (OdbcCommand localCmd = localConn.CreateCommand())
                    {
                        try
                        {
                            localCmd.CommandText = @"SELECT * FROM TBL_LOGBOOK WHERE Custom1 IS NULL OR Custom1 <> 'HarrisUploadDone'";
                            using (OdbcDataReader reader = localCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Contact c = new Contact();
                                    c.SourceId = cs.SourceId;
                                    c.Callsign = (string)reader["Station"];
                                    c.StartTime = (DateTime)reader["StartTime"];
                                    c.EndTime = (DateTime)reader["EndTime"];
                                    c.Band = BandHelper.Parse(reader["BandMHz"] as string);
                                    c.Frequency = GetFrequency(reader["Frequency"] as string);
                                    c.Mode = ModeHelper.Parse(reader["Mode"] as string);
                                    c.Notes = reader["Remark"] as string;
                                    c.Operator = reader["MyOperator"] as string;
                                    c.Station = "GS3PYE/P";
                                    c.LocatorReceived = new Locator(0, 0);
                                    c.ReportReceived = c.ReportSent = "NA";
                                    cs.SaveContact(c);
                                    Invoke(new MethodInvoker(() => m_LastQsoLabel.Text = c.Callsign));
                                    keysToMarkUploaded.Add(reader.GetInt32(reader.GetOrdinal("PrimaryKey")));
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
                                "UPDATE TBL_LOGBOOK SET Custom1='HarrisUploadDone' WHERE PrimaryKey=" + key;
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
                }
                else
                    Close();
            }
        }
    }
}
