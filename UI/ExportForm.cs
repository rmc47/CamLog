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
    public partial class ExportForm : Form
    {
        private string m_ExportPath;
        private Locator m_SourceLocator;
        private Band m_Band;
        
        public ExportForm()
        {
            InitializeComponent();
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

        private void m_BtnBrowse_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Text Files (*.txt)|*.txt";
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

        private void m_CmbBand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Band = BandHelper.Parse(m_CmbBand.SelectedItem.ToString());
        }
    }
}
