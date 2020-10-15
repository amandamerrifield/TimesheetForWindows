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
using Microsoft.Reporting.WinForms;

namespace TimesheetForWindows
{
	public partial class ReportDisplayForm : Form
	{
		// This is the array version of the List queried from the database
		private ReportTimeCardRollup01[] _rollupDataArray;
		private Employee[] _allEmployeesArray;
		private BindingSource _bindingSource;
		private string _requestedReport;

		public ReportDisplayForm(Employee[] allEmployees) {
			InitializeComponent();
			_requestedReport = "AllEmployees";
			_allEmployeesArray = allEmployees;
			_bindingSource = new BindingSource();
		}
		public ReportDisplayForm(ReportTimeCardRollup01[] tcRollup) {
			InitializeComponent();
			_requestedReport = "TimecardRollup";
			_rollupDataArray = tcRollup;
			_bindingSource = new BindingSource();
		}

		private void ReportDisplayForm_Load(object sender, EventArgs e) {
			reportViewer1.ProcessingMode = ProcessingMode.Local;

			if (_requestedReport == "TimecardRollup") {
				reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\Report3.rdlc";

				_bindingSource.Clear();
				_bindingSource.DataSource = _rollupDataArray;
			}
			if (_requestedReport == "AllEmployees") {
				reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\Report4.rdlc";
				reportViewer1.LocalReport.DisplayName = "All Employees Report";

				_bindingSource.Clear();
				_bindingSource.DataSource = _allEmployeesArray;
			}

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
