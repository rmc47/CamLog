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
    public partial class CustomCabrilloHeaders : Form
    {
        private string m_Headers;
        
        public CustomCabrilloHeaders()
        {
            InitializeComponent();
        }
        
        public string Headers
        {
            get { return m_Headers; }
            set {
                m_Headers = value;
                m_TxtCustomHeaders.Text = m_Headers;
            }
        }



        private void m_CustomHeaders_TextChanged(object sender, EventArgs e)
        {
            Headers = m_TxtCustomHeaders.Text;
        }
    }
}
