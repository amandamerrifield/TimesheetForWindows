using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SsOpsDatabaseLibrary.Entity;
using SsOpsDatabaseLibrary;
using Microsoft.Reporting.WinForms;

namespace TimesheetForWindows
{
	public partial class ReportDisplayForm : Form
	{
		// This is the array version of the List queried from the database
		private SsOpsDatabaseLibrary.Entity.ReportTimeCardRollup01[] _rollupDataArray;
		private BindingSource _bindingSource;

		public ReportDisplayForm(ReportTimeCardRollup01[] tcRollup) {
			InitializeComponent();
			_rollupDataArray = tcRollup;
			_bindingSource = new BindingSource();
			Width = 1000;
			Height = 1000;
		}

		private void ReportDisplayForm_Load(object sender, EventArgs e) {
			reportViewer1.ProcessingMode = ProcessingMode.Local;
			reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\Report3.rdlc";

			_bindingSource.Clear();
			_bindingSource.DataSource = _rollupDataArray;
			_bindingSource.ResetBindings(false);

			ReportDataSource rds = new ReportDataSource();
			rds.Name = "DataSet1";
			rds.Value = _bindingSource;
			reportViewer1.LocalReport.DataSources.Clear();
			reportViewer1.LocalReport.DataSources.Add(rds);

			this.reportViewer1.RefreshReport();

		}
	}
}
