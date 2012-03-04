using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Engine;
using Microsoft.Win32;
using System.Threading;

namespace UI
{
    public partial class ContestForm : Form
    {
        private static readonly Color c_DupeColor = Color.Red;
        private static readonly Color c_WorkedOtherBandsColor = Color.Green;

        private ContactStore m_ContactStore;
        private CivServer m_CivServer;
        private CallsignLookup m_CallsignLookup = new CallsignLookup("cty.xml");
        private Locator m_OurLocatorValue = new Locator("JO01GI");
        private Label[][] m_ContactTableLabels;
        private KeyValuePair<int, int>[] m_ContactIds;
        private QrzServer m_QrzServer = new QrzServer("M0VFC", "g3pyeflossie");
        private bool m_LocatorSetManually;
        
        public ContestForm()
        {
            InitializeComponent();
        }

        private void ClearContactRow()
        {
            m_Callsign.Text = string.Empty;
            m_RstReceived.Text = m_RstSent.Text = ModeHelper.GetDefaultReport(ModeHelper.Parse(m_OurMode.Text));
            m_SerialReceived.Text = string.Empty;
            m_Locator.Text = string.Empty;
            m_Comments.Text = string.Empty;

            if (m_ContactStore != null) // Can't do this before we connect to the DB
                m_SerialSent.Text = m_ContactStore.GetSerial(BandHelper.Parse(m_Band.Text)).ToString().PadLeft(3, '0'); // For IOTA just use unknown band

            m_LocatorSetManually = false;
            m_Callsign.Focus();
        }
        /// <summary>
        /// Returns TRUE if SUCCEEDED
        /// </summary>
        /// <returns></returns>
        private bool ValidateContact()
        {
            bool failed = false;

            failed |= ValidationHelper.ValidateTextbox(m_Band, delegate(TextBox tb)
            {
                return (BandHelper.Parse(m_Band.Text) == Band.Unknown);
            });
            failed |= ValidationHelper.ValidateTextbox(m_Callsign, 3);
            failed |= ValidationHelper.ValidateTextbox(m_OurOperator, 3);
            //failed |= ValidationHelper.ValidateTextbox(m_Locator, ValidationHelper.ValidateLocatorTextbox);
            failed |= ValidationHelper.ValidateTextbox(m_OurLocator, ValidationHelper.ValidateLocatorTextbox);
            failed |= ValidationHelper.ValidateTextbox(m_RstSent, 2);
            failed |= ValidationHelper.ValidateTextbox(m_RstReceived, 2);
            if (m_SerialReceived.TextLength > 0)
                failed |= ValidationHelper.ValidateTextbox(m_SerialReceived, ValidationHelper.ValidateSerialTextbox);
            failed |= ValidationHelper.ValidateTextbox(m_SerialSent, ValidationHelper.ValidateSerialTextbox);

            return !failed;

        }

        private Contact Contact
        {
            get
            {
                Contact c = new Contact();
                c.SourceId = m_ContactStore.SourceId;
                c.Band = BandHelper.Parse(m_Band.Text);
                c.Callsign = m_Callsign.Text.ToUpperInvariant ();
                if (m_Locator.TextLength > 0)
                    c.LocatorReceived = new Locator(m_Locator.Text.ToUpperInvariant());
                c.Mode = ModeHelper.Parse(m_OurMode.Text);
                c.Frequency = FrequencyHelper.Parse(m_Frequency.Text);
                c.Notes = m_Comments.Text;
                c.Operator = m_OurOperator.Text.ToUpperInvariant ();
                //c.Points = int.Parse(m_Distance.Text); // TODO: Points calculation
                c.ReportReceived = m_RstReceived.Text;
                c.ReportSent = m_RstSent.Text;
                if (m_SerialReceived.TextLength > 0)
                    c.SerialReceived = int.Parse(m_SerialReceived.Text);
                c.SerialSent = int.Parse(m_SerialSent.Text);
                c.StartTime = c.EndTime = DateTime.Now.ToUniversalTime();
                c.Frequency = FrequencyHelper.Parse(m_Frequency.Text);
                c.Station = m_Station.Text;

                c.LocatorReceived = new Locator(m_Locator.Text);
                //// Construct a rather dodgy locator
                //PrefixRecord pr = m_CallsignLookup.LookupPrefix(m_Callsign.Text);
                //if (pr != null)
                //    c.LocatorReceived = new Locator(pr.Latitude, pr.Longitude);

                return c;
            }
        }

        private void m_Locator_TextChanged(object sender, EventArgs e)
        {
            if (m_Locator.TextLength == 6)
            {
                try
                {
                    Locator theirLocator = new Locator(m_Locator.Text);
                    m_Beam.Text = Geographics.BeamHeading(m_OurLocatorValue, theirLocator).ToString();
                    int distance = (int)Math.Ceiling(Geographics.GeodesicDistance(m_OurLocatorValue, theirLocator) / 1000);
                    if (distance == 0)
                        distance = 1; // By definition - QSOs in same square = 1 point
                    m_Distance.Text = distance.ToString();
                }
                catch (ArgumentException)
                {
                    m_Beam.Text = m_Distance.Text = string.Empty;
                }
            }
            else
            {
                m_Beam.Text = m_Distance.Text = string.Empty;
            }

            if (m_Locator.TextLength >= 4)
            {
                bool isNewSquare = m_ContactStore.IsNewSquare(m_Locator.Text, BandHelper.Parse(m_Band.Text));
                if (isNewSquare)
                    m_Locator.BackColor = Color.LightGreen;
                else
                    m_Locator.BackColor = SystemColors.Window;
            }
            else
            {
                m_Locator.BackColor = SystemColors.Window;
            }
        }


        private void m_Locator_KeyUp(object sender, KeyEventArgs e)
        {
            char kv = (char)e.KeyValue;
            if (char.IsLetterOrDigit(kv) || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                m_LocatorSetManually = m_Locator.TextLength > 0;
                if (m_LocatorSetManually && m_Callsign.TextLength == 0)
                {
                    // No callsign set, but we've entered a locator, do a partial match on that
                    List<string> matches = m_ContactStore.GetLocatorMatchesThisContest(m_Locator.Text);
                    m_MatchesThisContest.Items.Clear();
                    foreach (string match in matches)
                        m_MatchesThisContest.Items.Add(match);
                }
            }
        }

        private void m_OurLocator_TextChanged(object sender, EventArgs e)
        {
            if (m_OurLocator.TextLength == 6)
            {
                try
                {
                    m_OurLocatorValue = new Locator(m_OurLocator.Text);

                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC Contest Log"))
                    {
                        key.SetValue("Locator", m_OurLocator.Text);
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }

        private void m_RedrawTimer_Tick(object sender, EventArgs e)
        {
            m_Time.Text = DateTime.Now.ToUniversalTime().ToString("HHmm");
            PopulatePreviousContactsGrid();
            PopulateFrequenciesBox();
        }

        private void ContestForm_Load(object sender, EventArgs e)
        {
            // Populate the Band / Mode combos
            m_OurBand.Items.Clear();
            foreach (Band b in Enum.GetValues(typeof(Band)))
                m_OurBand.Items.Add(BandHelper.ToString(b));
            m_OurBand.SelectedIndex = 0;

            m_OurMode.Items.Clear();
            foreach (Mode m in Enum.GetValues(typeof(Mode)))
                m_OurMode.Items.Add(ModeHelper.ToString(m));
            m_OurMode.SelectedIndex = 0;

            // Populate the previous contacts table
            InitialisePreviousContactsGrid();

            // Clear stuff out ready for use
            ClearContactRow();

            // Get our station number from the registry
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\M0VFC Contest Log", false))
            {
                if (key != null)
                {
                    m_Station.Text = (string)key.GetValue("StationNumber", "1");
                    m_OurLocator.Text = (string)key.GetValue("Locator", "JO02BG");
                    m_OurOperator.Text = (string)key.GetValue("Operator", "UNKNOWN");
                }
            }
        }

        void m_CivServer_ModeChanged(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                m_OurMode.SelectedItem = ModeHelper.ToString(m_CivServer.Mode);
            }));
        }

        void m_CivServer_FrequencyChanged(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                m_Frequency.Text = FrequencyHelper.ToString(m_CivServer.Frequency);
                m_OurBand.SelectedItem = BandHelper.ToString(BandHelper.FromFrequency(m_CivServer.Frequency));
            }));
        }

        private void m_OurBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Band.Text = m_OurBand.Text;
            if (m_ContactStore != null)
                m_SerialSent.Text = m_ContactStore.GetSerial(BandHelper.Parse(m_Band.Text)).ToString().PadLeft(3, '0');
        }

        private void ContestForm_Shown(object sender, EventArgs e)
        {
            // Get the login details
            using (LogonForm lf = new LogonForm())
            {
                DialogResult dr = lf.ShowDialog(this);
                if (dr != DialogResult.OK)
                    Close();
                else
                {
                    m_ContactStore = new ContactStore(lf.Server, lf.Database, lf.Username, lf.Password);

                    m_RedrawTimer.Enabled = true;
                    m_SerialSent.Text = m_ContactStore.GetSerial(Band.Unknown).ToString().PadLeft(3, '0');
                    if (!string.IsNullOrEmpty(lf.CivSerialPort))
                    {
                        m_CivServer = new CivServer(lf.CivSerialPort, lf.CivDtr, lf.CivRts);
                        // Hook up to the frequency and mode change events
                        m_CivServer.FrequencyChanged += new EventHandler<EventArgs>(m_CivServer_FrequencyChanged);
                        m_CivServer.ModeChanged += new EventHandler<EventArgs>(m_CivServer_ModeChanged);

                        // Force a frequency query
                        m_CivServer.QueryFrequency();
                    }
                }
            }
        }

        private void ContactControls_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ValidateContact())
                {
                    m_ContactStore.SaveContact(Contact);
                    ClearContactRow();
                    PopulatePreviousContactsGrid();
                }
            }
        }

        private void EditLabel_Click(object sender, EventArgs args)
        {
            LinkLabel l = sender as LinkLabel;
            if (l == null || !(l.Tag is Int32))
                return;

            int row = (int)l.Tag;
            using (EditForm ef = new EditForm())
            {
                ef.Contact = m_ContactStore.LoadContact(m_ContactIds[row - 1].Key, m_ContactIds[row-1].Value);
                DialogResult dr = ef.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_ContactStore.SaveContact(ef.Contact);
                    PopulatePreviousContactsGrid();
                }
            }
        }

        private void InitialisePreviousContactsGrid()
        {
            TableLayoutControlCollection controls = m_ContactTable.Controls;
            m_ContactTableLabels = new Label [m_ContactTable.RowCount - 2][];
            m_ContactIds = new KeyValuePair<int, int>[m_ContactTableLabels.Length];

            for (int i = 1 /* ignore header row */; i < m_ContactTable.RowCount - 1 /* ignore new contact row */; i++)
            {
                m_ContactTableLabels[i - 1] = new Label[11];
                foreach (ContactTableColumns column in Enum.GetValues(typeof(ContactTableColumns)))
                {
                    Label l = new Label ();
                    l.AutoSize = true;
                    l.AutoEllipsis = true;
                    l.Anchor = AnchorStyles.Left;
                    l.Text = string.Empty;
                    m_ContactTableLabels[i -1][(int)column] = l;
                    controls.Add (l, (int)column, i);
                }
                LinkLabel editLabel = new LinkLabel();
                editLabel.Tag = i;
                editLabel.Text = "Edit";
                editLabel.Click += EditLabel_Click;
                controls.Add(editLabel, m_ContactTable.ColumnCount - 1, i);
            }
        }

        private void PopulatePreviousContactsGrid()
        {
            if (m_ContactStore == null)
                return;
            List<Contact> contacts = m_ContactStore.GetLatestContacts(m_ContactTable.RowCount - 2);
            Locator ourLocation = m_OurLocatorValue;
            for (int i = 1; i < m_ContactTable.RowCount - 1; i++)
            {
                int contactsIndex = m_ContactTable.RowCount - i-2;
                if (contacts.Count > contactsIndex)
                {
                    Contact c = contacts[contactsIndex];
                    Label[] rowLabels = m_ContactTableLabels[i - 1];
                    rowLabels[(int)ContactTableColumns.Band].Text = BandHelper.ToString(c.Band);

                    Locator theirLocator;
                    if (c.LocatorReceived != null && !(c.LocatorReceived.Latitude == 0 && c.LocatorReceived.Longitude == 0))
                    {
                        theirLocator = c.LocatorReceived;
                    }
                    else
                    {
                        PrefixRecord pr = m_CallsignLookup.LookupPrefix(c.Callsign);
                        if (pr != null)
                            theirLocator = new Locator(pr.Latitude, pr.Longitude);
                        else
                            theirLocator = new Locator(0, 0);
                    }
                    rowLabels[(int)ContactTableColumns.Beam].Text = Geographics.BeamHeading(ourLocation, theirLocator).ToString().PadLeft(3, '0');
                    rowLabels[(int)ContactTableColumns.Distance].Text = ((int)Math.Ceiling(Geographics.GeodesicDistance(ourLocation, theirLocator) / 1000)).ToString();

                    rowLabels[(int)ContactTableColumns.Callsign].Text = c.Callsign;
                    rowLabels[(int)ContactTableColumns.Comments].Text = c.Notes;
                    rowLabels[(int)ContactTableColumns.LocatorReceived].Text = c.LocatorReceived.ToString().ToUpper();
                    rowLabels[(int)ContactTableColumns.RstReceived].Text = c.ReportReceived;
                    rowLabels[(int)ContactTableColumns.RstSent].Text = c.ReportSent;
                    rowLabels[(int)ContactTableColumns.SerialReceived].Text = c.SerialReceived.ToString().PadLeft(3, '0');
                    rowLabels[(int)ContactTableColumns.SerialSent].Text = c.SerialSent.ToString().PadLeft(3, '0');
                    rowLabels[(int)ContactTableColumns.Time].Text = c.StartTime.ToString("HHmm");
                    m_ContactIds[i - 1] = new KeyValuePair<int, int>(c.SourceId, c.Id);

                }
                else
                {
                    foreach (Label l in m_ContactTableLabels[i - 1])
                        l.Text = string.Empty;
                }
            }
        }

        private void PopulateFrequenciesBox()
        {
            return;
            try
            {
                m_MatchesKnownCalls.Items.Clear();
                foreach (var kvp in m_ContactStore.GetFrequencies())
                    m_MatchesKnownCalls.Items.Add(string.Format("{0} - {1}", kvp.Key, FrequencyHelper.ToString(kvp.Value)));
                m_ContactStore.SetFrequency(m_Station.Text, (int)FrequencyHelper.Parse(m_Frequency.Text));
            }
            catch (Exception)
            { }
        }

        private void m_Callsign_TextChanged(object sender, EventArgs e)
        {
            if (m_ContactStore == null)
                return;


            Locator existingLocator;
            List<Band> bands = m_ContactStore.GetPreviousBands(m_Callsign.Text, out existingLocator);
            Band ourBand = BandHelper.Parse(m_Band.Text);
            if (bands.Contains(ourBand))
            {
                m_Notes.BackColor = c_DupeColor;
                m_Notes.Text = string.Format("Already worked {0} on {1}", m_Callsign.Text, BandHelper.ToString(ourBand));
            }
            else if (bands.Count > 0)
            {
                m_Notes.BackColor = c_WorkedOtherBandsColor;
                string bandString = string.Empty;
                foreach (Band b in bands)
                {
                    bandString += BandHelper.ToString(b) + ", ";
                }
                m_Notes.Text = string.Format("Worked {0} on {1} - {2}", m_Callsign.Text, bandString, existingLocator);
                if (existingLocator != null)
                    m_Locator.Text = existingLocator.ToString();
            }
            else
            {
                m_Notes.Text = string.Empty;
                m_Notes.BackColor = BackColor;
            }

            // Do a first guess beam heading etc
            PrefixRecord pfx = m_CallsignLookup.LookupPrefix(m_Callsign.Text);
            if (pfx != null)
            {
                Locator theirLocator = new Locator(pfx.Latitude, pfx.Longitude);
                m_Beam.Text = Geographics.BeamHeading(m_OurLocatorValue, theirLocator).ToString();
                m_Distance.Text = Math.Ceiling(Geographics.GeodesicDistance(m_OurLocatorValue, theirLocator) / 1000).ToString();
                m_Comments.Text = pfx.Entity;
            }

            // Populate the lists of partial callsign matches
            if (m_Callsign.TextLength > 0)
            {
                m_MatchesKnownCalls.Items.Clear();
                m_MatchesThisContest.Items.Clear();
                m_MatchesKnownCalls.Items.AddRange(m_ContactStore.GetPartialMatchesKnownCalls(m_Callsign.Text).ToArray());
                m_MatchesThisContest.Items.AddRange(m_ContactStore.GetPartialMatchesThisContest(m_Callsign.Text).ToArray());
            }

            // Also search locators if we've got enough digits for it to be sensibly 
            // distinguished from a callsign
            if (m_Callsign.TextLength > 2)
            {
                m_MatchesThisContest.Items.AddRange(m_ContactStore.GetLocatorMatchesThisContest(m_Callsign.Text).ToArray());
            }
        }

        private void m_OurMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_RstSent.Text = m_RstReceived.Text = ModeHelper.GetDefaultReport(ModeHelper.Parse(m_OurMode.Text));
        }

        private void m_Export_Click(object sender, EventArgs e)
        {
            using (ExportForm ef = new ExportForm { 
                SourceLocator = m_OurLocatorValue, 
                AvailableBands=m_ContactStore.GetAllBands() 
            })
            {
                DialogResult dr = ef.ShowDialog();
                if (dr == DialogResult.OK)
                    System.IO.File.WriteAllText(ef.ExportPath, m_ContactStore.ExportLog(ef.SourceLocator, ef.Band));
            }
        }

        private void m_Station_TextChanged(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC Contest Log"))
            {
                key.SetValue("StationNumber", m_Station.Text);
            }
        }

        private void m_ImportCallsigns_Click(object sender, EventArgs e)
        {
            if (m_ContactStore == null)
                return;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    m_ContactStore.ImportKnownCalls(ofd.FileName);
                }
            }
        }

        private void m_OurOperator_TextChanged(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\M0VFC Contest Log"))
            {
                key.SetValue("Operator", m_OurOperator.Text);
            }
        }

        private void m_ExportAdif_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                AdifHandler.ExportContacts(m_ContactStore.GetAllContacts("%"), sfd.FileName);
                MessageBox.Show("Export complete!");
            }
        }

        private void m_ExportCabrillo_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "log";
            sfd.Filter = "Cabrillo files (*.log)|*.log|All files (*.*)|*.*";
            using (sfd)
            {
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                CabrilloExporter.ExportContacts(m_ContactStore.GetAllContacts("1"), sfd.FileName);
                MessageBox.Show("Export complete!");
            } 
        }

        private void m_Callsign_Leave(object sender, EventArgs e)
        {
            if (m_Callsign.TextLength > 2)
            {
                // Fire off a thread to try and grab the locator from qrz.com
                string targetCall = m_Callsign.Text;
                m_Notes.Text = "Beginning fetch from QRZ.com...";
                ThreadPool.QueueUserWorkItem(BeginFetchLocator, targetCall);
            }
        }

        private void BeginFetchLocator(object state)
        {
            try
            {
                QrzEntry qrz = m_QrzServer.LookupCallsign((string)state);
                if (qrz != null && qrz.Locator != null)
                {
                    Invoke(new MethodInvoker(() =>
                        {
                            if (m_LocatorSetManually)
                            {
                                m_Notes.Text = "QRZ.com: Locator found (" + qrz.Locator + ") but not overriding manual value";
                            }
                            else
                            {
                                m_Notes.Text = "QRZ.com: Locator used (" + qrz.Locator + ")";
                                m_Locator.Text = qrz.Locator.ToString();
                            }
                        }));
                }
                else
                {
                    Invoke(new MethodInvoker(() =>
                        {
                            if (qrz == null)
                                m_Notes.Text = "QRZ.com: No callsign found";
                            else
                                m_Notes.Text = "QRZ.com: Callsign found, but no locator present";
                        }));
                }
            }
            catch (Exception ex)
            {
                Invoke(new MethodInvoker(() =>
                    {
                        m_Notes.Text = ex.Message;
                    }));
            }
        }

    }
}