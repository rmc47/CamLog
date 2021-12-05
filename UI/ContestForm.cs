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
using RigCAT.NET;
using System.IO;

namespace UI
{
    public partial class ContestForm : Form
    {
        private static readonly Color c_DupeColor = Color.Tomato;
        private static readonly Color c_WorkedOtherBandsColor = Color.LightGreen;

        private Controller Controller { get; set; }
        private ContactStore m_ContactStore;
        private IRadio m_RadioCAT;
        private double m_TransverterOffsetMHz;
        private CallsignLookup m_CallsignLookup = new CallsignLookup("cty.xml.gz");
        private Locator m_OurLocatorValue = new Locator(Settings.Get("Locator", "JO02ce"));
        private Label[][] m_ContactTableLabels;
        private KeyValuePair<int, int>[] m_ContactIds;
        private QrzServer m_QrzServer = new QrzServer(Settings.Get("QRZUsername", "M0VFC"), Settings.Get("QRZPassword", ""));
        private bool m_LocatorSetManually;
        private bool m_Online = true;
        private string m_lastComment;
        
        public ContestForm()
        {
            Controller = Program.Controller;
            InitializeComponent();

            Controller.ContactStoreChanged += new EventHandler(ContactStoreChanged);
            Controller.CivServerChanged += new EventHandler(CivServerChanged);
        }

        private void CivServerChanged(object sender, EventArgs e)
        {
            if (m_RadioCAT != null)
            {
                m_RadioCAT.FrequencyChanged -= m_CivServer_FrequencyChanged;
            }

            m_RadioCAT = Controller.Radio;
            if (m_RadioCAT != null)
            {
                m_RadioCAT.FrequencyChanged += m_CivServer_FrequencyChanged;
            }
        }

        private void ContactStoreChanged(object sender, EventArgs e)
        {
            m_ContactStore = Controller.ContactStore;

            m_RedrawTimer.Enabled = true;
            m_SerialSent.Text = m_ContactStore.GetSerial(Band.Unknown).ToString().PadLeft(3, '0');
        }

        private void ClearContactRow(bool newSerial)
        {
            m_Callsign.Text = string.Empty;
            m_RstReceived.Text = m_RstSent.Text = ModeHelper.GetDefaultReport(ModeHelper.Parse(m_OurMode.Text));
            m_SerialReceived.Text = string.Empty;
            m_Locator.Text = string.Empty;
            m_Comments.Text = string.Empty;
            m_Notes.Text = string.Empty;
            m_Notes.BackColor = Color.Transparent;

            if (newSerial && m_ContactStore != null) // Can't do this before we connect to the DB
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

        private bool OnlineStatus
        {
            get { return m_Online; }
            set
            {
                if (value == m_Online)
                    return;
                if (value)
                {
                    m_OnlineStatus.Text = "Online";
                    m_OnlineStatus.BackColor = Color.PaleGreen;
                }
                else
                {
                    m_OnlineStatus.Text = "Offline";
                    m_OnlineStatus.BackColor = Color.Pink;
                }
                m_Online = value;
            }
        }

        private Contact Contact
        {
            get
            {
                Contact c = new Contact();
                c.SourceId = m_ContactStore.SourceId;
                c.Band = BandHelper.Parse(m_Band.Text.Trim());
                c.Callsign = m_Callsign.Text.Trim(). ToUpperInvariant();
                if (m_Locator.Text.Trim().Length > 0)
                    c.LocatorReceived = new Locator(m_Locator.Text.Trim().ToUpperInvariant());
                c.Mode = ModeHelper.Parse(m_OurMode.Text);
                c.Frequency = FrequencyHelper.Parse(m_Frequency.Text);
                c.Notes = m_Comments.Text;
                c.Operator = m_OurOperator.Text.ToUpperInvariant ().Trim();
                //c.Points = int.Parse(m_Distance.Text); // TODO: Points calculation
                c.ReportReceived = m_RstReceived.Text.Trim();
                c.ReportSent = m_RstSent.Text.Trim();
                if (m_SerialReceived.Text.Trim().Length > 0)
                    c.SerialReceived = int.Parse(m_SerialReceived.Text.Trim());
                c.SerialSent = int.Parse(m_SerialSent.Text.Trim());
                c.StartTime = c.EndTime = DateTime.Now.ToUniversalTime();
                c.Frequency = FrequencyHelper.Parse(m_Frequency.Text.Trim());
                c.Station = m_Station.Text.Trim();

                c.LocatorReceived = new Locator(m_Locator.Text);
                if (!string.IsNullOrWhiteSpace(m_SatelliteMode.Text))
                    c.SatelliteMode = m_SatelliteMode.Text;
                if (!string.IsNullOrWhiteSpace(m_SatelliteName.Text))
                    c.SatelliteName = m_SatelliteName.Text;
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
                    if (theirLocator.IsValid)
                    {
                        m_Beam.Text = Geographics.BeamHeading(m_OurLocatorValue, theirLocator).ToString();
                        int distance = (int)Math.Ceiling(Geographics.GeodesicDistance(m_OurLocatorValue, theirLocator) / 1000);
                        if (distance == 0)
                            distance = 1; // By definition - QSOs in same square = 1 point
                        m_Distance.Text = distance.ToString();
                    }
                    else
                    {
                        m_Beam.Text = string.Empty;
                        m_Distance.Text = string.Empty;
                    }
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

        private void m_SerialReceived_KeyUp(object sender, KeyEventArgs e)
        {
            if ((m_SerialReceived.Text == "") && ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down)))
                m_SerialReceived.Text = "0";
            if (e.KeyCode == Keys.Up)
                m_SerialReceived.Text = (int.Parse(m_SerialReceived.Text) + 1).ToString().PadLeft(3, '0');
            else if ((e.KeyCode == Keys.Down) && (int.Parse(m_SerialReceived.Text) > 0))
                m_SerialReceived.Text = (int.Parse(m_SerialReceived.Text) - 1).ToString().PadLeft(3,'0');
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

                    Settings.Set("Locator", m_OurLocator.Text);
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
            ClearContactRow(true);

            // Get our station number from the registry
            m_Station.Text = (string)Settings.Get("StationNumber", "1");
            m_OurLocator.Text = (string)Settings.Get("Locator", "JO02BG");
            m_OurOperator.Text = (string)Settings.Get("Operator", "UNKNOWN");
            bool performQRZLookups;
            string performQRZLookupsObj = (string)Settings.Get("PerformQRZLookups", "True");
            bool.TryParse(performQRZLookupsObj, out performQRZLookups);
            m_PerformQRZLookups.Checked = performQRZLookups;
        }

        void m_CivServer_FrequencyChanged(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                long frequencyWithOffset = m_RadioCAT.PrimaryFrequency + (long)(m_TransverterOffsetMHz * 1000000);
                m_Frequency.Text = FrequencyHelper.ToString(frequencyWithOffset);
                m_OurBand.SelectedItem = BandHelper.ToString(BandHelper.FromFrequency(frequencyWithOffset));
                m_OurMode.SelectedItem = ModeHelper.ToString(m_RadioCAT.PrimaryMode);
            }));
        }

        private void m_OurBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Band.Text = m_OurBand.Text;
            if (m_ContactStore != null && OnlineStatus)
            {
                try
                {
                    m_SerialSent.Text = m_ContactStore.GetSerial(BandHelper.Parse(m_Band.Text)).ToString().PadLeft(3, '0');
                }
                catch
                {
                    OnlineStatus = false;
                }
            }
            else
                m_SerialSent.Text = string.Empty;
        }

        private void ContestForm_Shown(object sender, EventArgs e)
        {
            Controller.OpenLog();
        }

        private void CurrentQSOKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Check if the callsign field looks like a number...
                long number;
                if (long.TryParse(m_Callsign.Text, out number))
                {
                    if (number > 1000) // it's probably a frequency
                    {
                        if (m_RadioCAT != null)
                        {
                            //m_RadioCAT.EqualiseVFOs();
                            //m_RadioCAT.SecondaryFrequency = number * 1000;
                        }
                    }
                    ClearContactRow(false);
                }
                else if (OnlineStatus && ValidateContact())
                {
                    try
                    {
                        e.SuppressKeyPress = true;
                        m_ContactStore.SaveContact(Contact);
                        ClearContactRow(true);
                        PopulatePreviousContactsGrid();
                    }
                    catch
                    {
                        OnlineStatus = false;
                    }
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
                ef.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = ef.ShowDialog(this);
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

            string station;
            if (m_OnlyMyQSOs.Checked)
                station = m_Station.Text;
            else
                station = null;
            
            int maxToFetch = m_ContactTable.RowCount - 2;

            string band = m_Band.Text;
            string op = m_OurOperator.Text;

            ThreadPool.QueueUserWorkItem((dummy) =>
            {
                try
                {
                    List<Contact> contacts = m_ContactStore.GetLatestContacts(m_ContactTable.RowCount - 2, station, band, op);

                    if (Disposing || IsDisposed)
                        return;
                    Invoke(new MethodInvoker(() =>
                    {
                        PopulatePreviousContactsGridCallback(contacts);
                        OnlineStatus = true;
                    }));
                }
                catch
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        OnlineStatus = false;
                    }));
                }
            });
        }

        private string BandTextFromContact(Contact c)
        {
            if (!string.IsNullOrWhiteSpace(c.SatelliteName))
                return c.SatelliteName;
            else
                return BandHelper.ToString(c.Band);
        }

        private void PopulatePreviousContactsGridCallback(List<Contact> contacts)
        {
            
            Locator ourLocation = m_OurLocatorValue;
            //m_QSOGrid.Rows.Clear();
            string station = m_Station.Text;
            string band = m_Band.Text;
            string op = m_OurOperator.Text;

            for (int i = 1; i < m_ContactTable.RowCount - 1; i++)
            {
                int contactsIndex = m_ContactTable.RowCount - i-2;
                if (contacts.Count > contactsIndex)
                {
                    Contact c = contacts[contactsIndex];
                    string notesLowerInvariant = c.Notes.ToLowerInvariant();
                    bool alert = (notesLowerInvariant.Contains(station.ToLowerInvariant()) 
                        || notesLowerInvariant.Contains(band.ToLowerInvariant()) 
                        || notesLowerInvariant.Contains(op.ToLowerInvariant()));

                    Label[] rowLabels = m_ContactTableLabels[i - 1];
                    rowLabels[(int)ContactTableColumns.Band].Text = BandTextFromContact(c);

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
                    if (alert)
                        rowLabels[(int)ContactTableColumns.Comments].BackColor = Color.Pink;
                    else
                        rowLabels[(int)ContactTableColumns.Comments].BackColor = SystemColors.Control;

                    rowLabels[(int)ContactTableColumns.LocatorReceived].Text = c.LocatorReceivedString;
                    rowLabels[(int)ContactTableColumns.RstReceived].Text = c.ReportReceived;
                    rowLabels[(int)ContactTableColumns.RstSent].Text = c.ReportSent;
                    rowLabels[(int)ContactTableColumns.SerialReceived].Text = c.SerialReceived.ToString().PadLeft(3, '0');
                    rowLabels[(int)ContactTableColumns.SerialSent].Text = c.SerialSent.ToString().PadLeft(3, '0');
                    rowLabels[(int)ContactTableColumns.Time].Text = c.StartTime.ToString("HHmm");
                    m_ContactIds[i - 1] = new KeyValuePair<int, int>(c.SourceId, c.Id);

                    if (alert)
                    {
                        Array.ForEach(rowLabels, l => { l.ForeColor = Color.Black; l.BackColor = Color.Pink; });
                    }
                    else if (string.Equals(c.Station, m_Station.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Array.ForEach(rowLabels, l => { l.ForeColor = Color.Black; l.BackColor = SystemColors.Control; });
                    }
                    else
                    {
                        Array.ForEach(rowLabels, l => { l.ForeColor = Color.DarkGray; l.BackColor = SystemColors.Control; });
                    }

                    //DataGridViewRow row = new DataGridViewRow();
                    //m_QSOGrid.Rows.Add(row);
                    //row.Cells[(int)ContactTableColumns.Beam].Value = Geographics.BeamHeading(ourLocation, theirLocator).ToString().PadLeft(3, '0');
                    //row.Cells[(int)ContactTableColumns.Distance].Value = ((int)Math.Ceiling(Geographics.GeodesicDistance(ourLocation, theirLocator) / 1000)).ToString();

                    //row.Cells[(int)ContactTableColumns.Callsign].Value = c.Callsign;
                    //row.Cells[(int)ContactTableColumns.Comments].Value = c.Notes;
                    //row.Cells[(int)ContactTableColumns.LocatorReceived].Value = c.LocatorReceivedString;
                    //row.Cells[(int)ContactTableColumns.RstReceived].Value = c.ReportReceived;
                    //row.Cells[(int)ContactTableColumns.RstSent].Value = c.ReportSent;
                    //row.Cells[(int)ContactTableColumns.SerialReceived].Value = c.SerialReceived.ToString().PadLeft(3, '0');
                    //row.Cells[(int)ContactTableColumns.SerialSent].Value = c.SerialSent.ToString().PadLeft(3, '0');
                    //row.Cells[(int)ContactTableColumns.Time].Value = c.StartTime.ToString("HHmm");
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

            ThreadPool.QueueUserWorkItem(_ => CallsignChangedWorker(m_Callsign.Text, m_Band.Text, m_OurLocatorValue));
        }

        private void CallsignChangedWorker(string callsign, string ourBandText, Locator ourLocatorValue)
        {
            try
            {
                string notesText = null;
                Color notesBackColor = Color.Transparent;
                string locatorText = null;
                string beamText = null, distanceText = null, commentsText = null;
                object[] matchesKnownCalls = null, matchesThisContest = null, locatorMatchesThisContest = null;

                Locator existingLocator;
                if (callsign.Length > 2)
                {
                    List<Band> bands = m_ContactStore.GetPreviousBands(callsign, out existingLocator);
                    Band ourBand = BandHelper.Parse(ourBandText);
                    if (bands.Contains(ourBand))
                    {
                        notesBackColor = c_DupeColor;
                        List<Contact> previousQsos = m_ContactStore.GetPreviousContacts(callsign);
                        if (previousQsos.Count > 0)
                        {
                            Contact previousQso = previousQsos[previousQsos.Count - 1];
                            notesText = string.Format("Already worked {0} on {1} (TX: {2} {3:000} / RX: {4} {5:000} on {6} / {7})", callsign, BandHelper.ToString(ourBand), previousQso.ReportSent, previousQso.SerialSent, previousQso.ReportReceived, previousQso.SerialReceived, previousQso.StartTime.ToString("d MMM HH:mm"), previousQso.LocatorReceivedString);
                        }
                        else
                        {
                            notesText = string.Format("Already worked {0} on {1} (Missing QSO details?)", callsign, BandHelper.ToString(ourBand));
                        }
                    }
                    else if (bands.Count > 0)
                    {
                        notesBackColor = c_WorkedOtherBandsColor;
                        string bandString = string.Empty;
                        foreach (Band b in bands)
                        {
                            bandString += BandHelper.ToString(b) + ", ";
                        }
                        notesText = string.Format("Worked {0} on {1} - {2}", callsign, bandString, existingLocator);
                        if (existingLocator != null)
                        {
                            locatorText = existingLocator.ToString();
                            beamText = Geographics.BeamHeading(ourLocatorValue, existingLocator).ToString();
                            distanceText = Math.Ceiling(Geographics.GeodesicDistance(ourLocatorValue, existingLocator) / 1000).ToString();
                        }
                    }
                    else
                    {
                        notesText = string.Empty;
                        notesBackColor = Color.Transparent;
                    }

                    // Do a first guess beam heading etc
                    PrefixRecord pfx = m_CallsignLookup.LookupPrefix(callsign);
                    if (pfx != null)
                    {
                        Locator theirLocator = new Locator(pfx.Latitude, pfx.Longitude);

                        // Only use the DXCC if it's a substantial distance (500km) away from us
                        if (Geographics.GeodesicDistance(ourLocatorValue, theirLocator) > 500 * 1000)
                        {
                            beamText = Geographics.BeamHeading(ourLocatorValue, theirLocator).ToString();
                            distanceText = Math.Ceiling(Geographics.GeodesicDistance(ourLocatorValue, theirLocator) / 1000).ToString();
                        }

                        // Only change comments field if it has not been manually changed
                        if ((m_Comments.Text == m_lastComment) || (m_Comments.Text == ""))
                        {
                            m_lastComment = commentsText = pfx.Entity;
                        }
                    }
                }

                // Populate the lists of partial callsign matches
                if (callsign.Length > 0)
                {
                    matchesKnownCalls = m_ContactStore.GetPartialMatchesKnownCalls(callsign).ToArray();
                    matchesThisContest = m_ContactStore.GetPartialMatchesThisContest(callsign).ToArray();
                }

                // Also search locators if we've got enough digits for it to be sensibly 
                // distinguished from a callsign
                if (callsign.Length > 2)
                {
                    locatorMatchesThisContest = m_ContactStore.GetLocatorMatchesThisContest(callsign).ToArray();
                }

                // Actually populate everything back on the UI thread!
                if (Disposing || IsDisposed || !IsHandleCreated)
                    return;
                Invoke(new MethodInvoker(() =>
                {
                    // If the callsign has been changed while we've been doing this work, don't do the update - another request
                    // will have been kicked off since, and we don't want to trample on the later update
                    if (m_Callsign.Text != callsign)
                        return;

                    if (!string.IsNullOrWhiteSpace(notesText))
                    {
                        m_Notes.Text = notesText;
                        m_Notes.BackColor = notesBackColor;
                    }

                    if (locatorText != null) m_Locator.Text = locatorText;

                    m_Beam.Text = beamText ?? string.Empty;
                    m_Distance.Text = distanceText ?? string.Empty;
                    if (commentsText != null)
                    {
                        m_Comments.Text = commentsText;
                    }


                    if (matchesKnownCalls != null || locatorMatchesThisContest != null)
                    {
                        m_MatchesKnownCalls.BeginUpdate();
                        m_MatchesKnownCalls.Items.Clear();

                        if (matchesKnownCalls != null)
                            m_MatchesKnownCalls.Items.AddRange(matchesKnownCalls);
                        if (locatorMatchesThisContest != null)
                            m_MatchesKnownCalls.Items.AddRange(locatorMatchesThisContest);
                        m_MatchesKnownCalls.EndUpdate();
                    }
                    if (matchesThisContest != null)
                    {
                        m_MatchesThisContest.BeginUpdate();
                        m_MatchesThisContest.Items.Clear();
                        m_MatchesThisContest.Items.AddRange(matchesThisContest);
                        m_MatchesThisContest.EndUpdate();
                    }
                }));
            }
            catch (Exception ex)
            {
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
            Settings.Set("StationNumber", m_Station.Text);
            this.Text = string.Format("{0} - CamLog", m_Station.Text);
        }

        private void ImportKnownCallsigns(object sender, EventArgs e)
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
            Settings.Set("Operator", m_OurOperator.Text);
            if (m_OurOperator.Text.Length > 7)
            {
                MessageBox.Show("Operator callsign longer than expected - are you typing in the right box?");
            }
        }

        private void ExportAdif(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = "adi";
                sfd.Filter = "ADIF files (*.adi)|*.adi|All files (*.*)|*.*"; 
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                AdifHandler.ExportContacts(m_ContactStore.GetAllContacts(null), sfd.FileName);
                MessageBox.Show("Export complete!");
            }
        }

        private void ExportCabrillo(object sender, EventArgs e)
        {
            using (ExportCabrilloForm ef = new ExportCabrilloForm
            {
                CallSent = m_OurOperator.Text,
                SourceLocator = m_OurLocatorValue,
                AvailableBands = m_ContactStore.GetAllBands()
            })
            {
                DialogResult dr = ef.ShowDialog();
                if (dr == DialogResult.OK)
                    CabrilloExporter.ExportContacts(m_ContactStore.GetAllContacts(null), ef.ExportPath, ef.SourceLocator.ToString(), ef.CallSent, ef.Operators, ef.Contest, ef.ClaimedScore);
            }
        }

        private void ExportMultiple(object sender, EventArgs e)
        {
            using (ExportMultiple ef = new ExportMultiple
            {
                CallSent = m_OurOperator.Text,
                SourceLocator = m_OurLocatorValue,
                AvailableBands = m_ContactStore.GetAllBands()
            })
            {
                DialogResult dr = ef.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string exportFileName = ef.ExportPath + "\\" + ef.FileName;
                    if(ef.ExportADIF == true)
                        AdifHandler.ExportContacts(m_ContactStore.GetAllContacts(null), exportFileName + ".adi");
                    if (ef.ExportCabrillo == true)
                        CabrilloExporter.ExportContacts(m_ContactStore.GetAllContacts(null), exportFileName + ".log", ef.SourceLocator.ToString(), ef.CallSent, ef.Operators, ef.Contest, ef.ClaimedScore);
                    if (ef.ExportREG1TEST == true)
                        System.IO.File.WriteAllText(exportFileName + ".txt", m_ContactStore.ExportLog(ef.SourceLocator, ef.Band));
                    MessageBox.Show("Export complete!");
                }
            }
        }



        private void m_Callsign_Leave(object sender, EventArgs e)
        {
            if (m_Callsign.TextLength > 2 && m_PerformQRZLookups.Checked)
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
                                m_Notes.Text = "QRZ.com: Locator " + qrz.Locator + " not overriding manual value - " + qrz.Name;
                            }
                            else
                            {
                                m_Notes.Text = "QRZ.com: Locator used (" + qrz.Locator + ") - " + qrz.Name;
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
                                m_Notes.Text = "QRZ.com: Callsign found, but no locator present. Name: " + qrz.Name;
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

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controller.OpenLog();
        }

        private void WipeQSOClicked(object sender, EventArgs e)
        {
            ClearContactRow(false);
        }

        private void ContestForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12 && e.Modifiers == Keys.None)
            {
                e.SuppressKeyPress = true;
                if (m_OurMode.Text == "CW")
                {
                    try
                    {
                        Controller.CWMacro.SendMacro(e.KeyCode - Keys.F1, new Dictionary<string, string> { 
                        { "HISCALL", m_Callsign.Text },
                        { "MYCALL", "GT3PYE/P" },
                        { "EXCHTX", m_RstSent.Text.Replace("9", "N").Replace("0", "T") }
                    });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending CW macro: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        IVoiceKeyer voiceKeyer = Controller.Radio as IVoiceKeyer;
                        if (voiceKeyer != null)
                        {
                            voiceKeyer.SendDvk(1 + e.KeyCode - Keys.F1); // Assumes KeyCodes for F1-4 are contiguous. Because I am a bad person.
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending DVK macro: " + ex.Message);
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape && e.Modifiers == Keys.None)
            {
                if (m_OurMode.Text == "CW")
                {
                    try
                    {
                        if (Controller.CWMacro.WinKey != null)
                            Controller.CWMacro.WinKey.StopSending();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error stopping CW macro: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        IVoiceKeyer voiceKeyer = Controller.Radio as IVoiceKeyer;
                        if (voiceKeyer != null)
                        {
                            voiceKeyer.CancelDvk();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error stopping DVK: " + ex.Message);
                    }
                }
            }
        }

        private void OnlyMyQSOsClicked(object sender, EventArgs e)
        {
            m_OnlyMyQSOs.Checked = !m_OnlyMyQSOs.Checked;
            PopulatePreviousContactsGrid();
        }

        private void m_PerformQRZLookups_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("PerformQRZLookups", m_PerformQRZLookups.Checked.ToString());
        }

        private void ImportAdif(object sender, EventArgs e)
        {
            try
            {
                string filename;
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "ADIF Files (*.adi)|*.adi|All Files (*.*)|*.*";
                    ofd.CheckFileExists = true;
                    DialogResult dr = ofd.ShowDialog();
                    if (dr != System.Windows.Forms.DialogResult.OK)
                        return;

                    filename = ofd.FileName;
                }
                List<Contact> contacts = AdifHandler.ImportAdif(File.ReadAllText(filename), "IMPORT", m_ContactStore.SourceId, "IMPORT");

                int importedCount = m_ContactStore.Import(contacts);

                MessageBox.Show(string.Format("Import of {0} contacts successful", importedCount), "CamLog | ADIF Import");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Import failed: {0}", ex.Message), "CamLog | ADIF Import");
            }
        }

        private void QrzUserSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (QrzUserSetup QrzSetup = new QrzUserSetup())
            {
                DialogResult dr = QrzSetup.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Settings.Set("QRZUsername", QrzSetup.QrzUsername);
                    Settings.Set("QRZPassword", QrzSetup.QrzPassword);
                    m_QrzServer = new QrzServer(Settings.Get("QRZUsername", ""), Settings.Get("QRZPassword", ""));
                }
            }
        }

        private void SetTransverterOffset(object sender, EventArgs e)
        {
            using (TransverterOffsetForm tvo = new TransverterOffsetForm())
            {
                tvo.Offset = m_TransverterOffsetMHz;
                DialogResult dr = tvo.ShowDialog();
                if (dr != System.Windows.Forms.DialogResult.OK)
                    return;
                m_TransverterOffsetMHz = tvo.Offset;
            }
        }
    }
}