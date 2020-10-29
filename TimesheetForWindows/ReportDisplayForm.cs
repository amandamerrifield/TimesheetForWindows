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
		private string[] _reportParms;
        private SsOpsDatabaseLibrary.Entity.Task[] _allTasksArray;

		public ReportDisplayForm(Employee[] allEmployees) {
			InitializeComponent();
			_requestedReport = "AllEmployees";
			_allEmployeesArray = allEmployees;
			_bindingSource = new BindingSource();
		}
		public ReportDisplayForm(ReportTimeCardRollup01[] tcRollup, string[] reportParameters) {
			InitializeComponent();
			_requestedReport = "TimecardRollup";
			_rollupDataArray = tcRollup;
			_reportParms = reportParameters;
			_bindingSource = new BindingSource();
		}
        public ReportDisplayForm(SsOpsDatabaseLibrary.Entity.Task[] allActiveTasks)
        {
            InitializeComponent();
            _allTasksArray = allActiveTasks;
            _requestedReport = "GetAllActiveTasks";
            _bindingSource = new BindingSource();
        }
		private void ReportDisplayForm_Load(object sender, EventArgs e) {
			reportViewer1.ProcessingMode = ProcessingMode.Local;

			if (_requestedReport == "TimecardRollup") {
				List<ReportParameter> rpList = new List<ReportParameter>();
				rpList.Add(new ReportParameter("ParamReportYear", "Year " + _reportParms[0]));
				rpList.Add(new ReportParameter("ParamReportWeekBegin", "Week " + _reportParms[1]));
				rpList.Add(new ReportParameter("ParamReportWeekEnd", "Week " + _reportParms[2]));

				// If we have 4 parameters then we are to display the Employee Rollup
				if (_reportParms.Length > 3) {
					rpList.Add(new ReportParameter("ParamReportEmployee", "Employee " + _reportParms[3] + " " + _reportParms[4]));
					reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\Report5.rdlc";
					
				} else {
					// We are to display the Timecard Rollup Report
					reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\Report3.rdlc";
				}
				reportViewer1.LocalReport.SetParameters(rpList.ToArray());

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
