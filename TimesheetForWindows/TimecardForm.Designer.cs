namespace TimesheetForWindows
{
	partial class TimecardForm
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonAddTask = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonQuit = new System.Windows.Forms.Button();
			this.comboBoxWeek = new System.Windows.Forms.ComboBox();
			this.dgvTimecardDetail = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgvTimecardDetail)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 422);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(779, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(497, 14);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(132, 25);
			this.buttonUpdate.TabIndex = 97;
			this.buttonUpdate.TabStop = false;
			this.buttonUpdate.Text = "Save Changes";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			// 
			// buttonAddTask
			// 
			this.buttonAddTask.Location = new System.Drawing.Point(215, 14);
			this.buttonAddTask.Name = "buttonAddTask";
			this.buttonAddTask.Size = new System.Drawing.Size(137, 24);
			this.buttonAddTask.TabIndex = 99;
			this.buttonAddTask.TabStop = false;
			this.buttonAddTask.Text = "Append Task";
			this.buttonAddTask.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(358, 14);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(133, 25);
			this.buttonCancel.TabIndex = 98;
			this.buttonCancel.TabStop = false;
			this.buttonCancel.Text = "Cancel Changes";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonQuit
			// 
			this.buttonQuit.Location = new System.Drawing.Point(635, 14);
			this.buttonQuit.Name = "buttonQuit";
			this.buttonQuit.Size = new System.Drawing.Size(132, 25);
			this.buttonQuit.TabIndex = 96;
			this.buttonQuit.TabStop = false;
			this.buttonQuit.Text = "Quit";
			this.buttonQuit.UseVisualStyleBackColor = true;
			// 
			// comboBoxWeek
			// 
			this.comboBoxWeek.FormattingEnabled = true;
			this.comboBoxWeek.Items.AddRange(new object[] {
            "Week 1",
            "Week 2",
            "Week 3",
            "Week 4",
            "Week 5",
            "Week 6",
            "Week 7",
            "Week 8",
            "Week 9",
            "Week 10",
            "Week 11",
            "Week 12",
            "Week 13",
            "Week 14",
            "Week 15",
            "Week 16",
            "Week 17",
            "Week 18",
            "Week 19",
            "Week 20"});
			this.comboBoxWeek.Location = new System.Drawing.Point(12, 18);
			this.comboBoxWeek.Name = "comboBoxWeek";
			this.comboBoxWeek.Size = new System.Drawing.Size(161, 21);
			this.comboBoxWeek.TabIndex = 95;
			this.comboBoxWeek.TabStop = false;
			// 
			// dgvTimecardDetail
			// 
			this.dgvTimecardDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTimecardDetail.Location = new System.Drawing.Point(12, 58);
			this.dgvTimecardDetail.MultiSelect = false;
			this.dgvTimecardDetail.Name = "dgvTimecardDetail";
			this.dgvTimecardDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dgvTimecardDetail.Size = new System.Drawing.Size(754, 351);
			this.dgvTimecardDetail.TabIndex = 0;
			// 
			// TimecardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(779, 444);
			this.ControlBox = false;
			this.Controls.Add(this.dgvTimecardDetail);
			this.Controls.Add(this.comboBoxWeek);
			this.Controls.Add(this.buttonQuit);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonAddTask);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.statusStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "TimecardForm";
			this.Text = "Timecard -- Amanda Merrifield";
			this.Load += new System.EventHandler(this.TimecardForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvTimecardDetail)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.Button buttonAddTask;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonQuit;
		private System.Windows.Forms.ComboBox comboBoxWeek;
		private System.Windows.Forms.DataGridView dgvTimecardDetail;
	}
}