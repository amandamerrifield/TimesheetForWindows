namespace TimesheetForWindows
{
	partial class TimecardRollupParamsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbxYearNumber = new System.Windows.Forms.TextBox();
			this.EndingWeekNbr = new System.Windows.Forms.TextBox();
			this.tbxStartingWeekNbr = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Year Number :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Starting Week Number :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 86);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Ending Week Number :";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// tbxYearNumber
			// 
			this.tbxYearNumber.Location = new System.Drawing.Point(145, 19);
			this.tbxYearNumber.Name = "tbxYearNumber";
			this.tbxYearNumber.Size = new System.Drawing.Size(104, 20);
			this.tbxYearNumber.TabIndex = 3;
			// 
			// EndingWeekNbr
			// 
			this.EndingWeekNbr.Location = new System.Drawing.Point(145, 83);
			this.EndingWeekNbr.Name = "EndingWeekNbr";
			this.EndingWeekNbr.Size = new System.Drawing.Size(104, 20);
			this.EndingWeekNbr.TabIndex = 4;
			// 
			// tbxStartingWeekNbr
			// 
			this.tbxStartingWeekNbr.Location = new System.Drawing.Point(145, 51);
			this.tbxStartingWeekNbr.Name = "tbxStartingWeekNbr";
			this.tbxStartingWeekNbr.Size = new System.Drawing.Size(104, 20);
			this.tbxStartingWeekNbr.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(145, 133);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(103, 27);
			this.button1.TabIndex = 6;
			this.button1.Text = "Launch Report";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// TimecardRollupParamsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 174);
			this.ControlBox = false;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tbxStartingWeekNbr);
			this.Controls.Add(this.EndingWeekNbr);
			this.Controls.Add(this.tbxYearNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TimecardRollupParamsForm";
			this.Text = "Please Enter Year and Week Range";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbxYearNumber;
		private System.Windows.Forms.TextBox EndingWeekNbr;
		private System.Windows.Forms.TextBox tbxStartingWeekNbr;
		private System.Windows.Forms.Button button1;
	}
}