using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Engine;

namespace QslManager
{
    public partial class LogonForm : Form
    {
        private const string c_RegistryRoot = @"SOFTWARE\CamLog";

        public LogonForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void m_Connect_Click(object sender, EventArgs e)
        {
            try
            {
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

                if (ContactStore != null)
                {
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(c_RegistryRoot))
                    {
                        key.SetValue("Server", Server);
                        key.SetValue("Database", Database);
                        key.SetValue("Username", Username);
                        key.SetValue("Password", Password);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
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

        private void LogonForm_Load(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\CamLog", false))
            {
                if (key != null)
                {
                    m_Server.Text = (string)key.GetValue("Server", string.Empty);
                    m_Database.Text = (string)key.GetValue("Database", string.Empty);
                    m_Username.Text = (string)key.GetValue("Username", string.Empty);
                    m_Password.Text = (string)key.GetValue("Password", string.Empty);
                }
            }
        }
    }
}