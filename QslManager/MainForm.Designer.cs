namespace QslManager
{
    partial class MainForm
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
            this.m_TxtCallsign = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_ContactsGrid = new System.Windows.Forms.DataGridView();
            this.QSL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Callsign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Band = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QSLRX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QSLTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QSLMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_MarkSelectedAsReceived = new System.Windows.Forms.Button();
            this.m_PrintQueuedCards = new System.Windows.Forms.Button();
            this.m_UpdateLabelsUsed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_OutputPath = new System.Windows.Forms.TextBox();
            this.m_OutputPathBrowse = new System.Windows.Forms.Button();
            this.m_QslMethod = new System.Windows.Forms.ComboBox();
            this.m_OurCallsign = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_ChangeDB = new System.Windows.Forms.Button();
            this.m_Layouts = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_DeepSearch = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.m_LabelOffset = new System.Windows.Forms.NumericUpDown();
            this.m_ImportClubLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_ContactsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_LabelOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // m_TxtCallsign
            // 
            this.m_TxtCallsign.Location = new System.Drawing.Point(88, 12);
            this.m_TxtCallsign.MaxLength = 20;
            this.m_TxtCallsign.Name = "m_TxtCallsign";
            this.m_TxtCallsign.Size = new System.Drawing.Size(100, 20);
            this.m_TxtCallsign.TabIndex = 1;
            this.m_TxtCallsign.Text = "HB9/HB0GGG";
            this.m_TxtCallsign.TextChanged += new System.EventHandler(this.m_TxtCallsign_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Callsign:";
            // 
            // m_ContactsGrid
            // 
            this.m_ContactsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ContactsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_ContactsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QSL,
            this.Callsign,
            this.Date,
            this.Band,
            this.Mode,
            this.RST,
            this.QSLRX,
            this.QSLTX,
            this.QSLMethod});
            this.m_ContactsGrid.Location = new System.Drawing.Point(12, 127);
            this.m_ContactsGrid.Name = "m_ContactsGrid";
            this.m_ContactsGrid.Size = new System.Drawing.Size(670, 315);
            this.m_ContactsGrid.TabIndex = 10;
            // 
            // QSL
            // 
            this.QSL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.QSL.HeaderText = "QSL?";
            this.QSL.Name = "QSL";
            this.QSL.Width = 40;
            // 
            // Callsign
            // 
            this.Callsign.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Callsign.HeaderText = "Callsign";
            this.Callsign.Name = "Callsign";
            this.Callsign.ReadOnly = true;
            this.Callsign.Width = 68;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 55;
            // 
            // Band
            // 
            this.Band.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Band.HeaderText = "Band";
            this.Band.Name = "Band";
            this.Band.ReadOnly = true;
            this.Band.Width = 57;
            // 
            // Mode
            // 
            this.Mode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            this.Mode.ReadOnly = true;
            this.Mode.Width = 59;
            // 
            // RST
            // 
            this.RST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RST.HeaderText = "RST";
            this.RST.Name = "RST";
            this.RST.ReadOnly = true;
            this.RST.Width = 54;
            // 
            // QSLRX
            // 
            this.QSLRX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QSLRX.HeaderText = "QSL RX";
            this.QSLRX.Name = "QSLRX";
            this.QSLRX.ReadOnly = true;
            this.QSLRX.Width = 71;
            // 
            // QSLTX
            // 
            this.QSLTX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QSLTX.HeaderText = "QSL TX";
            this.QSLTX.Name = "QSLTX";
            this.QSLTX.ReadOnly = true;
            this.QSLTX.Width = 70;
            // 
            // QSLMethod
            // 
            this.QSLMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QSLMethod.HeaderText = "QSL Method";
            this.QSLMethod.Name = "QSLMethod";
            this.QSLMethod.ReadOnly = true;
            this.QSLMethod.Width = 92;
            // 
            // m_MarkSelectedAsReceived
            // 
            this.m_MarkSelectedAsReceived.Location = new System.Drawing.Point(241, 9);
            this.m_MarkSelectedAsReceived.Name = "m_MarkSelectedAsReceived";
            this.m_MarkSelectedAsReceived.Size = new System.Drawing.Size(199, 23);
            this.m_MarkSelectedAsReceived.TabIndex = 2;
            this.m_MarkSelectedAsReceived.Text = "&Mark selected cards as received";
            this.m_MarkSelectedAsReceived.UseVisualStyleBackColor = true;
            this.m_MarkSelectedAsReceived.Click += new System.EventHandler(this.m_MarkSelectedAsReceived_Click);
            // 
            // m_PrintQueuedCards
            // 
            this.m_PrintQueuedCards.Location = new System.Drawing.Point(446, 9);
            this.m_PrintQueuedCards.Name = "m_PrintQueuedCards";
            this.m_PrintQueuedCards.Size = new System.Drawing.Size(115, 23);
            this.m_PrintQueuedCards.TabIndex = 3;
            this.m_PrintQueuedCards.Text = "&Print queued cards";
            this.m_PrintQueuedCards.UseVisualStyleBackColor = true;
            this.m_PrintQueuedCards.Click += new System.EventHandler(this.m_PrintQueuedCards_Click);
            // 
            // m_UpdateLabelsUsed
            // 
            this.m_UpdateLabelsUsed.Location = new System.Drawing.Point(567, 63);
            this.m_UpdateLabelsUsed.Name = "m_UpdateLabelsUsed";
            this.m_UpdateLabelsUsed.Size = new System.Drawing.Size(115, 23);
            this.m_UpdateLabelsUsed.TabIndex = 4;
            this.m_UpdateLabelsUsed.Text = "&Labels used: 0";
            this.m_UpdateLabelsUsed.UseVisualStyleBackColor = true;
            this.m_UpdateLabelsUsed.Click += new System.EventHandler(this.m_UpdateLabelsUsed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "&QSL Method:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "&Output folder:";
            // 
            // m_OutputPath
            // 
            this.m_OutputPath.Location = new System.Drawing.Point(321, 38);
            this.m_OutputPath.MaxLength = 20;
            this.m_OutputPath.Name = "m_OutputPath";
            this.m_OutputPath.Size = new System.Drawing.Size(325, 20);
            this.m_OutputPath.TabIndex = 8;
            this.m_OutputPath.Text = "C:\\Temp\\test";
            // 
            // m_OutputPathBrowse
            // 
            this.m_OutputPathBrowse.Location = new System.Drawing.Point(652, 36);
            this.m_OutputPathBrowse.Name = "m_OutputPathBrowse";
            this.m_OutputPathBrowse.Size = new System.Drawing.Size(30, 23);
            this.m_OutputPathBrowse.TabIndex = 9;
            this.m_OutputPathBrowse.Text = "...";
            this.m_OutputPathBrowse.UseVisualStyleBackColor = true;
            this.m_OutputPathBrowse.Click += new System.EventHandler(this.m_OutputPathBrowse_Click);
            // 
            // m_QslMethod
            // 
            this.m_QslMethod.FormattingEnabled = true;
            this.m_QslMethod.Items.AddRange(new object[] {
            "Bureau",
            "Direct"});
            this.m_QslMethod.Location = new System.Drawing.Point(88, 38);
            this.m_QslMethod.Name = "m_QslMethod";
            this.m_QslMethod.Size = new System.Drawing.Size(100, 21);
            this.m_QslMethod.TabIndex = 11;
            // 
            // m_OurCallsign
            // 
            this.m_OurCallsign.FormattingEnabled = true;
            this.m_OurCallsign.Items.AddRange(new object[] {
            "Bureau",
            "Direct"});
            this.m_OurCallsign.Location = new System.Drawing.Point(88, 65);
            this.m_OurCallsign.Name = "m_OurCallsign";
            this.m_OurCallsign.Size = new System.Drawing.Size(100, 21);
            this.m_OurCallsign.TabIndex = 13;
            this.m_OurCallsign.SelectedIndexChanged += new System.EventHandler(this.m_OurCallsign_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "&Our Callsign:";
            // 
            // m_ChangeDB
            // 
            this.m_ChangeDB.Location = new System.Drawing.Point(567, 9);
            this.m_ChangeDB.Name = "m_ChangeDB";
            this.m_ChangeDB.Size = new System.Drawing.Size(115, 23);
            this.m_ChangeDB.TabIndex = 14;
            this.m_ChangeDB.Text = "&Change Database";
            this.m_ChangeDB.UseVisualStyleBackColor = true;
            this.m_ChangeDB.Click += new System.EventHandler(this.m_ChangeDB_Click);
            // 
            // m_Layouts
            // 
            this.m_Layouts.FormattingEnabled = true;
            this.m_Layouts.Items.AddRange(new object[] {
            "Bureau",
            "Direct"});
            this.m_Layouts.Location = new System.Drawing.Point(321, 65);
            this.m_Layouts.Name = "m_Layouts";
            this.m_Layouts.Size = new System.Drawing.Size(119, 21);
            this.m_Layouts.TabIndex = 16;
            this.m_Layouts.SelectedIndexChanged += new System.EventHandler(this.m_Layouts_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "&Label Format:";
            // 
            // m_DeepSearch
            // 
            this.m_DeepSearch.Location = new System.Drawing.Point(446, 63);
            this.m_DeepSearch.Name = "m_DeepSearch";
            this.m_DeepSearch.Size = new System.Drawing.Size(115, 23);
            this.m_DeepSearch.TabIndex = 17;
            this.m_DeepSearch.Text = "&Deep Search";
            this.m_DeepSearch.UseVisualStyleBackColor = true;
            this.m_DeepSearch.Click += new System.EventHandler(this.m_DeepSearch_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "L&able offet:";
            // 
            // m_LabelOffset
            // 
            this.m_LabelOffset.Location = new System.Drawing.Point(88, 93);
            this.m_LabelOffset.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.m_LabelOffset.Name = "m_LabelOffset";
            this.m_LabelOffset.Size = new System.Drawing.Size(100, 20);
            this.m_LabelOffset.TabIndex = 19;
            // 
            // m_ImportClubLog
            // 
            this.m_ImportClubLog.Location = new System.Drawing.Point(241, 90);
            this.m_ImportClubLog.Name = "m_ImportClubLog";
            this.m_ImportClubLog.Size = new System.Drawing.Size(199, 23);
            this.m_ImportClubLog.TabIndex = 20;
            this.m_ImportClubLog.Text = "Import Club Log...";
            this.m_ImportClubLog.UseVisualStyleBackColor = true;
            this.m_ImportClubLog.Click += new System.EventHandler(this.ImportClubLog);
            // 
            // MainForm
            // 
            this.AcceptButton = this.m_MarkSelectedAsReceived;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 454);
            this.Controls.Add(this.m_ImportClubLog);
            this.Controls.Add(this.m_LabelOffset);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_DeepSearch);
            this.Controls.Add(this.m_Layouts);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_ChangeDB);
            this.Controls.Add(this.m_OurCallsign);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_QslMethod);
            this.Controls.Add(this.m_OutputPathBrowse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_OutputPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_UpdateLabelsUsed);
            this.Controls.Add(this.m_PrintQueuedCards);
            this.Controls.Add(this.m_MarkSelectedAsReceived);
            this.Controls.Add(this.m_ContactsGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_TxtCallsign);
            this.Name = "MainForm";
            this.Text = "CamLog | QSL Manager";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.m_ContactsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_LabelOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_TxtCallsign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView m_ContactsGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn QSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Callsign;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Band;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn RST;
        private System.Windows.Forms.DataGridViewTextBoxColumn QSLRX;
        private System.Windows.Forms.DataGridViewTextBoxColumn QSLTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn QSLMethod;
        private System.Windows.Forms.Button m_MarkSelectedAsReceived;
        private System.Windows.Forms.Button m_PrintQueuedCards;
        private System.Windows.Forms.Button m_UpdateLabelsUsed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_OutputPath;
        private System.Windows.Forms.Button m_OutputPathBrowse;
        private System.Windows.Forms.ComboBox m_QslMethod;
        private System.Windows.Forms.ComboBox m_OurCallsign;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_ChangeDB;
        private System.Windows.Forms.ComboBox m_Layouts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_DeepSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown m_LabelOffset;
        private System.Windows.Forms.Button m_ImportClubLog;

    }
}

