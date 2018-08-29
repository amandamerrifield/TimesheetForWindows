namespace TimesheetForWindows
{
	partial class TaskCategoriesForm
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnSaveChanges = new System.Windows.Forms.Button();
			this.btnClearChanges = new System.Windows.Forms.Button();
			this.btnCancelChanges = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(66, 431);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(220, 20);
			this.textBox1.TabIndex = 0;
			// 
			// btnSaveChanges
			// 
			this.btnSaveChanges.Location = new System.Drawing.Point(453, 481);
			this.btnSaveChanges.Name = "btnSaveChanges";
			this.btnSaveChanges.Size = new System.Drawing.Size(171, 23);
			this.btnSaveChanges.TabIndex = 5;
			this.btnSaveChanges.Text = "Save Changes";
			this.btnSaveChanges.UseVisualStyleBackColor = true;
			this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
			// 
			// btnClearChanges
			// 
			this.btnClearChanges.Location = new System.Drawing.Point(66, 481);
			this.btnClearChanges.Name = "btnClearChanges";
			this.btnClearChanges.Size = new System.Drawing.Size(176, 23);
			this.btnClearChanges.TabIndex = 3;
			this.btnClearChanges.Text = "Clear Changes";
			this.btnClearChanges.UseVisualStyleBackColor = true;
			this.btnClearChanges.Click += new System.EventHandler(this.btnClearChanges_Click);
			// 
			// btnCancelChanges
			// 
			this.btnCancelChanges.Location = new System.Drawing.Point(266, 481);
			this.btnCancelChanges.Name = "btnCancelChanges";
			this.btnCancelChanges.Size = new System.Drawing.Size(166, 23);
			this.btnCancelChanges.TabIndex = 4;
			this.btnCancelChanges.Text = "Cancel Changes";
			this.btnCancelChanges.UseVisualStyleBackColor = true;
			this.btnCancelChanges.Click += new System.EventHandler(this.btnCancelChanges_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(303, 431);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(321, 20);
			this.textBox2.TabIndex = 1;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.CategoryId});
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(776, 377);
			this.dataGridView1.TabIndex = 15;
			// 
			// Column1
			// 
			this.Column1.DataPropertyName = "CategoryName";
			this.Column1.HeaderText = "Category Name";
			this.Column1.Name = "Column1";
			this.Column1.Width = 250;
			// 
			// Column2
			// 
			this.Column2.DataPropertyName = "CategoryDescription";
			this.Column2.HeaderText = "Category Description";
			this.Column2.Name = "Column2";
			this.Column2.Width = 400;
			// 
			// Column3
			// 
			this.Column3.DataPropertyName = "IsOverheadYn";
			this.Column3.HeaderText = "Is Overhead Y/N";
			this.Column3.Name = "Column3";
			this.Column3.Width = 60;
			// 
			// CategoryId
			// 
			this.CategoryId.DataPropertyName = "CategoryId";
			this.CategoryId.HeaderText = "CategoryId";
			this.CategoryId.Name = "CategoryId";
			this.CategoryId.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(300, 415);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Add New Category Description";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(63, 415);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Add New Category Name";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.TopRight;
			this.checkBox1.Location = new System.Drawing.Point(639, 434);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(84, 17);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Is Overhead";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// TaskCategoriesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(803, 516);
			this.ControlBox = false;
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.btnCancelChanges);
			this.Controls.Add(this.btnClearChanges);
			this.Controls.Add(this.btnSaveChanges);
			this.Controls.Add(this.textBox1);
			this.Name = "TaskCategoriesForm";
			this.Text = "Viewing Task Categories";
			this.Load += new System.EventHandler(this.TaskCategoriesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnSaveChanges;
		private System.Windows.Forms.Button btnClearChanges;
		private System.Windows.Forms.Button btnCancelChanges;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn CategoryId;
	}
}