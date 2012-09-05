namespace EastSidazFantasy
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMatchups = new System.Windows.Forms.TabPage();
            this.grdMatchups = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNewMatchup = new System.Windows.Forms.Button();
            this.tabScores = new System.Windows.Forms.TabPage();
            this.grdScores = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.cmbMatchups = new System.Windows.Forms.ComboBox();
            this.grdDaily = new System.Windows.Forms.DataGridView();
            this.colID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbLeagues = new System.Windows.Forms.ComboBox();
            this.lblLeague = new System.Windows.Forms.Label();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RBI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AVG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.K = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WHIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabMatchups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMatchups)).BeginInit();
            this.tabScores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdScores)).BeginInit();
            this.tabDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDaily)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(662, 468);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.Visible = false;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.cmbLeagues);
            this.panel1.Controls.Add(this.lblLeague);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 468);
            this.panel1.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(555, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(62, 26);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMatchups);
            this.tabControl1.Controls.Add(this.tabScores);
            this.tabControl1.Controls.Add(this.tabDaily);
            this.tabControl1.Location = new System.Drawing.Point(6, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(650, 416);
            this.tabControl1.TabIndex = 5;
            // 
            // tabMatchups
            // 
            this.tabMatchups.Controls.Add(this.grdMatchups);
            this.tabMatchups.Controls.Add(this.btnNewMatchup);
            this.tabMatchups.Location = new System.Drawing.Point(4, 22);
            this.tabMatchups.Name = "tabMatchups";
            this.tabMatchups.Padding = new System.Windows.Forms.Padding(3);
            this.tabMatchups.Size = new System.Drawing.Size(642, 390);
            this.tabMatchups.TabIndex = 0;
            this.tabMatchups.Text = "Matchups";
            this.tabMatchups.UseVisualStyleBackColor = true;
            // 
            // grdMatchups
            // 
            this.grdMatchups.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMatchups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMatchups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMatchups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Team1,
            this.Team2,
            this.StartDate,
            this.EndDate});
            this.grdMatchups.Location = new System.Drawing.Point(2, 4);
            this.grdMatchups.Name = "grdMatchups";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMatchups.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdMatchups.RowHeadersVisible = false;
            this.grdMatchups.Size = new System.Drawing.Size(634, 354);
            this.grdMatchups.TabIndex = 2;
            this.grdMatchups.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // ID
            // 
            this.ID.HeaderText = "I.D.";
            this.ID.Name = "ID";
            this.ID.Width = 50;
            // 
            // Team1
            // 
            this.Team1.HeaderText = "Team 1";
            this.Team1.Name = "Team1";
            this.Team1.Width = 175;
            // 
            // Team2
            // 
            this.Team2.HeaderText = "Team 2";
            this.Team2.Name = "Team2";
            this.Team2.Width = 175;
            // 
            // StartDate
            // 
            this.StartDate.HeaderText = "Start Date";
            this.StartDate.Name = "StartDate";
            this.StartDate.Width = 80;
            // 
            // EndDate
            // 
            this.EndDate.HeaderText = "End Date";
            this.EndDate.Name = "EndDate";
            this.EndDate.Width = 80;
            // 
            // btnNewMatchup
            // 
            this.btnNewMatchup.Location = new System.Drawing.Point(556, 360);
            this.btnNewMatchup.Name = "btnNewMatchup";
            this.btnNewMatchup.Size = new System.Drawing.Size(82, 26);
            this.btnNewMatchup.TabIndex = 4;
            this.btnNewMatchup.Text = "New Matchup";
            this.btnNewMatchup.UseVisualStyleBackColor = true;
            this.btnNewMatchup.Click += new System.EventHandler(this.btnNewMatchup_Click);
            // 
            // tabScores
            // 
            this.tabScores.Controls.Add(this.grdScores);
            this.tabScores.Controls.Add(this.btnUpdate);
            this.tabScores.Location = new System.Drawing.Point(4, 22);
            this.tabScores.Name = "tabScores";
            this.tabScores.Padding = new System.Windows.Forms.Padding(3);
            this.tabScores.Size = new System.Drawing.Size(642, 390);
            this.tabScores.TabIndex = 1;
            this.tabScores.Text = "Scores";
            this.tabScores.UseVisualStyleBackColor = true;
            // 
            // grdScores
            // 
            this.grdScores.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdScores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdScores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdScores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.Team,
            this.R,
            this.HR,
            this.RBI,
            this.SB,
            this.AVG,
            this.OBP,
            this.W,
            this.S,
            this.K,
            this.ERA,
            this.WHIP,
            this.Score});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdScores.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdScores.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdScores.Location = new System.Drawing.Point(3, 3);
            this.grdScores.Name = "grdScores";
            this.grdScores.RowHeadersVisible = false;
            this.grdScores.Size = new System.Drawing.Size(636, 349);
            this.grdScores.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(570, 356);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(66, 26);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tabDaily
            // 
            this.tabDaily.Controls.Add(this.cmbMatchups);
            this.tabDaily.Controls.Add(this.grdDaily);
            this.tabDaily.Location = new System.Drawing.Point(4, 22);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Padding = new System.Windows.Forms.Padding(3);
            this.tabDaily.Size = new System.Drawing.Size(642, 390);
            this.tabDaily.TabIndex = 2;
            this.tabDaily.Text = "Daily";
            this.tabDaily.UseVisualStyleBackColor = true;
            // 
            // cmbMatchups
            // 
            this.cmbMatchups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMatchups.FormattingEnabled = true;
            this.cmbMatchups.Location = new System.Drawing.Point(274, 360);
            this.cmbMatchups.Name = "cmbMatchups";
            this.cmbMatchups.Size = new System.Drawing.Size(326, 21);
            this.cmbMatchups.TabIndex = 3;
            this.cmbMatchups.SelectedIndexChanged += new System.EventHandler(this.cmbMatchups_SelectedIndexChanged);
            // 
            // grdDaily
            // 
            this.grdDaily.AllowUserToAddRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDaily.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grdDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDaily.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID2,
            this.dataGridViewTextBoxColumn1,
            this.Date,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDaily.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdDaily.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdDaily.Location = new System.Drawing.Point(3, 3);
            this.grdDaily.Name = "grdDaily";
            this.grdDaily.RowHeadersVisible = false;
            this.grdDaily.Size = new System.Drawing.Size(636, 349);
            this.grdDaily.TabIndex = 2;
            // 
            // colID2
            // 
            this.colID2.HeaderText = "I.D.";
            this.colID2.Name = "colID2";
            this.colID2.Width = 35;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Team";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 110;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 75;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "R";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 35;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "HR";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 35;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "RBI";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 35;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "SB";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 35;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "AVG";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 35;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "OBP";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 35;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "W";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 35;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "S";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 35;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "K";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 35;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "ERA";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 35;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "WHIP";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 35;
            // 
            // cmbLeagues
            // 
            this.cmbLeagues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeagues.FormattingEnabled = true;
            this.cmbLeagues.Location = new System.Drawing.Point(64, 10);
            this.cmbLeagues.Name = "cmbLeagues";
            this.cmbLeagues.Size = new System.Drawing.Size(258, 21);
            this.cmbLeagues.TabIndex = 1;
            this.cmbLeagues.SelectedIndexChanged += new System.EventHandler(this.cmbLeagues_SelectedIndexChanged);
            // 
            // lblLeague
            // 
            this.lblLeague.AutoSize = true;
            this.lblLeague.Location = new System.Drawing.Point(8, 14);
            this.lblLeague.Name = "lblLeague";
            this.lblLeague.Size = new System.Drawing.Size(43, 13);
            this.lblLeague.TabIndex = 0;
            this.lblLeague.Text = "League";
            // 
            // colID
            // 
            this.colID.HeaderText = "I.D.";
            this.colID.Name = "colID";
            this.colID.Width = 35;
            // 
            // Team
            // 
            this.Team.HeaderText = "Team";
            this.Team.Name = "Team";
            this.Team.Width = 130;
            // 
            // R
            // 
            this.R.HeaderText = "R";
            this.R.Name = "R";
            this.R.Width = 35;
            // 
            // HR
            // 
            this.HR.HeaderText = "HR";
            this.HR.Name = "HR";
            this.HR.Width = 35;
            // 
            // RBI
            // 
            this.RBI.HeaderText = "RBI";
            this.RBI.Name = "RBI";
            this.RBI.Width = 35;
            // 
            // SB
            // 
            this.SB.HeaderText = "SB";
            this.SB.Name = "SB";
            this.SB.Width = 35;
            // 
            // AVG
            // 
            this.AVG.HeaderText = "AVG";
            this.AVG.Name = "AVG";
            this.AVG.Width = 45;
            // 
            // OBP
            // 
            this.OBP.HeaderText = "OBP";
            this.OBP.Name = "OBP";
            this.OBP.Width = 45;
            // 
            // W
            // 
            this.W.HeaderText = "W";
            this.W.Name = "W";
            this.W.Width = 35;
            // 
            // S
            // 
            this.S.HeaderText = "S";
            this.S.Name = "S";
            this.S.Width = 35;
            // 
            // K
            // 
            this.K.HeaderText = "K";
            this.K.Name = "K";
            this.K.Width = 35;
            // 
            // ERA
            // 
            this.ERA.HeaderText = "ERA";
            this.ERA.Name = "ERA";
            this.ERA.Width = 35;
            // 
            // WHIP
            // 
            this.WHIP.HeaderText = "WHIP";
            this.WHIP.Name = "WHIP";
            this.WHIP.Width = 35;
            // 
            // Score
            // 
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            this.Score.Width = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 468);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EastSidaz Fantasy Sports";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Disposed += new System.EventHandler(this.Form1_Disposed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabMatchups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMatchups)).EndInit();
            this.tabScores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdScores)).EndInit();
            this.tabDaily.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDaily)).EndInit();
            this.ResumeLayout(false);

        }        

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbLeagues;
        private System.Windows.Forms.Label lblLeague;
        private System.Windows.Forms.DataGridView grdMatchups;
        private System.Windows.Forms.Button btnNewMatchup;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMatchups;
        private System.Windows.Forms.TabPage tabScores;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView grdScores;
        private System.Windows.Forms.TabPage tabDaily;
        private System.Windows.Forms.ComboBox cmbMatchups;
        private System.Windows.Forms.DataGridView grdDaily;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn R;
        private System.Windows.Forms.DataGridViewTextBoxColumn HR;
        private System.Windows.Forms.DataGridViewTextBoxColumn RBI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SB;
        private System.Windows.Forms.DataGridViewTextBoxColumn AVG;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBP;
        private System.Windows.Forms.DataGridViewTextBoxColumn W;
        private System.Windows.Forms.DataGridViewTextBoxColumn S;
        private System.Windows.Forms.DataGridViewTextBoxColumn K;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERA;
        private System.Windows.Forms.DataGridViewTextBoxColumn WHIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;        
    }
}

