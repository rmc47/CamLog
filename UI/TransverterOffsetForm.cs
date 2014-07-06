using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class TransverterOffsetForm : Form
    {
        public TransverterOffsetForm()
        {
            InitializeComponent();
        }

        public double Offset
        {
            get
            {
                double offset;
                double.TryParse(m_Offset.Text, out offset);
                return offset;
            }
            set
            {
                m_Offset.Text = value.ToString();
            }
        }

        private void m_OK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
