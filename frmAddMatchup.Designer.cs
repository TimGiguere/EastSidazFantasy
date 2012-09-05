namespace EastSidazFantasy
{
    partial class frmAddMatchup
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
            this.lblTeam1 = new System.Windows.Forms.Label();
            this.lblTeam2 = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbTeam1 = new System.Windows.Forms.ComboBox();
            this.cmbTeam2 = new System.Windows.Forms.ComboBox();
            this.dteStartDate = new System.Windows.Forms.DateTimePicker();
            this.dteEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTeam1
            // 
            this.lblTeam1.AutoSize = true;
            this.lblTeam1.Location = new System.Drawing.Point(15, 8);
            this.lblTeam1.Name = "lblTeam1";
            this.lblTeam1.Size = new System.Drawing.Size(43, 13);
            this.lblTeam1.TabIndex = 0;
            this.lblTeam1.Text = "Team 1";
            // 
            // lblTeam2
            // 
            this.lblTeam2.AutoSize = true;
            this.lblTeam2.Location = new System.Drawing.Point(15, 36);
            this.lblTeam2.Name = "lblTeam2";
            this.lblTeam2.Size = new System.Drawing.Size(43, 13);
            this.lblTeam2.TabIndex = 1;
            this.lblTeam2.Text = "Team 2";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(15, 92);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(15, 64);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Start Date";
            // 
            // cmbTeam1
            // 
            this.cmbTeam1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeam1.FormattingEnabled = true;
            this.cmbTeam1.Location = new System.Drawing.Point(92, 4);
            this.cmbTeam1.Name = "cmbTeam1";
            this.cmbTeam1.Size = new System.Drawing.Size(186, 21);
            this.cmbTeam1.TabIndex = 4;
            // 
            // cmbTeam2
            // 
            this.cmbTeam2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeam2.FormattingEnabled = true;
            this.cmbTeam2.Location = new System.Drawing.Point(92, 32);
            this.cmbTeam2.Name = "cmbTeam2";
            this.cmbTeam2.Size = new System.Drawing.Size(186, 21);
            this.cmbTeam2.TabIndex = 5;
            // 
            // dteStartDate
            // 
            this.dteStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dteStartDate.Location = new System.Drawing.Point(170, 60);
            this.dteStartDate.Name = "dteStartDate";
            this.dteStartDate.Size = new System.Drawing.Size(108, 20);
            this.dteStartDate.TabIndex = 6;
            // 
            // dteEndDate
            // 
            this.dteEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dteEndDate.Location = new System.Drawing.Point(170, 88);
            this.dteEndDate.Name = "dteEndDate";
            this.dteEndDate.Size = new System.Drawing.Size(108, 20);
            this.dteEndDate.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(158, 114);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(58, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmAddMatchup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 146);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dteEndDate);
            this.Controls.Add(this.dteStartDate);
            this.Controls.Add(this.cmbTeam2);
            this.Controls.Add(this.cmbTeam1);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblTeam2);
            this.Controls.Add(this.lblTeam1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddMatchup";
            this.ShowInTaskbar = false;
            this.Text = "Create New Matchup";
            this.Load += new System.EventHandler(this.frmAddMatchup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTeam1;
        private System.Windows.Forms.Label lblTeam2;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ComboBox cmbTeam1;
        private System.Windows.Forms.ComboBox cmbTeam2;
        private System.Windows.Forms.DateTimePicker dteStartDate;
        private System.Windows.Forms.DateTimePicker dteEndDate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}