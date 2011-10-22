using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.IO.Ports;
using Engine;

namespace UI
{
    public partial class LogonForm : Form
    {
        public LogonForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void m_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                // Try making a connection
                using (MySqlConnection conn = new MySqlConnection())
                {
                    MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder();
                    csb.Server = Server;
                    //csb.Database = Database;
                    csb.UserID = Username;
                    csb.Password = Password;
                    conn.ConnectionString = csb.ConnectionString;
                    conn.Open();
                    conn.Close();

                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC Contest Log"))
                    {
                        key.SetValue("Server", Server);
                        key.SetValue("Database", Database);
                        key.SetValue("Username", Username);
                        key.SetValue("Password", Password);
                        if (CivSerialPort != null)
                            key.SetValue("SerialPort", CivSerialPort);
                        else
                            key.SetValue("SerialPort", string.Empty);
                        key.SetValue("CivDtr", CivDtr.ToString());
                        key.SetValue("CivRts", CivRts.ToString());
                    }
                }

                try
                {
                    ContactStore = new ContactStore(Server, Database, Username, Password);
                }
                catch (ContactStore.DatabaseNotFoundException)
                {
                    DialogResult dr = MessageBox.Show(string.Format("Database '{0}' doesn't exist, or you don't have permission to use it. Try and create it as a new log?", Database), "Database not found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.No)
                        return;

                    // OK, so try and create the new DB...
                    ContactStore = ContactStore.Create(Server, Database, Username, Password);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string Server
        {
            get { return m_Server.Text; }
        }

        public string Database
        {
            get { return m_Database.Text; }
        }

        public string Username
        {
            get { return m_Username.Text; }
        }

        public string Password
        {
            get { return m_Password.Text; }
        }

        public ContactStore ContactStore
        {
            get;
            private set;
        }

        public string CivSerialPort
        {
            get { return m_SerialPort.SelectedItem as string; }
        }

        public bool CivDtr
        {
            get { return m_DTR.Checked; }
        }

        public bool CivRts
        {
            get { return m_RTS.Checked; }
        }

        private void LogonForm_Load(object sender, EventArgs e)
        {
            // Grab list of serial ports
            m_SerialPort.Items.Clear();
            foreach (string port in SerialPort.GetPortNames())
                m_SerialPort.Items.Add(port);

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\M0VFC Contest Log", false))
            {
                if (key != null)
                {
                    m_Server.Text = (string)key.GetValue("Server", "harris.local");
                    m_Database.Text = (string)key.GetValue("Database", "");
                    m_Username.Text = (string)key.GetValue("Username", "gs3pye");
                    m_Password.Text = (string)key.GetValue("Password", "gs3pye");
                    string savedSerialPort = (string)key.GetValue("SerialPort", "");
                    if (savedSerialPort != null && m_SerialPort.Items.Contains(savedSerialPort))
                        m_SerialPort.SelectedItem = savedSerialPort;
                    bool civDtr, civRts;
                    bool.TryParse((string)key.GetValue("CivDtr", "True"), out civDtr);
                    bool.TryParse((string)key.GetValue("CivRts", "True"), out civRts);
                    m_RTS.Checked = civRts;
                    m_DTR.Checked = civDtr;
                }
            }
        }
    }
}