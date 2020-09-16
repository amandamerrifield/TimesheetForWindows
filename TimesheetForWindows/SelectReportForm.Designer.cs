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
            this.SuspendLayout();
            // 
            // lbxSelect
            // 
            this.lbxSelect.FormattingEnabled = true;
            this.lbxSelect.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.lbxSelect.Location = new System.Drawing.Point(96, 70);
            this.lbxSelect.Name = "lbxSelect";
            this.lbxSelect.Size = new System.Drawing.Size(693, 368);
            this.lbxSelect.TabIndex = 0;
            this.lbxSelect.SelectedIndexChanged += new System.EventHandler(this.lbxSelect_SelectedIndexChanged);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(96, 21);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(144, 30);
            this.btnViewReport.TabIndex = 1;
            this.btnViewReport.Text = "View Selected Report";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // SelectReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnViewReport);
            this.Controls.Add(this.lbxSelect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectReportForm";
            this.Text = "Select Report";
            this.Load += new System.EventHandler(this.SelectReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxSelect;
        private System.Windows.Forms.Button btnViewReport;
    }
}