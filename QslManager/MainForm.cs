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
using System.IO;

namespace QslManager
{
    public partial class MainForm : Form
    {
        public ContactStore m_ContactStore;
        private List<Contact> m_VisibleContacts;
        private Dictionary<int, string> m_SourceIdCallsigns;
        private int m_SelectedSource;

        private const int c_SelectedIndex = 0;
        private const int c_QslRxDateIndex = 6;
        private const int c_QslMethodIndex = 8;

        public MainForm()
        {
            InitializeComponent();
            m_ContactStore = new ContactStore("localhost", "harris2010", "root", "");
            m_SourceIdCallsigns = m_ContactStore.GetSources();

            foreach (var kvp in m_SourceIdCallsigns)
                if (kvp.Value == "GM3PYE/P")
                    m_SelectedSource = kvp.Key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedCallsign;
            if (!m_SourceIdCallsigns.TryGetValue(m_SelectedSource, out selectedCallsign))
            {
                MessageBox.Show("No source selected");
                return;
            }

            PdfEngine engine = new PdfEngine(selectedCallsign);
            
            engine.AddQSOs(m_VisibleContacts);

            engine.PrintDocument(@"C:\Temp\test.pdf");
        }

        private void m_TxtCallsign_TextChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            m_ContactsGrid.Rows.Clear();
            
            // If the callsign's long enough, see if we can get any exact matches for it
            if (m_TxtCallsign.TextLength < 3)
            {
                m_VisibleContacts = null;
                return;
            }

            // Get the previous contacts, filtering according to the selected source
            m_VisibleContacts = m_ContactStore.GetPreviousContacts(m_TxtCallsign.Text).FindAll(c => c.SourceId == m_SelectedSource);
            
            foreach (Contact c in m_VisibleContacts)
            {
                m_ContactsGrid.Rows.Add(new object[] { 
                    c.QslRxDate == null, 
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

        private void m_MarkSelectedAsReceived_Click(object sender, EventArgs e)
        {
            if (m_VisibleContacts == null || m_VisibleContacts.Count == 0)
                return;

            for (int row = 0; row < m_VisibleContacts.Count; row++)
            {
                DataGridViewRow gridRow = m_ContactsGrid.Rows[row];
                bool selected = (bool)gridRow.Cells[0].Value;
                if (selected)
                {
                    Contact c = m_VisibleContacts[row];
                    c.QslRxDate = DateTime.UtcNow;
                    c.QslTxDate = null; // Need to mark as "not sent" again, or it won't get reprinted
                    c.QslMethod = m_QslMethod.Text;
                    m_ContactStore.SaveContact(c);
                }
            }
            UpdateGrid();
            m_TxtCallsign.Text = string.Empty;
            m_TxtCallsign.Focus();
        }

        private void m_UpdateLabelsUsed_Click(object sender, EventArgs e)
        {
            PdfEngine engine = new PdfEngine(m_SourceIdCallsigns[m_SelectedSource]);
            List<List<Contact>> contactsToPrint = m_ContactStore.GetContactsToQsl(m_SelectedSource);
            int labelsUsed = 0;
            foreach (List<Contact> contacts in contactsToPrint)
            {
                labelsUsed += engine.CalculateLabelCount(contacts);
            }
            m_UpdateLabelsUsed.Text = string.Format("&Labels used: {0}", labelsUsed);
            m_TxtCallsign.Focus();
        }

        private void m_PrintQueuedCards_Click(object sender, EventArgs e)
        {
            string myCall = m_SourceIdCallsigns[m_SelectedSource];
            PdfEngine engine = new PdfEngine(myCall);
            List<List<Contact>> contactsToPrint = m_ContactStore.GetContactsToQsl(m_SelectedSource);
            if (contactsToPrint.Count == 0)
            {
                MessageBox.Show("No QSOs to print");
            }
            else
            {
                // Sort according to the QSL method we're using
                switch (m_QslMethod.Text)
                {
                    case "Bureau":
                        contactsToPrint.Sort(BureauSorter);
                        break;
                    case "Direct":
                        contactsToPrint.Sort(DirectSorter);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unknown QSL method: " + m_QslMethod.Text);
                }
                foreach (List<Contact> contacts in contactsToPrint)
                {
                    engine.AddQSOs(contacts);
                }
                engine.PrintDocument(Path.Combine(m_OutputPath.Text, string.Format("QSL-{0}-{1}.pdf", myCall.Replace('/', '_'), DateTime.UtcNow.ToString("yyyy-MM-dd-HHmm"))));
                m_ContactStore.MarkQslsSent(contactsToPrint);
            }
            m_TxtCallsign.Focus();
        }

        private static int BureauSorter(List<Contact> contacts1, List<Contact> contacts2)
        {
            if ((contacts1 == null || contacts1.Count == 0) && (contacts2 == null || contacts2.Count == 0))
                return 0;
            if (contacts1 == null || contacts1.Count == 0)
                return 1;
            if (contacts2 == null || contacts2.Count == 0)
                return -1;

            return contacts1[0].Callsign.CompareTo(contacts2[0].Callsign);
        }

        private static int DirectSorter(List<Contact> contacts1, List<Contact> contacts2)
        {
            if ((contacts1 == null || contacts1.Count == 0) && (contacts2 == null || contacts2.Count == 0))
                return 0;
            if (contacts1 == null || contacts1.Count == 0)
                return 1;
            if (contacts2 == null || contacts2.Count == 0)
                return -1;

            if (!contacts1[0].QslRxDate.HasValue)
                return 1;

            return contacts1[0].QslRxDate.Value.CompareTo(contacts2[0].QslRxDate);
        }

        private void m_OutputPathBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult dr = fbd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    m_OutputPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
