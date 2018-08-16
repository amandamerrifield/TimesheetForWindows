namespace TimesheetForWindows
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDefineTaskCategories = new System.Windows.Forms.Button();
            this.btnDefineTasks = new System.Windows.Forms.Button();
            this.btnEnterHours = new System.Windows.Forms.Button();
            this.btnViewReports = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(163, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 330);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TimesheetForWindows.Properties.Resources.WeeklyTimeTracker;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(261, 328);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnDefineTaskCategories
            // 
            this.btnDefineTaskCategories.Location = new System.Drawing.Point(12, 17);
            this.btnDefineTaskCategories.Name = "btnDefineTaskCategories";
            this.btnDefineTaskCategories.Size = new System.Drawing.Size(135, 40);
            this.btnDefineTaskCategories.TabIndex = 3;
            this.btnDefineTaskCategories.Text = "Define Task Categories";
            this.btnDefineTaskCategories.UseVisualStyleBackColor = true;
            // 
            // btnDefineTasks
            // 
            this.btnDefineTasks.Location = new System.Drawing.Point(12, 72);
            this.btnDefineTasks.Name = "btnDefineTasks";
            this.btnDefineTasks.Size = new System.Drawing.Size(135, 40);
            this.btnDefineTasks.TabIndex = 4;
            this.btnDefineTasks.Text = "Define Tasks";
            this.btnDefineTasks.UseVisualStyleBackColor = true;
            // 
            // btnEnterHours
            // 
            this.btnEnterHours.Location = new System.Drawing.Point(12, 128);
            this.btnEnterHours.Name = "btnEnterHours";
            this.btnEnterHours.Size = new System.Drawing.Size(135, 40);
            this.btnEnterHours.TabIndex = 5;
            this.btnEnterHours.Text = "Enter Hours";
            this.btnEnterHours.UseVisualStyleBackColor = true;
            this.btnEnterHours.Click += new System.EventHandler(this.btnEnterHours_Click);
            // 
            // btnViewReports
            // 
            this.btnViewReports.Location = new System.Drawing.Point(12, 185);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Size = new System.Drawing.Size(135, 40);
            this.btnViewReports.TabIndex = 6;
            this.btnViewReports.Text = "View Reports";
            this.btnViewReports.UseVisualStyleBackColor = true;
            this.btnViewReports.Click += new System.EventHandler(this.btnViewReports_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 298);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 40);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit Timesheet Plus!";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 353);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnViewReports);
            this.Controls.Add(this.btnEnterHours);
            this.Controls.Add(this.btnDefineTasks);
            this.Controls.Add(this.btnDefineTaskCategories);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = " == Timesheet For Windows ==";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDefineTaskCategories;
        private System.Windows.Forms.Button btnDefineTasks;
        private System.Windows.Forms.Button btnEnterHours;
        private System.Windows.Forms.Button btnViewReports;
        private System.Windows.Forms.Button btnExit;

    }
}