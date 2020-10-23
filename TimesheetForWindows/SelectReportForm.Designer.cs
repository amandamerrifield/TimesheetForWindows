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
            this.label4 = new System.Windows.Forms.Label();
            this.btnLaunchTimecardRollup01 = new System.Windows.Forms.Button();
            this.tbxStartingWeekNbr = new System.Windows.Forms.TextBox();
            this.tbxEndingWeekNbr = new System.Windows.Forms.TextBox();
            this.tbxYearNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxRollup02 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLaunchTimecardRollup02 = new System.Windows.Forms.Button();
            this.tbxStartWeekNbr02 = new System.Windows.Forms.TextBox();
            this.tbxEndingWeekNbr02 = new System.Windows.Forms.TextBox();
            this.tbxYearNumber02 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxEmployeeIdNbr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.gbxTimecardRollup01.SuspendLayout();
            this.gbxRollup02.SuspendLayout();
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
            this.gbxTimecardRollup01.Controls.Add(this.label4);
            this.gbxTimecardRollup01.Controls.Add(this.btnLaunchTimecardRollup01);
            this.gbxTimecardRollup01.Controls.Add(this.tbxStartingWeekNbr);
            this.gbxTimecardRollup01.Controls.Add(this.tbxEndingWeekNbr);
            this.gbxTimecardRollup01.Controls.Add(this.tbxYearNumber);
            this.gbxTimecardRollup01.Controls.Add(this.label3);
            this.gbxTimecardRollup01.Controls.Add(this.label2);
            this.gbxTimecardRollup01.Controls.Add(this.label1);
            this.gbxTimecardRollup01.Location = new System.Drawing.Point(17, 239);
            this.gbxTimecardRollup01.Name = "gbxTimecardRollup01";
            this.gbxTimecardRollup01.Size = new System.Drawing.Size(279, 213);
            this.gbxTimecardRollup01.TabIndex = 2;
            this.gbxTimecardRollup01.TabStop = false;
            this.gbxTimecardRollup01.Text = "Timecard Rollup Parameters";
            this.gbxTimecardRollup01.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 26);
            this.label4.TabIndex = 14;
            this.label4.Text = "Insert below the year and week range\r\n to generate a report.";
            // 
            // btnLaunchTimecardRollup01
            // 
            this.btnLaunchTimecardRollup01.Location = new System.Drawing.Point(150, 176);
            this.btnLaunchTimecardRollup01.Name = "btnLaunchTimecardRollup01";
            this.btnLaunchTimecardRollup01.Size = new System.Drawing.Size(103, 27);
            this.btnLaunchTimecardRollup01.TabIndex = 6;
            this.btnLaunchTimecardRollup01.Text = "Launch Report";
            this.btnLaunchTimecardRollup01.UseVisualStyleBackColor = true;
            this.btnLaunchTimecardRollup01.Click += new System.EventHandler(this.btnTimecardRollup01_Click);
            // 
            // tbxStartingWeekNbr
            // 
            this.tbxStartingWeekNbr.Location = new System.Drawing.Point(150, 109);
            this.tbxStartingWeekNbr.Name = "tbxStartingWeekNbr";
            this.tbxStartingWeekNbr.Size = new System.Drawing.Size(104, 20);
            this.tbxStartingWeekNbr.TabIndex = 4;
            this.tbxStartingWeekNbr.Text = "1";
            // 
            // tbxEndingWeekNbr
            // 
            this.tbxEndingWeekNbr.Location = new System.Drawing.Point(150, 141);
            this.tbxEndingWeekNbr.Name = "tbxEndingWeekNbr";
            this.tbxEndingWeekNbr.Size = new System.Drawing.Size(104, 20);
            this.tbxEndingWeekNbr.TabIndex = 5;
            this.tbxEndingWeekNbr.Text = "53";
            // 
            // tbxYearNumber
            // 
            this.tbxYearNumber.Location = new System.Drawing.Point(150, 77);
            this.tbxYearNumber.Name = "tbxYearNumber";
            this.tbxYearNumber.Size = new System.Drawing.Size(104, 20);
            this.tbxYearNumber.TabIndex = 3;
            this.tbxYearNumber.Text = "2020";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ending Week Number :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Starting Week Number :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Year Number :";
            // 
            // gbxRollup02
            // 
            this.gbxRollup02.Controls.Add(this.label9);
            this.gbxRollup02.Controls.Add(this.tbxEmployeeIdNbr);
            this.gbxRollup02.Controls.Add(this.label5);
            this.gbxRollup02.Controls.Add(this.btnLaunchTimecardRollup02);
            this.gbxRollup02.Controls.Add(this.tbxStartWeekNbr02);
            this.gbxRollup02.Controls.Add(this.tbxEndingWeekNbr02);
            this.gbxRollup02.Controls.Add(this.tbxYearNumber02);
            this.gbxRollup02.Controls.Add(this.label6);
            this.gbxRollup02.Controls.Add(this.label7);
            this.gbxRollup02.Controls.Add(this.label8);
            this.gbxRollup02.Location = new System.Drawing.Point(17, 475);
            this.gbxRollup02.Name = "gbxRollup02";
            this.gbxRollup02.Size = new System.Drawing.Size(279, 236);
            this.gbxRollup02.TabIndex = 3;
            this.gbxRollup02.TabStop = false;
            this.gbxRollup02.Text = "Employee Timecard Rollup Parameters";
            this.gbxRollup02.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 26);
            this.label5.TabIndex = 14;
            this.label5.Text = "Insert below the year and week range\r\n to generate a report.";
            // 
            // btnLaunchTimecardRollup02
            // 
            this.btnLaunchTimecardRollup02.Location = new System.Drawing.Point(150, 203);
            this.btnLaunchTimecardRollup02.Name = "btnLaunchTimecardRollup02";
            this.btnLaunchTimecardRollup02.Size = new System.Drawing.Size(103, 27);
            this.btnLaunchTimecardRollup02.TabIndex = 6;
            this.btnLaunchTimecardRollup02.Text = "Launch Report";
            this.btnLaunchTimecardRollup02.UseVisualStyleBackColor = true;
            // 
            // tbxStartWeekNbr02
            // 
            this.tbxStartWeekNbr02.Location = new System.Drawing.Point(150, 109);
            this.tbxStartWeekNbr02.Name = "tbxStartWeekNbr02";
            this.tbxStartWeekNbr02.Size = new System.Drawing.Size(104, 20);
            this.tbxStartWeekNbr02.TabIndex = 4;
            this.tbxStartWeekNbr02.Text = "1";
            // 
            // tbxEndingWeekNbr02
            // 
            this.tbxEndingWeekNbr02.Location = new System.Drawing.Point(150, 141);
            this.tbxEndingWeekNbr02.Name = "tbxEndingWeekNbr02";
            this.tbxEndingWeekNbr02.Size = new System.Drawing.Size(104, 20);
            this.tbxEndingWeekNbr02.TabIndex = 5;
            this.tbxEndingWeekNbr02.Text = "53";
            // 
            // tbxYearNumber02
            // 
            this.tbxYearNumber02.Location = new System.Drawing.Point(150, 77);
            this.tbxYearNumber02.Name = "tbxYearNumber02";
            this.tbxYearNumber02.Size = new System.Drawing.Size(104, 20);
            this.tbxYearNumber02.TabIndex = 3;
            this.tbxYearNumber02.Text = "2020";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Ending Week Number :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Starting Week Number :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Year Number :";
            // 
            // tbxEmployeeIdNbr
            // 
            this.tbxEmployeeIdNbr.Location = new System.Drawing.Point(150, 172);
            this.tbxEmployeeIdNbr.Name = "tbxEmployeeIdNbr";
            this.tbxEmployeeIdNbr.Size = new System.Drawing.Size(104, 20);
            this.tbxEmployeeIdNbr.TabIndex = 15;
            this.tbxEmployeeIdNbr.Text = "102";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Employee ID Number :";
            // 
            // SelectReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 741);
            this.ControlBox = false;
            this.Controls.Add(this.gbxRollup02);
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
            this.gbxRollup02.ResumeLayout(false);
            this.gbxRollup02.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxSelect;
        private System.Windows.Forms.Button btnViewReport;
		private System.Windows.Forms.GroupBox gbxTimecardRollup01;
		private System.Windows.Forms.Button btnLaunchTimecardRollup01;
		private System.Windows.Forms.TextBox tbxStartingWeekNbr;
		private System.Windows.Forms.TextBox tbxEndingWeekNbr;
		private System.Windows.Forms.TextBox tbxYearNumber;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxRollup02;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxEmployeeIdNbr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLaunchTimecardRollup02;
        private System.Windows.Forms.TextBox tbxStartWeekNbr02;
        private System.Windows.Forms.TextBox tbxEndingWeekNbr02;
        private System.Windows.Forms.TextBox tbxYearNumber02;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}