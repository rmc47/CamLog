namespace UI
{
    partial class QrzUserSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_TxtQrzUsername = new System.Windows.Forms.TextBox();
            this.m_TxtQrzPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_TxtQrzUsername
            // 
            this.m_TxtQrzUsername.Location = new System.Drawing.Point(76, 12);
            this.m_TxtQrzUsername.Name = "m_TxtQrzUsername";
            this.m_TxtQrzUsername.Size = new System.Drawing.Size(168, 20);
            this.m_TxtQrzUsername.TabIndex = 0;
            this.m_TxtQrzUsername.TextChanged += new System.EventHandler(this.m_TxtQrzUsername_TextChanged);
            // 
            // m_TxtQrzPassword
            // 
            this.m_TxtQrzPassword.Location = new System.Drawing.Point(76, 38);
            this.m_TxtQrzPassword.Name = "m_TxtQrzPassword";
            this.m_TxtQrzPassword.PasswordChar = '*';
            this.m_TxtQrzPassword.Size = new System.Drawing.Size(168, 20);
            this.m_TxtQrzPassword.TabIndex = 1;
            this.m_TxtQrzPassword.UseSystemPasswordChar = true;
            this.m_TxtQrzPassword.TextChanged += new System.EventHandler(this.m_TxtQrzPassword_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Password:";
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_BtnOK.Location = new System.Drawing.Point(168, 65);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(75, 23);
            this.m_BtnOK.TabIndex = 4;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            // 
            // m_BtnCancel
            // 
            this.m_BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnCancel.Location = new System.Drawing.Point(87, 65);
            this.m_BtnCancel.Name = "m_BtnCancel";
            this.m_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_BtnCancel.TabIndex = 5;
            this.m_BtnCancel.Text = "Cancel";
            this.m_BtnCancel.UseVisualStyleBackColor = true;
            // 
            // QrzUserSetup
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_BtnCancel;
            this.ClientSize = new System.Drawing.Size(256, 100);
            this.Controls.Add(this.m_BtnCancel);
            this.Controls.Add(this.m_BtnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_TxtQrzPassword);
            this.Controls.Add(this.m_TxtQrzUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QrzUserSetup";
            this.Text = "QRZ.com Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_TxtQrzUsername;
        private System.Windows.Forms.TextBox m_TxtQrzPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_BtnOK;
        private System.Windows.Forms.Button m_BtnCancel;
    }
}