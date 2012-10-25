namespace UI
{
    partial class LogonForm
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
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            this.m_Password = new System.Windows.Forms.TextBox();
            this.m_Username = new System.Windows.Forms.TextBox();
            this.m_Database = new System.Windows.Forms.TextBox();
            this.m_Server = new System.Windows.Forms.TextBox();
            this.m_Connect = new System.Windows.Forms.Button();
            this.m_SerialPort = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.m_DTR = new System.Windows.Forms.CheckBox();
            this.m_RTS = new System.Windows.Forms.CheckBox();
            this.m_RadioModel = new System.Windows.Forms.ComboBox();
            this.m_Speed = new System.Windows.Forms.TextBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(this.m_Password, 1, 3);
            tableLayoutPanel1.Controls.Add(this.m_Username, 1, 2);
            tableLayoutPanel1.Controls.Add(this.m_Database, 1, 1);
            tableLayoutPanel1.Controls.Add(label4, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(this.m_Server, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Controls.Add(this.m_Connect, 1, 8);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(this.m_SerialPort, 1, 4);
            tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 5);
            tableLayoutPanel1.Controls.Add(this.m_RadioModel, 1, 6);
            tableLayoutPanel1.Controls.Add(label6, 0, 6);
            tableLayoutPanel1.Controls.Add(label7, 0, 7);
            tableLayoutPanel1.Controls.Add(this.m_Speed, 1, 7);
            tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            tableLayoutPanel1.Size = new System.Drawing.Size(328, 282);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // m_Password
            // 
            this.m_Password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Password.Location = new System.Drawing.Point(167, 98);
            this.m_Password.Name = "m_Password";
            this.m_Password.PasswordChar = '*';
            this.m_Password.Size = new System.Drawing.Size(158, 20);
            this.m_Password.TabIndex = 7;
            // 
            // m_Username
            // 
            this.m_Username.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Username.Location = new System.Drawing.Point(167, 67);
            this.m_Username.Name = "m_Username";
            this.m_Username.Size = new System.Drawing.Size(158, 20);
            this.m_Username.TabIndex = 5;
            // 
            // m_Database
            // 
            this.m_Database.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Database.Location = new System.Drawing.Point(167, 36);
            this.m_Database.Name = "m_Database";
            this.m_Database.Size = new System.Drawing.Size(158, 20);
            this.m_Database.TabIndex = 3;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(3, 71);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(58, 13);
            label4.TabIndex = 4;
            label4.Text = "Username:";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 13);
            label2.TabIndex = 2;
            label2.Text = "Database:";
            // 
            // m_Server
            // 
            this.m_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Server.Location = new System.Drawing.Point(167, 5);
            this.m_Server.Name = "m_Server";
            this.m_Server.Size = new System.Drawing.Size(158, 20);
            this.m_Server.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 13);
            label1.TabIndex = 0;
            label1.Text = "Server:";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 102);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(56, 13);
            label3.TabIndex = 6;
            label3.Text = "Password:";
            // 
            // m_Connect
            // 
            this.m_Connect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_Connect.Location = new System.Drawing.Point(250, 255);
            this.m_Connect.Name = "m_Connect";
            this.m_Connect.Size = new System.Drawing.Size(75, 19);
            this.m_Connect.TabIndex = 8;
            this.m_Connect.Text = "Connect";
            this.m_Connect.UseVisualStyleBackColor = true;
            this.m_Connect.Click += new System.EventHandler(this.m_Connect_Click);
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(3, 133);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 13);
            label5.TabIndex = 9;
            label5.Text = "C-IV Serial Port:";
            // 
            // m_SerialPort
            // 
            this.m_SerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_SerialPort.FormattingEnabled = true;
            this.m_SerialPort.Location = new System.Drawing.Point(167, 129);
            this.m_SerialPort.Name = "m_SerialPort";
            this.m_SerialPort.Size = new System.Drawing.Size(158, 21);
            this.m_SerialPort.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.m_DTR);
            this.flowLayoutPanel1.Controls.Add(this.m_RTS);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(164, 159);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(109, 23);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // m_DTR
            // 
            this.m_DTR.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_DTR.AutoSize = true;
            this.m_DTR.Location = new System.Drawing.Point(3, 3);
            this.m_DTR.Name = "m_DTR";
            this.m_DTR.Size = new System.Drawing.Size(49, 17);
            this.m_DTR.TabIndex = 0;
            this.m_DTR.Text = "DTR";
            this.m_DTR.UseVisualStyleBackColor = true;
            // 
            // m_RTS
            // 
            this.m_RTS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_RTS.AutoSize = true;
            this.m_RTS.Location = new System.Drawing.Point(58, 3);
            this.m_RTS.Name = "m_RTS";
            this.m_RTS.Size = new System.Drawing.Size(48, 17);
            this.m_RTS.TabIndex = 1;
            this.m_RTS.Text = "RTS";
            this.m_RTS.UseVisualStyleBackColor = true;
            // 
            // m_RadioModel
            // 
            this.m_RadioModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_RadioModel.FormattingEnabled = true;
            this.m_RadioModel.Location = new System.Drawing.Point(167, 191);
            this.m_RadioModel.Name = "m_RadioModel";
            this.m_RadioModel.Size = new System.Drawing.Size(158, 21);
            this.m_RadioModel.TabIndex = 12;
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(3, 195);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(61, 13);
            label6.TabIndex = 13;
            label6.Text = "Radio type:";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(3, 226);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(68, 13);
            label7.TabIndex = 14;
            label7.Text = "Serial speed:";
            // 
            // m_Speed
            // 
            this.m_Speed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Speed.Location = new System.Drawing.Point(167, 222);
            this.m_Speed.Name = "m_Speed";
            this.m_Speed.Size = new System.Drawing.Size(158, 20);
            this.m_Speed.TabIndex = 15;
            // 
            // LogonForm
            // 
            this.AcceptButton = this.m_Connect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 306);
            this.Controls.Add(tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to database";
            this.Load += new System.EventHandler(this.LogonForm_Load);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_Password;
        private System.Windows.Forms.TextBox m_Username;
        private System.Windows.Forms.TextBox m_Database;
        private System.Windows.Forms.TextBox m_Server;
        private System.Windows.Forms.Button m_Connect;
        private System.Windows.Forms.ComboBox m_SerialPort;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox m_DTR;
        private System.Windows.Forms.CheckBox m_RTS;
        private System.Windows.Forms.ComboBox m_RadioModel;
        private System.Windows.Forms.TextBox m_Speed;
    }
}