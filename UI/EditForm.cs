using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace UI
{
    public partial class EditForm : Form
    {
        private Contact m_Contact;
        private Locator m_OurLocator = new Locator("JO01GI");

        public EditForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        public Locator OurLocator
        {
            get { return m_OurLocator; }
            set { m_OurLocator = value; }
        }

        public Contact Contact
        {
            get
            {
                m_Contact.Band = BandHelper.Parse(m_Band.Text);
                m_Contact.Callsign = m_Callsign.Text;
                m_Contact.LocatorReceived = new Locator(m_Locator.Text);
                m_Contact.Notes = m_Comments.Text;
                //m_Contact.Points = int.Parse(m_Distance.Text);
                m_Contact.ReportReceived = m_RstReceived.Text;
                m_Contact.ReportSent = m_RstSent.Text;
                int serialSent, serialReceived;
                if (int.TryParse(m_SerialSent.Text, out serialSent))
                    m_Contact.SerialSent = serialSent;
                if (int.TryParse(m_SerialReceived.Text, out serialReceived))
                    m_Contact.SerialReceived = serialReceived;
#warning Need sep start and end times here
                m_Contact.StartTime = m_Contact.EndTime = DateTime.Parse(m_Time.Text);
                return m_Contact;
            }
            set
            {
                if (value.Id < 0)
                    throw new ArgumentException("Cannot edit contact without ID");
                m_Contact = value;

                m_Band.Text = BandHelper.ToString(value.Band);
                m_Callsign.Text = value.Callsign;
                m_Locator.Text = value.LocatorReceivedString;
                m_RstReceived.Text = value.ReportReceived;
                m_RstSent.Text = value.ReportSent;
                m_SerialSent.Text = value.SerialSent.ToString().PadLeft(3, '0');
                m_SerialReceived.Text = value.SerialReceived.ToString().PadLeft(3, '0');
#warning populate end time when it exists
                m_Time.Text = value.EndTime.ToString();
                m_Comments.Text = value.Notes;
            }
        }

        private bool ValidateContact()
        {
            bool failed = false;

            failed |= ValidationHelper.ValidateTextbox(m_Band, delegate(TextBox tb)
            {
                return (BandHelper.Parse(m_Band.Text) == Band.Unknown);
            });
            failed |= ValidationHelper.ValidateTextbox(m_Callsign, 3);
            if (m_Locator.TextLength > 0)
                failed |= ValidationHelper.ValidateTextbox(m_Locator, ValidationHelper.ValidateLocatorTextbox);
            failed |= ValidationHelper.ValidateTextbox(m_RstSent, 2);
            failed |= ValidationHelper.ValidateTextbox(m_RstReceived, 2);
            failed |= ValidationHelper.ValidateTextbox(m_SerialReceived, ValidationHelper.ValidateSerialTextbox);
            failed |= ValidationHelper.ValidateTextbox(m_SerialSent, ValidationHelper.ValidateSerialTextbox);
            failed |= ValidationHelper.ValidateTextbox(m_Time, delegate(TextBox tb)
            {
                DateTime res;
                return !DateTime.TryParse(tb.Text, out res);
            });

            return !failed;

        }

        private void m_Save_Click(object sender, EventArgs e)
        {
            if (ValidateContact())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void m_Locator_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Locator theirLocator = new Locator(m_Locator.Text);
                m_Beam.Text = Geographics.BeamHeading(OurLocator, theirLocator).ToString();
                int distance = (int)Math.Ceiling(Geographics.GeodesicDistance(OurLocator, theirLocator) / 1000);
                if (distance == 0)
                    distance = 1; // By definition - QSOs in same square = 1 point
                m_Distance.Text = distance.ToString();
            }
            catch (ArgumentException)
            {
                m_Beam.Text = m_Distance.Text = string.Empty;
            }
        }
    }
}