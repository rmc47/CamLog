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
    public partial class ExportCabrilloForm : Form
    {
        private string m_ExportPath;
        private Locator m_SourceLocator;
        private Band m_Band;
        private string m_CallSent;
        private string m_Operators;
        private string m_Contest;
        private string m_ClaimedScore;
        
        public ExportCabrilloForm()
        {
            InitializeComponent();
            m_CmbContest.Items.Add("RSGB AFS - Club Calls");
            m_CmbContest.Items.Add("RSGB AFS - 80m SSB/CW");
            m_CmbContest.Items.Add("RSGB AFS - 6m");
            m_CmbContest.Items.Add("RSGB UKAC");
        }

        public string ExportPath
        {
            get { return m_ExportPath; }
            private set
            {
                m_TxtExportPath.Text = value;
                m_ExportPath = value;
            }
        }

        public List<Band> AvailableBands
        {
            set
            {
                m_CmbBand.Items.Clear();
                m_CmbBand.Items.Add("ALL");
                foreach (Band b in value)
                    m_CmbBand.Items.Add(BandHelper.ToString(b));
            }
        }

        public Band Band
        {
            get { return m_Band; }
            private set { m_Band = value; }
        }

        public Locator SourceLocator
        {
            get
            {
                return m_SourceLocator;
            }
            set
            {
                m_SourceLocator = value;
                m_TxtLocator.Text = m_SourceLocator.ToString();
            }
        }


        public string CallSent
        {
            get
            {
                return m_CallSent;
            }
            set
            {
                m_CallSent = value;
                m_TxtCallSent.Text = m_CallSent.ToString();
            }
        }

        public string Operators
        {
            get
            {
                return m_Operators;
            }
            private set
            {
                m_Operators = value;
            }
        }

        public string Contest
        {
            get
            {
                return m_Contest;
            }
            private set
            {
                m_Contest = value;
            }
        }

        public string ClaimedScore
        {
            get
            {
                return m_ClaimedScore;
            }
            private set
            {
                m_ClaimedScore = value;
            }
        }

        private void m_BtnBrowse_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = "log";
                sfd.Filter = "Cabrillo files (*.log)|*.log|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportPath = sfd.FileName;
                }
            }
        }

        private void m_TxtLocator_TextChanged(object sender, EventArgs e)
        {
            Locator l = new Locator(m_TxtLocator.Text);
            if (!l.Equals(Locator.Unknown))
                SourceLocator = l;
        }

        private void m_TxtCallSent_TextChanged(object sender, EventArgs e)
        {
            CallSent = m_TxtCallSent.Text;
        }

        private void m_TxtOperators_TextChanged(object sender, EventArgs e)
        {
            Operators = m_TxtOperators.Text;
        }

        private void m_CmbContest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contest = m_CmbContest.SelectedItem.ToString();
        }

        private void m_ClaimedScore_TextChanged(object sender, EventArgs e)
        {
            ClaimedScore = m_TxtClaimedScore.Text;
        }

        private void m_CmbBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Band = BandHelper.Parse(m_CmbBand.SelectedItem.ToString());
        }
    }
}
