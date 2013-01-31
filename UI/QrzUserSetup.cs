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
    public partial class QrzUserSetup : Form
    {
        private string m_QrzUsername;
        private string m_QrzPassword;

        public QrzUserSetup()
        {
            InitializeComponent();
            m_TxtQrzUsername.Text = Settings.Get("QRZUsername", "");
            m_TxtQrzPassword.Text = Settings.Get("QRZPassword", "");
        }

        public string QrzUsername
        {
            get
            {
                return m_QrzUsername;
            }
            set
            {
                m_QrzUsername = value;
            }
        }

        public string QrzPassword
        {
            get
            {
                return m_QrzPassword;
            }
            private set
            {
                m_QrzPassword = value;
            }
        }

        private void m_TxtQrzUsername_TextChanged(object sender, EventArgs e)
        {
            QrzUsername = m_TxtQrzUsername.Text;
        }

        private void m_TxtQrzPassword_TextChanged(object sender, EventArgs e)
        {
            QrzPassword = m_TxtQrzPassword.Text;
        }
    }
}
