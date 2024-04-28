namespace WsjtxImport
{
    partial class WsjtxImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WsjtxImportForm));
            this.m_LogFileLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_StatusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_BrowseFileLocation = new System.Windows.Forms.Button();
            this.m_Station = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_LogFileLocation
            // 
            this.m_LogFileLocation.Location = new System.Drawing.Point(153, 6);
            this.m_LogFileLocation.Name = "m_LogFileLocation";
            this.m_LogFileLocation.Size = new System.Drawing.Size(225, 20);
            this.m_LogFileLocation.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "WSJT-X ADIF log:";
            // 
            // m_StatusLabel
            // 
            this.m_StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_StatusLabel.Location = new System.Drawing.Point(153, 62);
            this.m_StatusLabel.Name = "m_StatusLabel";
            this.m_StatusLabel.Size = new System.Drawing.Size(256, 68);
            this.m_StatusLabel.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Status:";
            // 
            // m_BrowseFileLocation
            // 
            this.m_BrowseFileLocation.Location = new System.Drawing.Point(385, 6);
            this.m_BrowseFileLocation.Name = "m_BrowseFileLocation";
            this.m_BrowseFileLocation.Size = new System.Drawing.Size(24, 23);
            this.m_BrowseFileLocation.TabIndex = 12;
            this.m_BrowseFileLocation.Text = "...";
            this.m_BrowseFileLocation.UseVisualStyleBackColor = true;
            this.m_BrowseFileLocation.Click += new System.EventHandler(this.m_BrowseFileLocation_Click);
            // 
            // m_Station
            // 
            this.m_Station.Location = new System.Drawing.Point(154, 32);
            this.m_Station.Name = "m_Station";
            this.m_Station.Size = new System.Drawing.Size(255, 20);
            this.m_Station.TabIndex = 14;
            this.m_Station.TextChanged += new System.EventHandler(this.m_Station_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Station:";
            // 
            // WsjtxImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 139);
            this.Controls.Add(this.m_Station);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_BrowseFileLocation);
            this.Controls.Add(this.m_LogFileLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_StatusLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WsjtxImportForm";
            this.Text = "CamLog WSJT-X importer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_LogFileLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_StatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_BrowseFileLocation;
        private System.Windows.Forms.TextBox m_Station;
        private System.Windows.Forms.Label label3;
    }
}