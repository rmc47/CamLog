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
using RigCAT.NET;

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
                    csb.CharacterSet = "utf8mb4";
                    conn.ConnectionString = csb.ConnectionString;
                    conn.Open();
                    conn.Close();

                    Settings.Set("Server", Server);
                    Settings.Set("Database", Database);
                    Settings.Set("Username", Username);
                    Settings.Set("Password", Password);
                    if (CivSerialPort != null)
                        Settings.Set("SerialPort", CivSerialPort);
                    else
                        Settings.Set("SerialPort", string.Empty);
                    Settings.Set("CivDtr", CivDtr.ToString());
                    Settings.Set("CivRts", CivRts.ToString());
                    Settings.Set("CivSpeed", m_Speed.Text);
                    Settings.Set("RadioModel", m_RadioModel.Text);
                    Settings.Set("OurCallsign", m_OurCallsign.Text);
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
                    ContactStore = ContactStore.Create(Server, Database, Username, Password, m_OurCallsign.Text);
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

        public int CivSpeed
        {
            get {
                int speed;
                int.TryParse(m_Speed.Text, out speed);
                return speed;
            }
        }

        public RadioModel? RadioModel
        {
            get
            {
                if (string.IsNullOrEmpty(m_RadioModel.Text))
                    return null;
                return (RadioModel)Enum.Parse(typeof(RadioModel), m_RadioModel.Text, true);
            }
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

            m_RadioModel.Items.Clear();
            foreach (string val in Enum.GetNames(typeof(RadioModel)))
                m_RadioModel.Items.Add(val);

            m_Server.Text = (string)Settings.Get("Server", "harris.local");
            m_Database.Text = (string)Settings.Get("Database", "");
            m_Username.Text = (string)Settings.Get("Username", "gs3pye");
            m_Password.Text = (string)Settings.Get("Password", "gs3pye");
            string savedSerialPort = (string)Settings.Get("SerialPort", "");
            if (savedSerialPort != null && m_SerialPort.Items.Contains(savedSerialPort))
                m_SerialPort.SelectedItem = savedSerialPort;
            bool civDtr, civRts;
            bool.TryParse((string)Settings.Get("CivDtr", "True"), out civDtr);
            bool.TryParse((string)Settings.Get("CivRts", "True"), out civRts);
            m_RTS.Checked = civRts;
            m_DTR.Checked = civDtr;
            m_RadioModel.SelectedText = (string)Settings.Get("RadioModel", string.Empty);
            m_Speed.Text = (string)Settings.Get("CivSpeed", "19200");
            m_OurCallsign.Text = (string)Settings.Get("OurCallsign", "G3PYE/P");
        }

        private void DatabaseDroppedDown(object sender, EventArgs e)
        {
            // When the DB combo is opened, try and populate it with a list of DBs for the server, if the credentials are valid
            m_Database.Items.Add("Foo");
            m_Database.Items.Add("Bar");
        }
    }
}