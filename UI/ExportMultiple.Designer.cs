namespace UI
{
    partial class ExportMultiple
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
            this.m_ChkAdif = new System.Windows.Forms.CheckBox();
            this.m_ChkCabrillo = new System.Windows.Forms.CheckBox();
            this.m_ChkREG1TEST = new System.Windows.Forms.CheckBox();
            this.m_TxtLocator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_BtnCancel = new System.Windows.Forms.Button();
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_BtnBrowse = new System.Windows.Forms.Button();
            this.m_TxtExportPath = new System.Windows.Forms.TextBox();
            this.m_CmbBand = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_TxtClaimedScore = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_TxtOperators = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_TxtCallSent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_TxtFileName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_CmbContest = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_ChkAdif
            // 
            this.m_ChkAdif.AutoSize = true;
            this.m_ChkAdif.Location = new System.Drawing.Point(13, 13);
            this.m_ChkAdif.Name = "m_ChkAdif";
            this.m_ChkAdif.Size = new System.Drawing.Size(50, 17);
            this.m_ChkAdif.TabIndex = 0;
            this.m_ChkAdif.Text = "ADIF";
            this.m_ChkAdif.UseVisualStyleBackColor = true;
            this.m_ChkAdif.CheckedChanged += new System.EventHandler(this.m_ChkAdif_CheckedChanged);
            // 
            // m_ChkCabrillo
            // 
            this.m_ChkCabrillo.AutoSize = true;
            this.m_ChkCabrillo.Location = new System.Drawing.Point(69, 12);
            this.m_ChkCabrillo.Name = "m_ChkCabrillo";
            this.m_ChkCabrillo.Size = new System.Drawing.Size(60, 17);
            this.m_ChkCabrillo.TabIndex = 1;
            this.m_ChkCabrillo.Text = "Cabrillo";
            this.m_ChkCabrillo.UseVisualStyleBackColor = true;
            this.m_ChkCabrillo.CheckedChanged += new System.EventHandler(this.m_ChkCabrillo_CheckedChanged);
            // 
            // m_ChkREG1TEST
            // 
            this.m_ChkREG1TEST.AutoSize = true;
            this.m_ChkREG1TEST.Location = new System.Drawing.Point(135, 13);
            this.m_ChkREG1TEST.Name = "m_ChkREG1TEST";
            this.m_ChkREG1TEST.Size = new System.Drawing.Size(83, 17);
            this.m_ChkREG1TEST.TabIndex = 0;
            this.m_ChkREG1TEST.Text = "REG1TEST";
            this.m_ChkREG1TEST.UseVisualStyleBackColor = true;
            this.m_ChkREG1TEST.CheckedChanged += new System.EventHandler(this.m_ChkREG1TEST_CheckedChanged);
            // 
            // m_TxtLocator
            // 
            this.m_TxtLocator.Location = new System.Drawing.Point(296, 40);
            this.m_TxtLocator.Name = "m_TxtLocator";
            this.m_TxtLocator.Size = new System.Drawing.Size(92, 20);
            this.m_TxtLocator.TabIndex = 12;
            this.m_TxtLocator.TextChanged += new System.EventHandler(this.m_TxtLocator_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Locator:";
            // 
            // m_BtnCancel
            // 
            this.m_BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnCancel.Location = new System.Drawing.Point(313, 178);
            this.m_BtnCancel.Name = "m_BtnCancel";
            this.m_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_BtnCancel.TabIndex = 17;
            this.m_BtnCancel.Text = "Cancel";
            this.m_BtnCancel.UseVisualStyleBackColor = true;
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_BtnOK.Location = new System.Drawing.Point(232, 178);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(75, 23);
            this.m_BtnOK.TabIndex = 16;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            // 
            // m_BtnBrowse
            // 
            this.m_BtnBrowse.Location = new System.Drawing.Point(354, 147);
            this.m_BtnBrowse.Name = "m_BtnBrowse";
            this.m_BtnBrowse.Size = new System.Drawing.Size(34, 23);
            this.m_BtnBrowse.TabIndex = 15;
            this.m_BtnBrowse.Text = "...";
            this.m_BtnBrowse.UseVisualStyleBackColor = true;
            this.m_BtnBrowse.Click += new System.EventHandler(this.m_BtnBrowse_Click);
            // 
            // m_TxtExportPath
            // 
            this.m_TxtExportPath.Location = new System.Drawing.Point(83, 149);
            this.m_TxtExportPath.Name = "m_TxtExportPath";
            this.m_TxtExportPath.Size = new System.Drawing.Size(265, 20);
            this.m_TxtExportPath.TabIndex = 14;
            this.m_TxtExportPath.TextChanged += new System.EventHandler(this.m_TxtExportPath_TextChanged);
            // 
            // m_CmbBand
            // 
            this.m_CmbBand.FormattingEnabled = true;
            this.m_CmbBand.Location = new System.Drawing.Point(83, 40);
            this.m_CmbBand.Name = "m_CmbBand";
            this.m_CmbBand.Size = new System.Drawing.Size(120, 21);
            this.m_CmbBand.TabIndex = 10;
            this.m_CmbBand.SelectedIndexChanged += new System.EventHandler(this.m_CmbBand_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Export to:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Export band:";
            // 
            // m_TxtClaimedScore
            // 
            this.m_TxtClaimedScore.Location = new System.Drawing.Point(296, 94);
            this.m_TxtClaimedScore.Name = "m_TxtClaimedScore";
            this.m_TxtClaimedScore.Size = new System.Drawing.Size(92, 20);
            this.m_TxtClaimedScore.TabIndex = 23;
            this.m_TxtClaimedScore.TextChanged += new System.EventHandler(this.m_ClaimedScore_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Claimed score:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Contest:";
            // 
            // m_TxtOperators
            // 
            this.m_TxtOperators.Location = new System.Drawing.Point(296, 66);
            this.m_TxtOperators.Name = "m_TxtOperators";
            this.m_TxtOperators.Size = new System.Drawing.Size(92, 20);
            this.m_TxtOperators.TabIndex = 19;
            this.m_TxtOperators.TextChanged += new System.EventHandler(this.m_TxtOperators_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(234, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Operators:";
            // 
            // m_TxtCallSent
            // 
            this.m_TxtCallSent.Location = new System.Drawing.Point(83, 67);
            this.m_TxtCallSent.Name = "m_TxtCallSent";
            this.m_TxtCallSent.Size = new System.Drawing.Size(120, 20);
            this.m_TxtCallSent.TabIndex = 18;
            this.m_TxtCallSent.TextChanged += new System.EventHandler(this.m_TxtCallSent_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Call sent:";
            // 
            // m_TxtFileName
            // 
            this.m_TxtFileName.Location = new System.Drawing.Point(83, 120);
            this.m_TxtFileName.Name = "m_TxtFileName";
            this.m_TxtFileName.Size = new System.Drawing.Size(305, 20);
            this.m_TxtFileName.TabIndex = 13;
            this.m_TxtFileName.TextChanged += new System.EventHandler(this.m_TxtFileName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "File Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(211, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 9);
            this.label9.TabIndex = 27;
            this.label9.Text = "(Requires specific band to be chosen)";
            // 
            // m_CmbContest
            // 
            this.m_CmbContest.FormattingEnabled = true;
            this.m_CmbContest.Location = new System.Drawing.Point(83, 94);
            this.m_CmbContest.Name = "m_CmbContest";
            this.m_CmbContest.Size = new System.Drawing.Size(120, 21);
            this.m_CmbContest.TabIndex = 28;
            // 
            // ExportMultiple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 214);
            this.Controls.Add(this.m_CmbContest);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_TxtFileName);
            this.Controls.Add(this.label8);
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
            this.Controls.Add(this.m_ChkREG1TEST);
            this.Controls.Add(this.m_ChkCabrillo);
            this.Controls.Add(this.m_ChkAdif);
            this.Name = "ExportMultiple";
            this.Text = "ExportMultiple";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_ChkAdif;
        private System.Windows.Forms.CheckBox m_ChkCabrillo;
        private System.Windows.Forms.CheckBox m_ChkREG1TEST;
        private System.Windows.Forms.TextBox m_TxtLocator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_BtnCancel;
        private System.Windows.Forms.Button m_BtnOK;
        private System.Windows.Forms.Button m_BtnBrowse;
        private System.Windows.Forms.TextBox m_TxtExportPath;
        private System.Windows.Forms.ComboBox m_CmbBand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_TxtClaimedScore;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_TxtOperators;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_TxtCallSent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_TxtFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox m_CmbContest;
    }
}