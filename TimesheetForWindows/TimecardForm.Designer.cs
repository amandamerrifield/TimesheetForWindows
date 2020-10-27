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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonAddTask = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.comboBoxWeek = new System.Windows.Forms.ComboBox();
			this.dgvTimecardDetail = new System.Windows.Forms.DataGridView();
			this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Monday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Tuesday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Wednesday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Thursday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Friday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Saturday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Sunday_Hrs = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvTimecardDetail)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 442);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(761, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(623, 14);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(107, 25);
			this.buttonUpdate.TabIndex = 97;
			this.buttonUpdate.TabStop = false;
			this.buttonUpdate.Text = "Save Changes";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonAddTask
			// 
			this.buttonAddTask.Location = new System.Drawing.Point(283, 14);
			this.buttonAddTask.Name = "buttonAddTask";
			this.buttonAddTask.Size = new System.Drawing.Size(107, 24);
			this.buttonAddTask.TabIndex = 99;
			this.buttonAddTask.TabStop = false;
			this.buttonAddTask.Text = "Append Task";
			this.buttonAddTask.UseVisualStyleBackColor = true;
			this.buttonAddTask.Click += new System.EventHandler(this.buttonAddTask_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(457, 14);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(107, 25);
			this.buttonCancel.TabIndex = 98;
			this.buttonCancel.TabStop = false;
			this.buttonCancel.Text = "Clear Changes";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// comboBoxWeek
			// 
			this.comboBoxWeek.FormattingEnabled = true;
			this.comboBoxWeek.Location = new System.Drawing.Point(22, 17);
			this.comboBoxWeek.Name = "comboBoxWeek";
			this.comboBoxWeek.Size = new System.Drawing.Size(138, 21);
			this.comboBoxWeek.TabIndex = 95;
			this.comboBoxWeek.TabStop = false;
			this.comboBoxWeek.SelectedIndexChanged += new System.EventHandler(this.comboBoxWeek_SelectedIndexChanged);
			// 
			// dgvTimecardDetail
			// 
			this.dgvTimecardDetail.AllowUserToAddRows = false;
			this.dgvTimecardDetail.AllowUserToDeleteRows = false;
			this.dgvTimecardDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTimecardDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskName,
            this.Monday_Hrs,
            this.Tuesday_Hrs,
            this.Wednesday_Hrs,
            this.Thursday_Hrs,
            this.Friday_Hrs,
            this.Saturday_Hrs,
            this.Sunday_Hrs});
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvTimecardDetail.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgvTimecardDetail.Location = new System.Drawing.Point(22, 58);
			this.dgvTimecardDetail.MultiSelect = false;
			this.dgvTimecardDetail.Name = "dgvTimecardDetail";
			this.dgvTimecardDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dgvTimecardDetail.Size = new System.Drawing.Size(708, 339);
			this.dgvTimecardDetail.TabIndex = 0;
			this.dgvTimecardDetail.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimecardDetail_CellValueChanged);
			// 
			// TaskName
			// 
			this.TaskName.DataPropertyName = "Task_Name";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.TaskName.DefaultCellStyle = dataGridViewCellStyle3;
			this.TaskName.HeaderText = "Task";
			this.TaskName.Name = "TaskName";
			this.TaskName.Width = 220;
			// 
			// Monday_Hrs
			// 
			this.Monday_Hrs.DataPropertyName = "Monday_Hrs";
			this.Monday_Hrs.HeaderText = "Monday";
			this.Monday_Hrs.Name = "Monday_Hrs";
			this.Monday_Hrs.Width = 60;
			// 
			// Tuesday_Hrs
			// 
			this.Tuesday_Hrs.DataPropertyName = "Tuesday_Hrs";
			this.Tuesday_Hrs.HeaderText = "Tuesday";
			this.Tuesday_Hrs.Name = "Tuesday_Hrs";
			this.Tuesday_Hrs.Width = 60;
			// 
			// Wednesday_Hrs
			// 
			this.Wednesday_Hrs.DataPropertyName = "Wednesday_Hrs";
			this.Wednesday_Hrs.HeaderText = "Wednsday";
			this.Wednesday_Hrs.Name = "Wednesday_Hrs";
			this.Wednesday_Hrs.Width = 60;
			// 
			// Thursday_Hrs
			// 
			this.Thursday_Hrs.DataPropertyName = "Thursday_Hrs";
			this.Thursday_Hrs.HeaderText = "Thursday";
			this.Thursday_Hrs.Name = "Thursday_Hrs";
			this.Thursday_Hrs.Width = 60;
			// 
			// Friday_Hrs
			// 
			this.Friday_Hrs.DataPropertyName = "Friday_Hrs";
			this.Friday_Hrs.HeaderText = "Friday";
			this.Friday_Hrs.Name = "Friday_Hrs";
			this.Friday_Hrs.Width = 60;
			// 
			// Saturday_Hrs
			// 
			this.Saturday_Hrs.DataPropertyName = "Saturday_Hrs";
			this.Saturday_Hrs.HeaderText = "Saturday";
			this.Saturday_Hrs.Name = "Saturday_Hrs";
			this.Saturday_Hrs.Width = 60;
			// 
			// Sunday_Hrs
			// 
			this.Sunday_Hrs.DataPropertyName = "Sunday_Hrs";
			this.Sunday_Hrs.HeaderText = "Sunday";
			this.Sunday_Hrs.Name = "Sunday_Hrs";
			this.Sunday_Hrs.Width = 60;
			// 
			// TimecardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(761, 464);
			this.ControlBox = false;
			this.Controls.Add(this.dgvTimecardDetail);
			this.Controls.Add(this.comboBoxWeek);
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
		private System.Windows.Forms.ComboBox comboBoxWeek;
		private System.Windows.Forms.DataGridView dgvTimecardDetail;
		private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Monday_Hrs;
		private System.Windows.Forms.DataGridViewTextBoxColumn Tuesday_Hrs;
		private System.Windows.Forms.DataGridViewTextBoxColumn Wednesday_Hrs;
		private System.Windows.Forms.DataGridViewTextBoxColumn Thursday_Hrs;
		private System.Windows.Forms.DataGridViewTextBoxColumn Friday_Hrs;
		private System.Windows.Forms.DataGridViewTextBoxColumn Saturday_Hrs;
		private System.Windows.Forms.DataGridViewTextBoxColumn Sunday_Hrs;
	}
}