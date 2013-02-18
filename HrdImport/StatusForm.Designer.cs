namespace HrdImport
{
    partial class StatusForm
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
            this.m_StatusLabel = new System.Windows.Forms.Label();
            this.m_LastQsoLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_StartSync = new System.Windows.Forms.Button();
            this.m_StopSync = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_DefaultOperator = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // m_StatusLabel
            // 
            this.m_StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_StatusLabel.Location = new System.Drawing.Point(153, 34);
            this.m_StatusLabel.Name = "m_StatusLabel";
            this.m_StatusLabel.Size = new System.Drawing.Size(256, 68);
            this.m_StatusLabel.TabIndex = 1;
            // 
            // m_LastQsoLabel
            // 
            this.m_LastQsoLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_LastQsoLabel.Location = new System.Drawing.Point(153, 114);
            this.m_LastQsoLabel.Name = "m_LastQsoLabel";
            this.m_LastQsoLabel.Size = new System.Drawing.Size(256, 18);
            this.m_LastQsoLabel.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Last QSO uploaded:";
            // 
            // m_StartSync
            // 
            this.m_StartSync.Location = new System.Drawing.Point(147, 136);
            this.m_StartSync.Name = "m_StartSync";
            this.m_StartSync.Size = new System.Drawing.Size(128, 23);
            this.m_StartSync.TabIndex = 4;
            this.m_StartSync.Text = "Start Synchronisation";
            this.m_StartSync.UseVisualStyleBackColor = true;
            this.m_StartSync.Click += new System.EventHandler(this.m_StartSync_Click);
            // 
            // m_StopSync
            // 
            this.m_StopSync.Location = new System.Drawing.Point(281, 136);
            this.m_StopSync.Name = "m_StopSync";
            this.m_StopSync.Size = new System.Drawing.Size(128, 23);
            this.m_StopSync.TabIndex = 5;
            this.m_StopSync.Text = "Stop Synchronisation";
            this.m_StopSync.UseVisualStyleBackColor = true;
            this.m_StopSync.Click += new System.EventHandler(this.m_StopSync_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "MixW Operator:";
            // 
            // m_DefaultOperator
            // 
            this.m_DefaultOperator.Location = new System.Drawing.Point(153, 6);
            this.m_DefaultOperator.Name = "m_DefaultOperator";
            this.m_DefaultOperator.Size = new System.Drawing.Size(256, 20);
            this.m_DefaultOperator.TabIndex = 7;
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 177);
            this.Controls.Add(this.m_DefaultOperator);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_StopSync);
            this.Controls.Add(this.m_StartSync);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_LastQsoLabel);
            this.Controls.Add(this.m_StatusLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(429, 118);
            this.Name = "StatusForm";
            this.Text = "M0VFC Log HRDSync";
            this.Load += new System.EventHandler(this.StatusForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_StatusLabel;
        private System.Windows.Forms.Label m_LastQsoLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_StartSync;
        private System.Windows.Forms.Button m_StopSync;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_DefaultOperator;
    }
}

