namespace TimesheetForWindows
{
	partial class ReportDisplayForm
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
			this.components = new System.ComponentModel.Container();
			Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
			this.reportDisplayFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.ReportTimeCardRollup01BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
			((System.ComponentModel.ISupportInitialize)(this.reportDisplayFormBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ReportTimeCardRollup01BindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// reportDisplayFormBindingSource
			// 
			this.reportDisplayFormBindingSource.DataSource = typeof(TimesheetForWindows.ReportDisplayForm);
			// 
			// ReportTimeCardRollup01BindingSource
			// 
			this.ReportTimeCardRollup01BindingSource.DataSource = typeof(SsOpsDatabaseLibrary.Entity.ReportTimeCardRollup01);
			// 
			// reportViewer1
			// 
			this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			reportDataSource1.Name = "DataSet1";
			reportDataSource1.Value = this.reportDisplayFormBindingSource;
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
			this.reportViewer1.LocalReport.ReportEmbeddedResource = "TimesheetForWindows.Report3.rdlc";
			this.reportViewer1.Location = new System.Drawing.Point(0, 0);
			this.reportViewer1.Name = "reportViewer1";
			this.reportViewer1.Size = new System.Drawing.Size(1039, 645);
			this.reportViewer1.TabIndex = 0;
			// 
			// ReportDisplayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1039, 645);
			this.Controls.Add(this.reportViewer1);
			this.Name = "ReportDisplayForm";
			this.Text = "Timesheet for Windows Report Displayer";
			this.Load += new System.EventHandler(this.ReportDisplayForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.reportDisplayFormBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ReportTimeCardRollup01BindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private System.Windows.Forms.BindingSource ReportTimeCardRollup01BindingSource;
		private System.Windows.Forms.BindingSource reportDisplayFormBindingSource;
	}
}