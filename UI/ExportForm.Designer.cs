namespace UI
{
    partial class ExportForm
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export band:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Export to:";
            // 
            // m_CmbBand
            // 
            this.m_CmbBand.FormattingEnabled = true;
            this.m_CmbBand.Location = new System.Drawing.Point(85, 6);
            this.m_CmbBand.Name = "m_CmbBand";
            this.m_CmbBand.Size = new System.Drawing.Size(100, 21);
            this.m_CmbBand.TabIndex = 1;
            this.m_CmbBand.SelectedIndexChanged += new System.EventHandler(this.m_CmbBand_SelectedIndexChanged);
            // 
            // m_TxtExportPath
            // 
            this.m_TxtExportPath.Location = new System.Drawing.Point(85, 33);
            this.m_TxtExportPath.Name = "m_TxtExportPath";
            this.m_TxtExportPath.Size = new System.Drawing.Size(265, 20);
            this.m_TxtExportPath.TabIndex = 5;
            // 
            // m_BtnBrowse
            // 
            this.m_BtnBrowse.Location = new System.Drawing.Point(356, 33);
            this.m_BtnBrowse.Name = "m_BtnBrowse";
            this.m_BtnBrowse.Size = new System.Drawing.Size(34, 23);
            this.m_BtnBrowse.TabIndex = 6;
            this.m_BtnBrowse.Text = "...";
            this.m_BtnBrowse.UseVisualStyleBackColor = true;
            this.m_BtnBrowse.Click += new System.EventHandler(this.m_BtnBrowse_Click);
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_BtnOK.Location = new System.Drawing.Point(234, 62);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(75, 23);
            this.m_BtnOK.TabIndex = 7;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            // 
            // m_BtnCancel
            // 
            this.m_BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnCancel.Location = new System.Drawing.Point(315, 62);
            this.m_BtnCancel.Name = "m_BtnCancel";
            this.m_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_BtnCancel.TabIndex = 8;
            this.m_BtnCancel.Text = "Cancel";
            this.m_BtnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Locator:";
            // 
            // m_TxtLocator
            // 
            this.m_TxtLocator.Location = new System.Drawing.Point(298, 6);
            this.m_TxtLocator.Name = "m_TxtLocator";
            this.m_TxtLocator.Size = new System.Drawing.Size(92, 20);
            this.m_TxtLocator.TabIndex = 3;
            this.m_TxtLocator.TextChanged += new System.EventHandler(this.m_TxtLocator_TextChanged);
            // 
            // ExportForm
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_BtnCancel;
            this.ClientSize = new System.Drawing.Size(402, 95);
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
            this.Name = "ExportForm";
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
    }
}