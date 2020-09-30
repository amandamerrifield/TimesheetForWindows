namespace TimesheetForWindows
{
	partial class DefineTasksForm
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.btnClearChanges = new System.Windows.Forms.Button();
			this.btnCancelChanges = new System.Windows.Forms.Button();
			this.btnSaveChanges = new System.Windows.Forms.Button();
			this.cbxCategories = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column6,
            this.Column1,
            this.Column9,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(761, 545);
			this.dataGridView1.TabIndex = 0;
			// 
			// Column8
			// 
			this.Column8.DataPropertyName = "CategoryId";
			this.Column8.HeaderText = "Category Id";
			this.Column8.Name = "Column8";
			this.Column8.ReadOnly = true;
			this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column8.Visible = false;
			// 
			// Column6
			// 
			this.Column6.DataPropertyName = "TaskId";
			this.Column6.HeaderText = "Task Id";
			this.Column6.Name = "Column6";
			this.Column6.ReadOnly = true;
			this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column6.Visible = false;
			// 
			// Column1
			// 
			this.Column1.DataPropertyName = "TaskName";
			this.Column1.HeaderText = "Task";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 250;
			// 
			// Column9
			// 
			this.Column9.DataPropertyName = "CategoryName";
			this.Column9.HeaderText = "Category";
			this.Column9.Name = "Column9";
			this.Column9.ReadOnly = true;
			this.Column9.Width = 150;
			// 
			// Column2
			// 
			this.Column2.DataPropertyName = "BudgetHours";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
			this.Column2.HeaderText = "Budget Hours";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 70;
			// 
			// Column3
			// 
			this.Column3.DataPropertyName = "ActualHours";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
			this.Column3.HeaderText = "Actual Hours";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Width = 70;
			// 
			// Column4
			// 
			this.Column4.DataPropertyName = "StartDate";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
			this.Column4.HeaderText = "Start Date";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.Column4.Width = 80;
			// 
			// Column5
			// 
			this.Column5.DataPropertyName = "EndDate";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.Column5.DefaultCellStyle = dataGridViewCellStyle4;
			this.Column5.HeaderText = "End Date";
			this.Column5.Name = "Column5";
			this.Column5.ReadOnly = true;
			this.Column5.Width = 80;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(29, 601);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(256, 20);
			this.textBox1.TabIndex = 1;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(528, 601);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(69, 20);
			this.textBox2.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(26, 585);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "Add New Task";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(525, 585);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Budget Hours";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(705, 653);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(65, 20);
			this.textBox3.TabIndex = 9;
			this.textBox3.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(302, 585);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "to Category";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(688, 585);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 23;
			this.label4.Text = "EndDate";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(600, 585);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "StartDate";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(603, 601);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(79, 20);
			this.textBox4.TabIndex = 4;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(691, 601);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(79, 20);
			this.textBox5.TabIndex = 5;
			// 
			// btnClearChanges
			// 
			this.btnClearChanges.Location = new System.Drawing.Point(87, 650);
			this.btnClearChanges.Name = "btnClearChanges";
			this.btnClearChanges.Size = new System.Drawing.Size(176, 23);
			this.btnClearChanges.TabIndex = 6;
			this.btnClearChanges.Text = "Clear Changes";
			this.btnClearChanges.UseVisualStyleBackColor = true;
			this.btnClearChanges.Click += new System.EventHandler(this.btnClearChanges_Click);
			// 
			// btnCancelChanges
			// 
			this.btnCancelChanges.Location = new System.Drawing.Point(305, 650);
			this.btnCancelChanges.Name = "btnCancelChanges";
			this.btnCancelChanges.Size = new System.Drawing.Size(166, 23);
			this.btnCancelChanges.TabIndex = 7;
			this.btnCancelChanges.Text = "Cancel Changes";
			this.btnCancelChanges.UseVisualStyleBackColor = true;
			this.btnCancelChanges.Click += new System.EventHandler(this.btnCancelChanges_Click);
			// 
			// btnSaveChanges
			// 
			this.btnSaveChanges.Location = new System.Drawing.Point(505, 650);
			this.btnSaveChanges.Name = "btnSaveChanges";
			this.btnSaveChanges.Size = new System.Drawing.Size(171, 23);
			this.btnSaveChanges.TabIndex = 8;
			this.btnSaveChanges.Text = "Save Changes";
			this.btnSaveChanges.UseVisualStyleBackColor = true;
			this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
			// 
			// cbxCategories
			// 
			this.cbxCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxCategories.FormattingEnabled = true;
			this.cbxCategories.Location = new System.Drawing.Point(305, 601);
			this.cbxCategories.Name = "cbxCategories";
			this.cbxCategories.Size = new System.Drawing.Size(211, 21);
			this.cbxCategories.TabIndex = 2;
			// 
			// DefineTasksForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 726);
			this.ControlBox = false;
			this.Controls.Add(this.cbxCategories);
			this.Controls.Add(this.btnSaveChanges);
			this.Controls.Add(this.btnCancelChanges);
			this.Controls.Add(this.btnClearChanges);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.dataGridView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DefineTasksForm";
			this.Text = "Define Tasks Form";
			this.Load += new System.EventHandler(this.DefineTasksForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Button btnClearChanges;
		private System.Windows.Forms.Button btnCancelChanges;
		private System.Windows.Forms.Button btnSaveChanges;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.ComboBox cbxCategories;
	}
}