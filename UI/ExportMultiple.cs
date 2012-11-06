using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace UI
{
    public partial class ExportMultiple : Form
    {
        private string m_ExportPath;
        private Locator m_SourceLocator;
        private Band m_Band;
        private string m_CallSent;
        private string m_Operators;
        private string m_Contest;
        private string m_FileName;
        private string m_ClaimedScore;
        private bool m_ExportADIF = false;
        private bool m_ExportCabrillo = false;
        private bool m_ExportREG1TEST = false;
        
        public ExportMultiple()
        {
            InitializeComponent();
            checkFields();
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

        public string FileName
        {
            get
            {
                return m_FileName;
            }
            private set
            {
                m_FileName = value;
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

        public bool ExportADIF
        {
            get
            {
                return m_ExportADIF;
            }
            private set
            {
                m_ExportADIF = value;
            }
        }

        public bool ExportCabrillo
        {
            get
            {
                return m_ExportCabrillo;
            }
            private set
            {
                m_ExportCabrillo = value;
            }
        }

        public bool ExportREG1TEST
        {
            get
            {
                return m_ExportREG1TEST;
            }
            private set
            {
                m_ExportREG1TEST = value;
            }
        }

        private void m_BtnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                //sfd.DefaultExt = "log";
                //sfd.Filter = "Cabrillo files (*.log)|*.log|All files (*.*)|*.*";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    ExportPath = fbd.SelectedPath;
                }
            }
            checkFields();
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

        private void m_TxtContest_TextChanged(object sender, EventArgs e)
        {
            Contest = m_TxtContest.Text;
        }

        private void m_TxtFileName_TextChanged(object sender, EventArgs e)
        {
            FileName = m_TxtFileName.Text;
            checkFields();
        }

        private void m_TxtExportPath_TextChanged(object sender, EventArgs e)
        {
            ExportPath = m_TxtExportPath.Text;
            checkFields();
        }

        private void m_ClaimedScore_TextChanged(object sender, EventArgs e)
        {
            ClaimedScore = m_TxtClaimedScore.Text;
        }

        private void m_CmbBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Band = BandHelper.Parse(m_CmbBand.SelectedItem.ToString());
        }

        private void m_ChkAdif_CheckedChanged(object sender, EventArgs e)
        {
            ExportADIF = m_ChkAdif.Checked;
        }

        private void m_ChkCabrillo_CheckedChanged(object sender, EventArgs e)
        {
            ExportCabrillo = m_ChkCabrillo.Checked;
        }

        private void m_ChkREG1TEST_CheckedChanged(object sender, EventArgs e)
        {
            ExportREG1TEST = m_ChkREG1TEST.Checked;
        }

        private void checkFields()
        {
            if ((m_TxtFileName.Text == "")||(m_TxtExportPath.Text==""))
            {
                m_BtnOK.Enabled = false;
            }
            else
            {
                m_BtnOK.Enabled = true;
            }
            if (m_TxtFileName.Text == "")
                label8.ForeColor = System.Drawing.Color.Red;
            else
                label8.ForeColor = System.Drawing.Color.Black;

            if (m_TxtExportPath.Text == "")
                label2.ForeColor = System.Drawing.Color.Red;
            else
                label2.ForeColor = System.Drawing.Color.Black;
        }
    }
}
