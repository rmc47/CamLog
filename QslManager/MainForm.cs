using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QslEngine;
using Engine;

namespace QslManager
{
    public partial class MainForm : Form
    {
        public ContactStore m_ContactStore;

        public MainForm()
        {
            InitializeComponent();
            m_ContactStore = new ContactStore("localhost", "arran2011", "root", "g3pyeflossie");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PdfEngine.Test();
        }

        private void m_TxtCallsign_TextChanged(object sender, EventArgs e)
        {
            // If the callsign's long enough, see if we can get any exact matches for it
            if (m_TxtCallsign.TextLength < 3)
                return;

            List<Contact> contacts = m_ContactStore.GetPreviousContacts(m_TxtCallsign.Text);
            m_ContactsGrid.Rows.Clear();

            foreach (Contact c in contacts)
            {
                m_ContactsGrid.Rows.Add(new object[] { 
                    c.QslTxDate == null, 
                    c.Callsign, 
                    c.StartTime, 
                    BandHelper.ToString(c.Band), 
                    ModeHelper.ToString(c.Mode), 
                    c.ReportSent, 
                    c.QslRxDate == null ? "-" : c.QslRxDate.Value.ToShortDateString(), 
                    c.QslTxDate == null ? "-" : c.QslTxDate.Value.ToShortDateString(), 
                    c.QslMethod == null ? "-" : c.QslMethod
                });
            }
        }
    }
}
