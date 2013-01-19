namespace UI
{
    partial class ExportCabrilloForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.m_CmbBand = new System.Windows.Forms.ComboBox();
            this.m_TxtExportPath = new System.Windows.Forms.TextBox();
            this.m_BtnBrowse = new System.Windows.Forms.Button();
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_BtnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_TxtLocator = new System.Windows.Forms.TextBox();
            this.m_TxtCallSent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_TxtOperators = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_TxtClaimedScore = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_CmbContest = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Band:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Export to:";
            // 
            // m_CmbBand
            // 
            this.m_CmbBand.FormattingEnabled = true;
            this.m_CmbBand.Location = new System.Drawing.Point(68, 6);
            this.m_CmbBand.Name = "m_CmbBand";
            this.m_CmbBand.Size = new System.Drawing.Size(155, 21);
            this.m_CmbBand.TabIndex = 1;
            this.m_CmbBand.SelectedIndexChanged += new System.EventHandler(this.m_CmbBand_SelectedIndexChanged);
            // 
            // m_TxtExportPath
            // 
            this.m_TxtExportPath.Location = new System.Drawing.Point(68, 86);
            this.m_TxtExportPath.Name = "m_TxtExportPath";
            this.m_TxtExportPath.Size = new System.Drawing.Size(282, 20);
            this.m_TxtExportPath.TabIndex = 13;
            // 
            // m_BtnBrowse
            // 
            this.m_BtnBrowse.Location = new System.Drawing.Point(356, 86);
            this.m_BtnBrowse.Name = "m_BtnBrowse";
            this.m_BtnBrowse.Size = new System.Drawing.Size(34, 23);
            this.m_BtnBrowse.TabIndex = 15;
            this.m_BtnBrowse.Text = "...";
            this.m_BtnBrowse.UseVisualStyleBackColor = true;
            this.m_BtnBrowse.Click += new System.EventHandler(this.m_BtnBrowse_Click);
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_BtnOK.Location = new System.Drawing.Point(234, 115);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(75, 23);
            this.m_BtnOK.TabIndex = 17;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            // 
            // m_BtnCancel
            // 
            this.m_BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnCancel.Location = new System.Drawing.Point(315, 115);
            this.m_BtnCancel.Name = "m_BtnCancel";
            this.m_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_BtnCancel.TabIndex = 19;
            this.m_BtnCancel.Text = "Cancel";
            this.m_BtnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Locator sent:";
            // 
            // m_TxtLocator
            // 
            this.m_TxtLocator.Location = new System.Drawing.Point(315, 6);
            this.m_TxtLocator.Name = "m_TxtLocator";
            this.m_TxtLocator.Size = new System.Drawing.Size(75, 20);
            this.m_TxtLocator.TabIndex = 3;
            this.m_TxtLocator.TextChanged += new System.EventHandler(this.m_TxtLocator_TextChanged);
            // 
            // m_TxtCallSent
            // 
            this.m_TxtCallSent.Location = new System.Drawing.Point(315, 32);
            this.m_TxtCallSent.Name = "m_TxtCallSent";
            this.m_TxtCallSent.Size = new System.Drawing.Size(75, 20);
            this.m_TxtCallSent.TabIndex = 5;
            this.m_TxtCallSent.TextChanged += new System.EventHandler(this.m_TxtCallSent_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Call sent:";
            // 
            // m_TxtOperators
            // 
            this.m_TxtOperators.Location = new System.Drawing.Point(68, 33);
            this.m_TxtOperators.Name = "m_TxtOperators";
            this.m_TxtOperators.Size = new System.Drawing.Size(155, 20);
            this.m_TxtOperators.TabIndex = 7;
            this.m_TxtOperators.TextChanged += new System.EventHandler(this.m_TxtOperators_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Operators:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Contest:";
            // 
            // m_TxtClaimedScore
            // 
            this.m_TxtClaimedScore.Location = new System.Drawing.Point(315, 60);
            this.m_TxtClaimedScore.Name = "m_TxtClaimedScore";
            this.m_TxtClaimedScore.Size = new System.Drawing.Size(75, 20);
            this.m_TxtClaimedScore.TabIndex = 11;
            this.m_TxtClaimedScore.TextChanged += new System.EventHandler(this.m_ClaimedScore_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Claimed score:";
            // 
            // m_CmbContest
            // 
            this.m_CmbContest.FormattingEnabled = true;
            this.m_CmbContest.Location = new System.Drawing.Point(68, 59);
            this.m_CmbContest.Name = "m_CmbContest";
            this.m_CmbContest.Size = new System.Drawing.Size(155, 21);
            this.m_CmbContest.TabIndex = 20;
            this.m_CmbContest.SelectedIndexChanged += new System.EventHandler(this.m_CmbContest_SelectedIndexChanged);
            // 
            // ExportCabrilloForm
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_BtnCancel;
            this.ClientSize = new System.Drawing.Size(402, 149);
            this.Controls.Add(this.m_CmbContest);
            this.Controls.Add(this.m_TxtClaimedScore);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_TxtOperators);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_TxtCallSent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_TxtLocator);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_BtnCancel);
            this.Controls.Add(this.m_BtnOK);
            this.Controls.Add(this.m_BtnBrowse);
            this.Controls.Add(this.m_TxtExportPath);
            this.Controls.Add(this.m_CmbBand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportCabrilloForm";
            this.Text = "Export Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_CmbBand;
        private System.Windows.Forms.TextBox m_TxtExportPath;
        private System.Windows.Forms.Button m_BtnBrowse;
        private System.Windows.Forms.Button m_BtnOK;
        private System.Windows.Forms.Button m_BtnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_TxtLocator;
        private System.Windows.Forms.TextBox m_TxtCallSent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_TxtOperators;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_TxtClaimedScore;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox m_CmbContest;
    }
}