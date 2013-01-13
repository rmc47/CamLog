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

namespace HrdImport
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
                    csb.Database = Database;
                    csb.UserID = Username;
                    csb.Password = Password;
                    conn.ConnectionString = csb.ConnectionString;
                    conn.Open();
                    conn.Close();

                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC\CamLog"))
                    {
                        key.SetValue("Server", Server);
                        key.SetValue("Database", Database);
                        key.SetValue("Username", Username);
                        key.SetValue("Password", Password);
                        key.SetValue("Station", Station);
                        key.SetValue("ConnString", ConnString);
                    }
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

        public string Station
        {
            get { return m_Station.Text; }
        }

        public string ConnString
        {
            get { return m_ConnString.Text; }
        }

        private void LogonForm_Load(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\M0VFC\CamLog", false))
            {
                if (key != null)
                {
                    m_Server.Text = (string)key.GetValue("Server", "harris.local");
                    m_Database.Text = (string)key.GetValue("Database", "");
                    m_Username.Text = (string)key.GetValue("Username", "gs3pye");
                    m_Password.Text = (string)key.GetValue("Password", "gs3pye");
                    m_Station.Text = (string)key.GetValue("Station", "DATA");
                    m_ConnString.Text = (string)key.GetValue("ConnString", "DSN=HRD My Logbook - Access");
                }
            }
        }
    }
}