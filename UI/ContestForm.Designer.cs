namespace UI
{
    partial class ContestForm
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
            if (m_RadioCAT != null)
            {
                m_RadioCAT.FrequencyChanged -= m_CivServer_FrequencyChanged;
            }

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label21;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label23;
            System.Windows.Forms.Label label24;
            System.Windows.Forms.Label label25;
            System.Windows.Forms.Label label26;
            System.Windows.Forms.Label label27;
            System.Windows.Forms.Label label29;
            System.Windows.Forms.Label label30;
            System.Windows.Forms.Label label31;
            System.Windows.Forms.Label label32;
            this.m_Frequency = new System.Windows.Forms.TextBox();
            this.m_Station = new System.Windows.Forms.TextBox();
            this.m_OurMode = new System.Windows.Forms.ComboBox();
            this.m_OurLocator = new System.Windows.Forms.TextBox();
            this.m_OurBand = new System.Windows.Forms.ComboBox();
            this.m_OurOperator = new System.Windows.Forms.TextBox();
            this.m_MatchesKnownCalls = new System.Windows.Forms.ListBox();
            this.m_MatchesThisContest = new System.Windows.Forms.ListBox();
            this.m_Time = new System.Windows.Forms.TextBox();
            this.m_Band = new System.Windows.Forms.TextBox();
            this.m_Callsign = new System.Windows.Forms.TextBox();
            this.m_RstSent = new System.Windows.Forms.TextBox();
            this.m_SerialSent = new System.Windows.Forms.TextBox();
            this.m_RstReceived = new System.Windows.Forms.TextBox();
            this.m_Locator = new System.Windows.Forms.TextBox();
            this.m_SerialReceived = new System.Windows.Forms.TextBox();
            this.m_Comments = new System.Windows.Forms.TextBox();
            this.m_Distance = new System.Windows.Forms.TextBox();
            this.m_Beam = new System.Windows.Forms.TextBox();
            this.m_Notes = new System.Windows.Forms.Label();
            this.m_ContactTable = new System.Windows.Forms.TableLayoutPanel();
            this.m_RedrawTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDIFToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.knownCallsignsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cabrilloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEG1TESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OnlyMyQSOs = new System.Windows.Forms.ToolStripMenuItem();
            this.m_PerformQRZLookups = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.winKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rigControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QrzUserSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wipeQSOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label21 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            label26 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            label29 = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            label31 = new System.Windows.Forms.Label();
            label32 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            this.m_ContactTable.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(this.m_Frequency);
            groupBox1.Controls.Add(this.m_Station);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(this.m_OurMode);
            groupBox1.Controls.Add(this.m_OurLocator);
            groupBox1.Controls.Add(this.m_OurBand);
            groupBox1.Controls.Add(this.m_OurOperator);
            groupBox1.Location = new System.Drawing.Point(400, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(392, 147);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Operator / Station details";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(21, 83);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(60, 13);
            label7.TabIndex = 13;
            label7.Text = "Frequency:";
            // 
            // m_Frequency
            // 
            this.m_Frequency.Location = new System.Drawing.Point(87, 80);
            this.m_Frequency.Name = "m_Frequency";
            this.m_Frequency.Size = new System.Drawing.Size(100, 20);
            this.m_Frequency.TabIndex = 12;
            this.m_Frequency.TabStop = false;
            this.m_Frequency.Text = "145.500.000";
            // 
            // m_Station
            // 
            this.m_Station.Location = new System.Drawing.Point(294, 27);
            this.m_Station.Name = "m_Station";
            this.m_Station.Size = new System.Drawing.Size(81, 20);
            this.m_Station.TabIndex = 10;
            this.m_Station.TabStop = false;
            this.m_Station.Text = "1";
            this.m_Station.TextChanged += new System.EventHandler(this.m_Station_TextChanged);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(207, 30);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 13);
            label5.TabIndex = 8;
            label5.Text = "Station number:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(207, 56);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(46, 13);
            label4.TabIndex = 7;
            label4.Text = "Locator:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(21, 109);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(37, 13);
            label3.TabIndex = 6;
            label3.Text = "Mode:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(21, 56);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 5;
            label2.Text = "Band:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(21, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(51, 13);
            label1.TabIndex = 4;
            label1.Text = "Operator:";
            // 
            // m_OurMode
            // 
            this.m_OurMode.FormattingEnabled = true;
            this.m_OurMode.Location = new System.Drawing.Point(87, 106);
            this.m_OurMode.Name = "m_OurMode";
            this.m_OurMode.Size = new System.Drawing.Size(100, 21);
            this.m_OurMode.TabIndex = 2;
            this.m_OurMode.TabStop = false;
            this.m_OurMode.Text = "SSB";
            this.m_OurMode.SelectedIndexChanged += new System.EventHandler(this.m_OurMode_SelectedIndexChanged);
            // 
            // m_OurLocator
            // 
            this.m_OurLocator.Location = new System.Drawing.Point(294, 53);
            this.m_OurLocator.Name = "m_OurLocator";
            this.m_OurLocator.Size = new System.Drawing.Size(81, 20);
            this.m_OurLocator.TabIndex = 3;
            this.m_OurLocator.TabStop = false;
            this.m_OurLocator.Text = "JO01GI";
            this.m_OurLocator.TextChanged += new System.EventHandler(this.m_OurLocator_TextChanged);
            // 
            // m_OurBand
            // 
            this.m_OurBand.FormattingEnabled = true;
            this.m_OurBand.Location = new System.Drawing.Point(87, 53);
            this.m_OurBand.Name = "m_OurBand";
            this.m_OurBand.Size = new System.Drawing.Size(100, 21);
            this.m_OurBand.TabIndex = 1;
            this.m_OurBand.TabStop = false;
            this.m_OurBand.Text = "70cm";
            this.m_OurBand.SelectedIndexChanged += new System.EventHandler(this.m_OurBand_SelectedIndexChanged);
            // 
            // m_OurOperator
            // 
            this.m_OurOperator.Location = new System.Drawing.Point(87, 27);
            this.m_OurOperator.Name = "m_OurOperator";
            this.m_OurOperator.Size = new System.Drawing.Size(100, 20);
            this.m_OurOperator.TabIndex = 0;
            this.m_OurOperator.TabStop = false;
            this.m_OurOperator.Text = "M0VFC";
            this.m_OurOperator.TextChanged += new System.EventHandler(this.m_OurOperator_TextChanged);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.m_MatchesKnownCalls);
            groupBox2.Controls.Add(this.m_MatchesThisContest);
            groupBox2.Location = new System.Drawing.Point(12, 27);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(382, 147);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Callsign matches";
            // 
            // m_MatchesKnownCalls
            // 
            this.m_MatchesKnownCalls.FormattingEnabled = true;
            this.m_MatchesKnownCalls.Location = new System.Drawing.Point(202, 27);
            this.m_MatchesKnownCalls.Name = "m_MatchesKnownCalls";
            this.m_MatchesKnownCalls.Size = new System.Drawing.Size(165, 95);
            this.m_MatchesKnownCalls.TabIndex = 1;
            // 
            // m_MatchesThisContest
            // 
            this.m_MatchesThisContest.FormattingEnabled = true;
            this.m_MatchesThisContest.Location = new System.Drawing.Point(13, 27);
            this.m_MatchesThisContest.Name = "m_MatchesThisContest";
            this.m_MatchesThisContest.Size = new System.Drawing.Size(176, 95);
            this.m_MatchesThisContest.TabIndex = 0;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label21.Location = new System.Drawing.Point(0, 0);
            label21.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(36, 13);
            label21.TabIndex = 13;
            label21.Text = "Band";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label22.Location = new System.Drawing.Point(41, 0);
            label22.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(34, 13);
            label22.TabIndex = 14;
            label22.Text = "Time";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label23.Location = new System.Drawing.Point(82, 0);
            label23.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(51, 13);
            label23.TabIndex = 15;
            label23.Text = "Callsign";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label24.Location = new System.Drawing.Point(180, 0);
            label24.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(33, 13);
            label24.TabIndex = 16;
            label24.Text = "Sent";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label25.Location = new System.Drawing.Point(231, 0);
            label25.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(39, 13);
            label25.TabIndex = 17;
            label25.Text = "Serial";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label26.Location = new System.Drawing.Point(282, 0);
            label26.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(37, 13);
            label26.TabIndex = 18;
            label26.Text = "Recv";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label27.Location = new System.Drawing.Point(333, 0);
            label27.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(39, 13);
            label27.TabIndex = 19;
            label27.Text = "Serial";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label29.Location = new System.Drawing.Point(384, 0);
            label29.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label29.Name = "label29";
            label29.Size = new System.Drawing.Size(50, 13);
            label29.TabIndex = 21;
            label29.Text = "Locator";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label30.Location = new System.Drawing.Point(445, 0);
            label30.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label30.Name = "label30";
            label30.Size = new System.Drawing.Size(29, 13);
            label30.TabIndex = 22;
            label30.Text = "Dist";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label31.Location = new System.Drawing.Point(498, 0);
            label31.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label31.Name = "label31";
            label31.Size = new System.Drawing.Size(38, 13);
            label31.TabIndex = 23;
            label31.Text = "Beam";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label32.Location = new System.Drawing.Point(539, 0);
            label32.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label32.Name = "label32";
            label32.Size = new System.Drawing.Size(64, 13);
            label32.TabIndex = 24;
            label32.Text = "Comments";
            // 
            // m_Time
            // 
            this.m_Time.Location = new System.Drawing.Point(44, 193);
            this.m_Time.Name = "m_Time";
            this.m_Time.ReadOnly = true;
            this.m_Time.Size = new System.Drawing.Size(35, 20);
            this.m_Time.TabIndex = 1;
            this.m_Time.TabStop = false;
            this.m_Time.Text = "2359";
            this.m_Time.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_Band
            // 
            this.m_Band.Location = new System.Drawing.Point(3, 193);
            this.m_Band.Name = "m_Band";
            this.m_Band.ReadOnly = true;
            this.m_Band.Size = new System.Drawing.Size(35, 20);
            this.m_Band.TabIndex = 0;
            this.m_Band.TabStop = false;
            this.m_Band.Text = "70cm";
            this.m_Band.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_Callsign
            // 
            this.m_Callsign.Location = new System.Drawing.Point(85, 193);
            this.m_Callsign.Name = "m_Callsign";
            this.m_Callsign.Size = new System.Drawing.Size(92, 20);
            this.m_Callsign.TabIndex = 2;
            this.m_Callsign.Text = "GB100CAM/P";
            this.m_Callsign.TextChanged += new System.EventHandler(this.m_Callsign_TextChanged);
            this.m_Callsign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            this.m_Callsign.Leave += new System.EventHandler(this.m_Callsign_Leave);
            // 
            // m_RstSent
            // 
            this.m_RstSent.Location = new System.Drawing.Point(183, 193);
            this.m_RstSent.Name = "m_RstSent";
            this.m_RstSent.Size = new System.Drawing.Size(45, 20);
            this.m_RstSent.TabIndex = 3;
            this.m_RstSent.Text = "59+40";
            this.m_RstSent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_SerialSent
            // 
            this.m_SerialSent.Location = new System.Drawing.Point(234, 193);
            this.m_SerialSent.Name = "m_SerialSent";
            this.m_SerialSent.ReadOnly = true;
            this.m_SerialSent.Size = new System.Drawing.Size(45, 20);
            this.m_SerialSent.TabIndex = 4;
            this.m_SerialSent.TabStop = false;
            this.m_SerialSent.Text = "1234";
            this.m_SerialSent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_RstReceived
            // 
            this.m_RstReceived.Location = new System.Drawing.Point(285, 193);
            this.m_RstReceived.Name = "m_RstReceived";
            this.m_RstReceived.Size = new System.Drawing.Size(45, 20);
            this.m_RstReceived.TabIndex = 5;
            this.m_RstReceived.Text = "59+40";
            this.m_RstReceived.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_Locator
            // 
            this.m_Locator.Location = new System.Drawing.Point(387, 193);
            this.m_Locator.Name = "m_Locator";
            this.m_Locator.Size = new System.Drawing.Size(55, 20);
            this.m_Locator.TabIndex = 7;
            this.m_Locator.Text = "AB12CD";
            this.m_Locator.TextChanged += new System.EventHandler(this.m_Locator_TextChanged);
            this.m_Locator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            this.m_Locator.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_Locator_KeyUp);
            // 
            // m_SerialReceived
            // 
            this.m_SerialReceived.Location = new System.Drawing.Point(336, 193);
            this.m_SerialReceived.Name = "m_SerialReceived";
            this.m_SerialReceived.Size = new System.Drawing.Size(45, 20);
            this.m_SerialReceived.TabIndex = 6;
            this.m_SerialReceived.Text = "0001";
            this.m_SerialReceived.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            this.m_SerialReceived.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_SerialReceived_KeyUp);
            // 
            // m_Comments
            // 
            this.m_Comments.Location = new System.Drawing.Point(542, 193);
            this.m_Comments.Name = "m_Comments";
            this.m_Comments.Size = new System.Drawing.Size(172, 20);
            this.m_Comments.TabIndex = 10;
            this.m_Comments.Text = "This was a very nice QSO";
            this.m_Comments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_Distance
            // 
            this.m_Distance.Location = new System.Drawing.Point(448, 193);
            this.m_Distance.Name = "m_Distance";
            this.m_Distance.ReadOnly = true;
            this.m_Distance.Size = new System.Drawing.Size(47, 20);
            this.m_Distance.TabIndex = 8;
            this.m_Distance.TabStop = false;
            this.m_Distance.Text = "23km";
            this.m_Distance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_Beam
            // 
            this.m_Beam.Location = new System.Drawing.Point(501, 193);
            this.m_Beam.Name = "m_Beam";
            this.m_Beam.ReadOnly = true;
            this.m_Beam.Size = new System.Drawing.Size(35, 20);
            this.m_Beam.TabIndex = 9;
            this.m_Beam.TabStop = false;
            this.m_Beam.Text = "090";
            this.m_Beam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentQSOKeyDown);
            // 
            // m_Notes
            // 
            this.m_Notes.Location = new System.Drawing.Point(12, 399);
            this.m_Notes.Name = "m_Notes";
            this.m_Notes.Size = new System.Drawing.Size(780, 18);
            this.m_Notes.TabIndex = 13;
            this.m_Notes.Text = "Notes go down here...";
            this.m_Notes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_ContactTable
            // 
            this.m_ContactTable.ColumnCount = 12;
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_ContactTable.Controls.Add(label32, 10, 0);
            this.m_ContactTable.Controls.Add(label31, 9, 0);
            this.m_ContactTable.Controls.Add(label30, 8, 0);
            this.m_ContactTable.Controls.Add(label29, 7, 0);
            this.m_ContactTable.Controls.Add(label27, 6, 0);
            this.m_ContactTable.Controls.Add(label26, 5, 0);
            this.m_ContactTable.Controls.Add(label25, 4, 0);
            this.m_ContactTable.Controls.Add(label24, 3, 0);
            this.m_ContactTable.Controls.Add(label23, 2, 0);
            this.m_ContactTable.Controls.Add(label22, 1, 0);
            this.m_ContactTable.Controls.Add(label21, 0, 0);
            this.m_ContactTable.Controls.Add(this.m_Band, 0, 9);
            this.m_ContactTable.Controls.Add(this.m_Time, 1, 9);
            this.m_ContactTable.Controls.Add(this.m_Callsign, 2, 9);
            this.m_ContactTable.Controls.Add(this.m_RstSent, 3, 9);
            this.m_ContactTable.Controls.Add(this.m_Comments, 10, 9);
            this.m_ContactTable.Controls.Add(this.m_Beam, 9, 9);
            this.m_ContactTable.Controls.Add(this.m_SerialSent, 4, 9);
            this.m_ContactTable.Controls.Add(this.m_Distance, 8, 9);
            this.m_ContactTable.Controls.Add(this.m_RstReceived, 5, 9);
            this.m_ContactTable.Controls.Add(this.m_SerialReceived, 6, 9);
            this.m_ContactTable.Controls.Add(this.m_Locator, 7, 9);
            this.m_ContactTable.Location = new System.Drawing.Point(12, 180);
            this.m_ContactTable.Name = "m_ContactTable";
            this.m_ContactTable.RowCount = 10;
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_ContactTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_ContactTable.Size = new System.Drawing.Size(780, 216);
            this.m_ContactTable.TabIndex = 1;
            // 
            // m_RedrawTimer
            // 
            this.m_RedrawTimer.Interval = 2500;
            this.m_RedrawTimer.Tick += new System.EventHandler(this.m_RedrawTimer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.wipeQSOToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.logToolStripMenuItem.Text = "&Log";
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openLogToolStripMenuItem.Text = "&Open log...";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDIFToolStripMenuItem1,
            this.knownCallsignsToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // aDIFToolStripMenuItem1
            // 
            this.aDIFToolStripMenuItem1.Name = "aDIFToolStripMenuItem1";
            this.aDIFToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.aDIFToolStripMenuItem1.Text = "&ADIF...";
            this.aDIFToolStripMenuItem1.Click += new System.EventHandler(this.ImportAdif);
            // 
            // knownCallsignsToolStripMenuItem
            // 
            this.knownCallsignsToolStripMenuItem.Name = "knownCallsignsToolStripMenuItem";
            this.knownCallsignsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.knownCallsignsToolStripMenuItem.Text = "&Known Callsigns...";
            this.knownCallsignsToolStripMenuItem.Click += new System.EventHandler(this.ImportKnownCallsigns);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDIFToolStripMenuItem,
            this.cabrilloToolStripMenuItem,
            this.rEG1TESTToolStripMenuItem,
            this.multipleToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            // 
            // aDIFToolStripMenuItem
            // 
            this.aDIFToolStripMenuItem.Name = "aDIFToolStripMenuItem";
            this.aDIFToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aDIFToolStripMenuItem.Text = "&ADIF...";
            this.aDIFToolStripMenuItem.Click += new System.EventHandler(this.ExportAdif);
            // 
            // cabrilloToolStripMenuItem
            // 
            this.cabrilloToolStripMenuItem.Name = "cabrilloToolStripMenuItem";
            this.cabrilloToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.cabrilloToolStripMenuItem.Text = "&Cabrillo...";
            this.cabrilloToolStripMenuItem.Click += new System.EventHandler(this.ExportCabrillo);
            // 
            // rEG1TESTToolStripMenuItem
            // 
            this.rEG1TESTToolStripMenuItem.Name = "rEG1TESTToolStripMenuItem";
            this.rEG1TESTToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.rEG1TESTToolStripMenuItem.Text = "&REG1TEST...";
            this.rEG1TESTToolStripMenuItem.Click += new System.EventHandler(this.m_Export_Click);
            // 
            // multipleToolStripMenuItem
            // 
            this.multipleToolStripMenuItem.Name = "multipleToolStripMenuItem";
            this.multipleToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.multipleToolStripMenuItem.Text = "Multiple...";
            this.multipleToolStripMenuItem.Click += new System.EventHandler(this.ExportMultiple);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_OnlyMyQSOs,
            this.m_PerformQRZLookups});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // m_OnlyMyQSOs
            // 
            this.m_OnlyMyQSOs.Name = "m_OnlyMyQSOs";
            this.m_OnlyMyQSOs.Size = new System.Drawing.Size(169, 22);
            this.m_OnlyMyQSOs.Text = "Only my QSOs";
            this.m_OnlyMyQSOs.Click += new System.EventHandler(this.OnlyMyQSOsClicked);
            // 
            // m_PerformQRZLookups
            // 
            this.m_PerformQRZLookups.CheckOnClick = true;
            this.m_PerformQRZLookups.Name = "m_PerformQRZLookups";
            this.m_PerformQRZLookups.Size = new System.Drawing.Size(169, 22);
            this.m_PerformQRZLookups.Text = "QRZ.com lookups";
            this.m_PerformQRZLookups.CheckedChanged += new System.EventHandler(this.m_PerformQRZLookups_CheckedChanged);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winKeyToolStripMenuItem,
            this.rigControlToolStripMenuItem,
            this.QrzUserSetupToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // winKeyToolStripMenuItem
            // 
            this.winKeyToolStripMenuItem.Name = "winKeyToolStripMenuItem";
            this.winKeyToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.winKeyToolStripMenuItem.Text = "WinKey...";
            // 
            // rigControlToolStripMenuItem
            // 
            this.rigControlToolStripMenuItem.Name = "rigControlToolStripMenuItem";
            this.rigControlToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.rigControlToolStripMenuItem.Text = "Rig Control...";
            // 
            // QrzUserSetupToolStripMenuItem
            // 
            this.QrzUserSetupToolStripMenuItem.Name = "QrzUserSetupToolStripMenuItem";
            this.QrzUserSetupToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.QrzUserSetupToolStripMenuItem.Text = "QRZ.com User Setup...";
            this.QrzUserSetupToolStripMenuItem.Click += new System.EventHandler(this.QrzUserSetupToolStripMenuItem_Click);
            // 
            // wipeQSOToolStripMenuItem
            // 
            this.wipeQSOToolStripMenuItem.Name = "wipeQSOToolStripMenuItem";
            this.wipeQSOToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.wipeQSOToolStripMenuItem.Text = "&Wipe QSO";
            this.wipeQSOToolStripMenuItem.Click += new System.EventHandler(this.WipeQSOClicked);
            // 
            // ContestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 423);
            this.Controls.Add(this.m_ContactTable);
            this.Controls.Add(groupBox2);
            this.Controls.Add(this.m_Notes);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ContestForm";
            this.Text = "CamLog";
            this.Load += new System.EventHandler(this.ContestForm_Load);
            this.Shown += new System.EventHandler(this.ContestForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContestForm_KeyDown);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            this.m_ContactTable.ResumeLayout(false);
            this.m_ContactTable.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_Time;
        private System.Windows.Forms.TextBox m_Band;
        private System.Windows.Forms.TextBox m_Callsign;
        private System.Windows.Forms.TextBox m_RstSent;
        private System.Windows.Forms.TextBox m_SerialSent;
        private System.Windows.Forms.TextBox m_RstReceived;
        private System.Windows.Forms.TextBox m_Locator;
        private System.Windows.Forms.TextBox m_SerialReceived;
        private System.Windows.Forms.ComboBox m_OurMode;
        private System.Windows.Forms.TextBox m_OurLocator;
        private System.Windows.Forms.ComboBox m_OurBand;
        private System.Windows.Forms.TextBox m_OurOperator;
        private System.Windows.Forms.TextBox m_Comments;
        private System.Windows.Forms.TextBox m_Distance;
        private System.Windows.Forms.TextBox m_Beam;
        private System.Windows.Forms.Label m_Notes;
        private System.Windows.Forms.TableLayoutPanel m_ContactTable;
        private System.Windows.Forms.Timer m_RedrawTimer;
        private System.Windows.Forms.TextBox m_Station;
        private System.Windows.Forms.TextBox m_Frequency;
        private System.Windows.Forms.ListBox m_MatchesThisContest;
        private System.Windows.Forms.ListBox m_MatchesKnownCalls;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem winKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wipeQSOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_OnlyMyQSOs;
        private System.Windows.Forms.ToolStripMenuItem rigControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_PerformQRZLookups;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cabrilloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEG1TESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDIFToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem knownCallsignsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multipleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QrzUserSetupToolStripMenuItem;
        //private System.Windows.Forms.DataGridView m_QSOGrid;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QBand;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QTime;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QCallsign;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QSent;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QSentSerial;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QReceived;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QReceivedSerial;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QLocator;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QDistance;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QBeam;
        //private System.Windows.Forms.DataGridViewTextBoxColumn m_QComments;
    }
}

