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
            if (m_CivServer != null)
            {
                m_CivServer.FrequencyChanged -= m_CivServer_FrequencyChanged;
                m_CivServer.ModeChanged -= m_CivServer_ModeChanged;
                m_CivServer.Dispose();
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
            this.m_ExportCabrillo = new System.Windows.Forms.Button();
            this.m_ExportAdif = new System.Windows.Forms.Button();
            this.m_Export = new System.Windows.Forms.Button();
            this.m_ImportCallsigns = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.m_ExportCabrillo);
            groupBox1.Controls.Add(this.m_ExportAdif);
            groupBox1.Controls.Add(this.m_Export);
            groupBox1.Controls.Add(this.m_ImportCallsigns);
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
            groupBox1.Location = new System.Drawing.Point(400, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(392, 147);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Operator / Station details";
            // 
            // m_ExportCabrillo
            // 
            this.m_ExportCabrillo.Location = new System.Drawing.Point(297, 78);
            this.m_ExportCabrillo.Name = "m_ExportCabrillo";
            this.m_ExportCabrillo.Size = new System.Drawing.Size(89, 23);
            this.m_ExportCabrillo.TabIndex = 17;
            this.m_ExportCabrillo.Text = "Export &Cabrillo";
            this.m_ExportCabrillo.UseVisualStyleBackColor = true;
            this.m_ExportCabrillo.Click += new System.EventHandler(this.m_ExportCabrillo_Click);
            // 
            // m_ExportAdif
            // 
            this.m_ExportAdif.Location = new System.Drawing.Point(297, 104);
            this.m_ExportAdif.Name = "m_ExportAdif";
            this.m_ExportAdif.Size = new System.Drawing.Size(89, 23);
            this.m_ExportAdif.TabIndex = 16;
            this.m_ExportAdif.Text = "Export ADIF...";
            this.m_ExportAdif.UseVisualStyleBackColor = true;
            this.m_ExportAdif.Click += new System.EventHandler(this.m_ExportAdif_Click);
            // 
            // m_Export
            // 
            this.m_Export.Location = new System.Drawing.Point(193, 104);
            this.m_Export.Name = "m_Export";
            this.m_Export.Size = new System.Drawing.Size(95, 23);
            this.m_Export.TabIndex = 15;
            this.m_Export.Text = "Export REG1...";
            this.m_Export.UseVisualStyleBackColor = true;
            this.m_Export.Click += new System.EventHandler(this.m_Export_Click);
            // 
            // m_ImportCallsigns
            // 
            this.m_ImportCallsigns.Location = new System.Drawing.Point(193, 78);
            this.m_ImportCallsigns.Name = "m_ImportCallsigns";
            this.m_ImportCallsigns.Size = new System.Drawing.Size(95, 23);
            this.m_ImportCallsigns.TabIndex = 14;
            this.m_ImportCallsigns.Text = "Import known callsigns...";
            this.m_ImportCallsigns.UseVisualStyleBackColor = true;
            this.m_ImportCallsigns.Click += new System.EventHandler(this.m_ImportCallsigns_Click);
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
            groupBox2.Location = new System.Drawing.Point(12, 12);
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
            // 
            // m_Callsign
            // 
            this.m_Callsign.Location = new System.Drawing.Point(85, 193);
            this.m_Callsign.Name = "m_Callsign";
            this.m_Callsign.Size = new System.Drawing.Size(92, 20);
            this.m_Callsign.TabIndex = 2;
            this.m_Callsign.Text = "GB100CAM/P";
            this.m_Callsign.TextChanged += new System.EventHandler(this.m_Callsign_TextChanged);
            this.m_Callsign.Leave += new System.EventHandler(this.m_Callsign_Leave);
            this.m_Callsign.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ContactControls_PreviewKeyDown);
            // 
            // m_RstSent
            // 
            this.m_RstSent.Location = new System.Drawing.Point(183, 193);
            this.m_RstSent.Name = "m_RstSent";
            this.m_RstSent.Size = new System.Drawing.Size(45, 20);
            this.m_RstSent.TabIndex = 3;
            this.m_RstSent.Text = "59+40";
            this.m_RstSent.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ContactControls_PreviewKeyDown);
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
            // 
            // m_RstReceived
            // 
            this.m_RstReceived.Location = new System.Drawing.Point(285, 193);
            this.m_RstReceived.Name = "m_RstReceived";
            this.m_RstReceived.Size = new System.Drawing.Size(45, 20);
            this.m_RstReceived.TabIndex = 5;
            this.m_RstReceived.Text = "59+40";
            this.m_RstReceived.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ContactControls_PreviewKeyDown);
            // 
            // m_Locator
            // 
            this.m_Locator.Location = new System.Drawing.Point(387, 193);
            this.m_Locator.Name = "m_Locator";
            this.m_Locator.Size = new System.Drawing.Size(55, 20);
            this.m_Locator.TabIndex = 7;
            this.m_Locator.Text = "AB12CD";
            this.m_Locator.TextChanged += new System.EventHandler(this.m_Locator_TextChanged);
            this.m_Locator.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_Locator_KeyUp);
            this.m_Locator.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ContactControls_PreviewKeyDown);
            // 
            // m_SerialReceived
            // 
            this.m_SerialReceived.Location = new System.Drawing.Point(336, 193);
            this.m_SerialReceived.Name = "m_SerialReceived";
            this.m_SerialReceived.Size = new System.Drawing.Size(45, 20);
            this.m_SerialReceived.TabIndex = 6;
            this.m_SerialReceived.Text = "0001";
            this.m_SerialReceived.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ContactControls_PreviewKeyDown);
            // 
            // m_Comments
            // 
            this.m_Comments.Location = new System.Drawing.Point(542, 193);
            this.m_Comments.Name = "m_Comments";
            this.m_Comments.Size = new System.Drawing.Size(172, 20);
            this.m_Comments.TabIndex = 10;
            this.m_Comments.Text = "This was a very nice QSO";
            this.m_Comments.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ContactControls_PreviewKeyDown);
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
            // 
            // m_Notes
            // 
            this.m_Notes.Location = new System.Drawing.Point(12, 384);
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
            this.m_ContactTable.Location = new System.Drawing.Point(12, 165);
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
            // ContestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 398);
            this.Controls.Add(this.m_ContactTable);
            this.Controls.Add(groupBox2);
            this.Controls.Add(this.m_Notes);
            this.Controls.Add(groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(812, 436);
            this.MinimumSize = new System.Drawing.Size(812, 436);
            this.Name = "ContestForm";
            this.Text = "M0VFC Contest Log";
            this.Load += new System.EventHandler(this.ContestForm_Load);
            this.Shown += new System.EventHandler(this.ContestForm_Shown);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            this.m_ContactTable.ResumeLayout(false);
            this.m_ContactTable.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button m_ImportCallsigns;
        private System.Windows.Forms.Button m_Export;
        private System.Windows.Forms.Button m_ExportAdif;
        private System.Windows.Forms.Button m_ExportCabrillo;
    }
}

