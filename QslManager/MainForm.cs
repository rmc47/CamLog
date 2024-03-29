﻿using System;
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
        private List<SourceCallsign> m_SourceIdCallsigns;
        private SourceCallsign m_SelectedSource;
        private List<string> m_CallsignsInLog;
        private int m_LabelsUsed;

        private IPageLayout m_PageLayout = new LayoutAvery7160();

        private const int c_SelectedIndex = 0;
        private const int c_QslRxDateIndex = 6;
        private const int c_QslMethodIndex = 8;

        public MainForm()
        {
            InitializeComponent();

            m_Layouts.BeginUpdate();
            m_Layouts.Items.Clear();
            m_Layouts.Items.Add(new LayoutAvery7160());
            m_Layouts.Items.Add(new LayoutAvery7162());
            m_Layouts.SelectedIndex = 1;
            m_Layouts.ValueMember = "Name";
            m_Layouts.EndUpdate();
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!ShowLogon())
                Close();
        }


        private void m_ChangeDB_Click(object sender, EventArgs e)
        {
            ShowLogon();
        }

        private bool ShowLogon()
        {
            using (LogonForm lf = new LogonForm())
            {
                DialogResult dr = lf.ShowDialog();
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return false;

                m_ContactStore = lf.ContactStore;
                m_SourceIdCallsigns = m_ContactStore.GetSources();
                m_CallsignsInLog = m_ContactStore.GetAllCallsigns();

                m_OurCallsign.BeginUpdate();
                m_OurCallsign.Items.Clear();
                foreach (SourceCallsign src in m_SourceIdCallsigns)
                {
                    m_OurCallsign.Items.Add(src);
                }
                if (m_OurCallsign.Items.Count > 0)
                    m_OurCallsign.SelectedIndex = 0;
                m_OurCallsign.EndUpdate();

                return true;
            }
        }

        private void m_TxtCallsign_TextChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            UpdateGrid(false);
        }

        private void UpdateGrid(bool deepSearch)
        {
            m_ContactsGrid.Rows.Clear();
            
            // If the callsign's long enough, see if we can get any exact matches for it
            if (m_TxtCallsign.TextLength < 3)
            {
                m_VisibleContacts = null;
                return;
            }

            m_Progress.Visible = true;
            System.Threading.ThreadPool.QueueUserWorkItem(_ => UpdateVisibleContacts(m_TxtCallsign.Text, deepSearch, m_SelectedSource.SourceID));
        }

        private void UpdateVisibleContacts(string callsign, bool deepSearch, int sourceID)
        {
            List<Contact> newVisibleContacts;

            if (!deepSearch)
            {
                // See if we can shortcut the database by checking if the callsign appears in the log at all
                if (!m_CallsignsInLog.Contains(callsign.ToUpperInvariant()))
                    newVisibleContacts = new List<Contact> ();
                else
                    newVisibleContacts = m_ContactStore.GetPreviousContacts(callsign).FindAll(c => c.SourceId == sourceID);
            }
            else
            {
                newVisibleContacts = m_ContactStore.GetApproximateMatches(callsign).FindAll(c => c.SourceId == sourceID);
            }

            BeginInvoke(new MethodInvoker(() => RedrawVisibleContacts(callsign, newVisibleContacts, deepSearch)));
        }

        private void RedrawVisibleContacts(string callsign, List<Contact> newVisibleContacts, bool deepSearch)
        {
            if (Disposing || IsDisposed || !IsHandleCreated)
                return;

            // We pass in callsign here to check the callsign textbox hasn't been changed since we were called, in case lookups complete out of order
            if (m_TxtCallsign.Text != callsign)
                return;

            m_Progress.Visible = false;

            m_VisibleContacts = newVisibleContacts;

            // If they've got loads of QSOs, default to not checked, as most incoming cards only have one QSO on them
            bool checkedByDefault = !deepSearch && m_VisibleContacts.Count < 2;

            foreach (Contact c in m_VisibleContacts)
            {
                m_ContactsGrid.Rows.Add(new object[] { 
                    c.QslRxDate == null && checkedByDefault, 
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
        
        private void m_SelectAllUnsent_Click(object sender, EventArgs e)
        {
            if (m_VisibleContacts == null || m_VisibleContacts.Count == 0)
                return;

            List<Contact> contactsToPrint = new List<Contact>();
            for (int row = 0; row < m_VisibleContacts.Count; row++)
            {
                Contact c = m_VisibleContacts[row];
                if (c.QslRxDate == null)
                {
                    DataGridViewRow gridRow = m_ContactsGrid.Rows[row];
                    gridRow.Cells[0].Value = true;
                }
            }
        }

        private void m_MarkSelectedAsReceived_Click(object sender, EventArgs e)
        {
            if (m_VisibleContacts == null || m_VisibleContacts.Count == 0)
                return;

            PdfEngine engine = new PdfEngine(m_PageLayout, m_SelectedSource.Callsign, 0);
            List<Contact> contactsToPrint = new List<Contact>();
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
                    contactsToPrint.Add(c);
                }
            }
            m_LabelsUsed += engine.CalculateLabelCount(contactsToPrint);
            m_UpdateLabelsUsed.Text = string.Format("&Labels used: {0}", m_LabelsUsed);

            UpdateGrid();
            m_TxtCallsign.Text = string.Empty;
            m_TxtCallsign.Focus();
        }

        private void m_UpdateLabelsUsed_Click(object sender, EventArgs e)
        {
            UpdateLabelsUsed();
            m_TxtCallsign.Focus();
        }

        private void UpdateLabelsUsed()
        {
            PdfEngine engine = new PdfEngine(m_PageLayout, m_SelectedSource.Callsign, 0);
            var contactsToPrint = m_ContactStore.GetContactsToQsl(m_SelectedSource.SourceID, m_QslMethod.Text);
            int labelsUsed = 0;
            foreach (var contacts in contactsToPrint.GroupBy(c => c.Callsign))
            {
                labelsUsed += engine.CalculateLabelCount(contacts.ToList());
            }
            m_LabelsUsed = labelsUsed;
            m_UpdateLabelsUsed.Text = string.Format("&Labels used: {0}", labelsUsed);
        }

        private void m_PrintQueuedCards_Click(object sender, EventArgs e)
        {
            string myCall = m_SelectedSource.Callsign;
            PdfEngine engine = new PdfEngine(m_PageLayout, myCall, (int)m_LabelOffset.Value);
            var contactsToPrint = m_ContactStore.GetContactsToQsl(m_SelectedSource.SourceID, m_QslMethod.Text);
            if (contactsToPrint.Count == 0)
            {
                MessageBox.Show("No QSOs to print");
            }
            else
            {
                var contactsByCallsign = contactsToPrint.GroupBy(c => c.Callsign).Select(g => g.ToList()).ToList();
                // Sort according to the QSL method we're using
                switch (m_QslMethod.Text)
                {
                    case "Bureau":
                        contactsByCallsign.Sort(new BureauSorter(contactsToPrint.Select(c => c.Callsign).Distinct()).Sort);
                        break;
                    case "Direct":
                        contactsByCallsign.Sort(DirectSorter);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unknown QSL method: " + m_QslMethod.Text);
                }

                foreach (List<Contact> contacts in contactsByCallsign)
                {
                    var contactsByLocation = contacts.GroupBy(c => c.LocationID);
                    foreach (var oneLocation in contactsByLocation)
                    {
                        Location l = m_ContactStore.LoadLocation(oneLocation.Key);

                        LabelEntry labelEntry = new LabelEntry
                        {
                            Callsign = contacts[0].Callsign,
                            MyCall = myCall
                        };

                        if (l != null)
                        {
                            labelEntry.Location = l;
                            engine.PrintFooter = true;
                        }
                        else
                        {
                            engine.PrintFooter = false;
                        }

                        engine.AddQSOs(labelEntry, oneLocation.ToList());
                    }
                }
                engine.PrintDocument(Path.Combine(m_OutputPath.Text, string.Format("QSL-{0}-{1}.pdf", myCall.Replace('/', '_'), DateTime.UtcNow.ToString("yyyy-MM-dd-HHmm"))));
                m_ContactStore.MarkQslsSent(contactsToPrint);
                m_LabelsUsed = 0;
            }
            m_TxtCallsign.Focus();
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

            if (contacts1[0].QslRxDate.Equals(contacts2[0].QslRxDate))
            {
                // Find minimum contact IDs, not just first ones
                int minID1 = int.MaxValue, minID2 = int.MaxValue;
                contacts1.ForEach(c => minID1 = Math.Min(minID1, c.Id));
                contacts2.ForEach(c => minID2 = Math.Min(minID2, c.Id));
                return minID1.CompareTo(minID2);
            }
            else
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

        private void m_OurCallsign_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectedSource = m_OurCallsign.SelectedItem as SourceCallsign;

            // Update the contact grid, in case we've already entered a callsign, so it gets populated with the new info for this source
            UpdateGrid();
        }

        private void m_Layouts_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_PageLayout = (IPageLayout)m_Layouts.SelectedItem;
        }

        private void m_DeepSearch_Click(object sender, EventArgs e)
        {
            UpdateGrid(true);
        }

        private void ImportClubLog(object sender, EventArgs e)
        {
            PdfEngine addressLabelEngine = new PdfEngine (new LayoutAvery7160(), m_OurCallsign.Text, (int)m_LabelOffset.Value);

            ClubLogCSVHandler csvHandler = new ClubLogCSVHandler (m_ContactStore, addressLabelEngine);
            ClubLogQslAdifHandler adifHandler = new ClubLogQslAdifHandler(m_ContactStore, addressLabelEngine);
            
            string importPath;
            using (OpenFileDialog ofd = new OpenFileDialog ())
            {
                ofd.Filter = "Club Log OQRS Files (*.csv, *.adi)|*.csv;*.adi";
                DialogResult dr = ofd.ShowDialog();
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;
                importPath = ofd.FileName;
            }

            int labelsToPrint;
            if (importPath.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
                labelsToPrint = csvHandler.ProcessFile(importPath);
            else
                labelsToPrint = adifHandler.ProcessFile(importPath);

            if (labelsToPrint > 0)
                ;// printed via Dymo now // addressLabelEngine.PrintDocument(Path.Combine(m_OutputPath.Text, string.Format("Address-{0}-{1}.pdf", m_OurCallsign.Text.Replace('/', '_'), DateTime.UtcNow.ToString("yyyy-MM-dd-HHmm"))));
            else
                MessageBox.Show("No address labels to print");

            UpdateLabelsUsed();
        }

        private void MagicClubLog(object sender, EventArgs e)
        {
            using (ClubLogImportForm clif = new ClubLogImportForm(m_ContactStore))
            {
                clif.Callsign = m_OurCallsign.Text;
                clif.ShowDialog();
            }
        }
    }
}
