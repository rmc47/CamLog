using Engine;
using QslEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QslManager
{
    public partial class ClubLogImportForm : Form
    {
        public ContactStore ContactStore { get; set; }

        public ClubLogImportForm(ContactStore contactStore = null)
        {
            ContactStore = contactStore;
            InitializeComponent();

            m_Username.Text = RegistryHelper.GetString(RegistryValue.ClublogUsername, string.Empty);
            m_Password.Text = RegistryHelper.GetString(RegistryValue.ClublogPassword, string.Empty);
        }

        public string Callsign
        {
            get { return m_Callsign.Text; }
            set { m_Callsign.Text = value; }
        }

        private void DownloadLog(object sender, EventArgs e)
        {
            try
            {
                var api = new ClubLogApi();
                api.Login(m_Username.Text, m_Password.Text);
                string log = api.DownloadLog(m_Callsign.Text);

                RegistryHelper.Set(RegistryValue.ClublogUsername, m_Username.Text);
                RegistryHelper.Set(RegistryValue.ClublogPassword, m_Password.Text);

                List<Contact> contacts = AdifHandler.ImportAdif(log, "IMPORT", ContactStore.SourceId, m_Callsign.Text);
                int contactsAdded = ContactStore.Import(contacts);
                MessageBox.Show("Imported " + contactsAdded + " QSOs from Club Log");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error downloading from Clublog: " + ex.Message);
            }
        }
    }
}
