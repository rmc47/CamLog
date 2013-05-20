namespace QslManager
{
    partial class ClubLogImportForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_Username = new System.Windows.Forms.TextBox();
            this.m_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_Callsign = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // m_Username
            // 
            this.m_Username.Location = new System.Drawing.Point(99, 6);
            this.m_Username.Name = "m_Username";
            this.m_Username.Size = new System.Drawing.Size(177, 20);
            this.m_Username.TabIndex = 1;
            // 
            // m_Password
            // 
            this.m_Password.Location = new System.Drawing.Point(99, 32);
            this.m_Password.Name = "m_Password";
            this.m_Password.PasswordChar = '*';
            this.m_Password.Size = new System.Drawing.Size(177, 20);
            this.m_Password.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // m_Callsign
            // 
            this.m_Callsign.Location = new System.Drawing.Point(99, 58);
            this.m_Callsign.Name = "m_Callsign";
            this.m_Callsign.Size = new System.Drawing.Size(177, 20);
            this.m_Callsign.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Callsign:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Download Log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DownloadLog);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(145, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Download OQRS";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ClubLogImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 117);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_Callsign);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_Username);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClubLogImportForm";
            this.Text = "Import from Club Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_Username;
        private System.Windows.Forms.TextBox m_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_Callsign;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}