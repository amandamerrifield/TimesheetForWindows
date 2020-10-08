namespace TimesheetForWindows
{
    partial class SelectReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.lbxSelect = new System.Windows.Forms.ListBox();
			this.btnViewReport = new System.Windows.Forms.Button();
			this.gbxTimecardRollup01 = new System.Windows.Forms.GroupBox();
			this.btnLaunchTimecardRollup01 = new System.Windows.Forms.Button();
			this.tbxStartingWeekNbr = new System.Windows.Forms.TextBox();
			this.EndingWeekNbr = new System.Windows.Forms.TextBox();
			this.tbxYearNumber = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gbxTimecardRollup01.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbxSelect
			// 
			this.lbxSelect.FormattingEnabled = true;
			this.lbxSelect.ImeMode = System.Windows.Forms.ImeMode.Alpha;
			this.lbxSelect.Location = new System.Drawing.Point(12, 12);
			this.lbxSelect.Name = "lbxSelect";
			this.lbxSelect.Size = new System.Drawing.Size(284, 160);
			this.lbxSelect.TabIndex = 0;
			this.lbxSelect.SelectedIndexChanged += new System.EventHandler(this.lbxSelect_SelectedIndexChanged);
			// 
			// btnViewReport
			// 
			this.btnViewReport.Location = new System.Drawing.Point(167, 194);
			this.btnViewReport.Name = "btnViewReport";
			this.btnViewReport.Size = new System.Drawing.Size(129, 30);
			this.btnViewReport.TabIndex = 1;
			this.btnViewReport.Text = "Launch Report";
			this.btnViewReport.UseVisualStyleBackColor = true;
			this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
			// 
			// gbxTimecardRollup01
			// 
			this.gbxTimecardRollup01.Controls.Add(this.btnLaunchTimecardRollup01);
			this.gbxTimecardRollup01.Controls.Add(this.tbxStartingWeekNbr);
			this.gbxTimecardRollup01.Controls.Add(this.EndingWeekNbr);
			this.gbxTimecardRollup01.Controls.Add(this.tbxYearNumber);
			this.gbxTimecardRollup01.Controls.Add(this.label3);
			this.gbxTimecardRollup01.Controls.Add(this.label2);
			this.gbxTimecardRollup01.Controls.Add(this.label1);
			this.gbxTimecardRollup01.Location = new System.Drawing.Point(17, 239);
			this.gbxTimecardRollup01.Name = "gbxTimecardRollup01";
			this.gbxTimecardRollup01.Size = new System.Drawing.Size(279, 171);
			this.gbxTimecardRollup01.TabIndex = 2;
			this.gbxTimecardRollup01.TabStop = false;
			this.gbxTimecardRollup01.Text = "Timecard Rollup Parameters";
			this.gbxTimecardRollup01.Visible = false;
			// 
			// btnLaunchTimecardRollup01
			// 
			this.btnLaunchTimecardRollup01.Location = new System.Drawing.Point(150, 127);
			this.btnLaunchTimecardRollup01.Name = "btnLaunchTimecardRollup01";
			this.btnLaunchTimecardRollup01.Size = new System.Drawing.Size(103, 27);
			this.btnLaunchTimecardRollup01.TabIndex = 13;
			this.btnLaunchTimecardRollup01.Text = "Launch Report";
			this.btnLaunchTimecardRollup01.UseVisualStyleBackColor = true;
			this.btnLaunchTimecardRollup01.Click += new System.EventHandler(this.btnTimecardRollup01_Click);
			// 
			// tbxStartingWeekNbr
			// 
			this.tbxStartingWeekNbr.Location = new System.Drawing.Point(150, 60);
			this.tbxStartingWeekNbr.Name = "tbxStartingWeekNbr";
			this.tbxStartingWeekNbr.Size = new System.Drawing.Size(104, 20);
			this.tbxStartingWeekNbr.TabIndex = 12;
			// 
			// EndingWeekNbr
			// 
			this.EndingWeekNbr.Location = new System.Drawing.Point(150, 92);
			this.EndingWeekNbr.Name = "EndingWeekNbr";
			this.EndingWeekNbr.Size = new System.Drawing.Size(104, 20);
			this.EndingWeekNbr.TabIndex = 11;
			// 
			// tbxYearNumber
			// 
			this.tbxYearNumber.Location = new System.Drawing.Point(150, 28);
			this.tbxYearNumber.Name = "tbxYearNumber";
			this.tbxYearNumber.Size = new System.Drawing.Size(104, 20);
			this.tbxYearNumber.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Ending Week Number :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Starting Week Number :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Year Number :";
			// 
			// SelectReportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(317, 559);
			this.ControlBox = false;
			this.Controls.Add(this.gbxTimecardRollup01);
			this.Controls.Add(this.btnViewReport);
			this.Controls.Add(this.lbxSelect);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectReportForm";
			this.Text = "Select Report";
			this.Load += new System.EventHandler(this.SelectReportForm_Load);
			this.gbxTimecardRollup01.ResumeLayout(false);
			this.gbxTimecardRollup01.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxSelect;
        private System.Windows.Forms.Button btnViewReport;
		private System.Windows.Forms.GroupBox gbxTimecardRollup01;
		private System.Windows.Forms.Button btnLaunchTimecardRollup01;
		private System.Windows.Forms.TextBox tbxStartingWeekNbr;
		private System.Windows.Forms.TextBox EndingWeekNbr;
		private System.Windows.Forms.TextBox tbxYearNumber;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}