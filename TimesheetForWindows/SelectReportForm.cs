using SsOpsDatabaseLibrary.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DecoratorsLibrary;
using static DecoratorsLibrary.ControlTextValidator;
using System.CodeDom;
using SsOpsDatabaseLibrary;

namespace TimesheetForWindows
{
    public partial class SelectReportForm : Form
    {

		private const string TIMECARD_ROLLUP_1 = "Timecard Rollup Report";
		private const string EMPLOYEES_REPORT_1 = "Employees Report";
		private const int DEFAULT_HEIGHT = 334;

		private Employee _employee;

        public SelectReportForm(Employee targetEmployee) {
            InitializeComponent();
            // We will manually control the form location on screen
            this.StartPosition = FormStartPosition.Manual;

            // Get a copy of the employee key
            _employee = targetEmployee;

			// Set the default form height
			this.Height = DEFAULT_HEIGHT;
		}

		private void SelectReportForm_Load(object sender, EventArgs e) {
			lbxSelect.Items.Add(EMPLOYEES_REPORT_1);
			lbxSelect.Items.Add(TIMECARD_ROLLUP_1);
			lbxSelect.SelectedIndex = 0;

			//Put validators in all the textbox tags
			bool required = true;

			tbxYearNumber.Tag = new ControlTextValidator(tbxYearNumber, "Year Number", required, ValidationStyle.DigitsNotZero);
			tbxStartingWeekNbr.Tag = new ControlTextValidator(tbxStartingWeekNbr, "Starting Week Number", required, ValidationStyle.DigitsNotZero);
			tbxEndingWeekNbr.Tag = new ControlTextValidator(tbxEndingWeekNbr, "Ending Week Number", required, ValidationStyle.DigitsNotZero);
		}

        private void lbxSelect_SelectedIndexChanged(object sender, EventArgs e) {
			//LaunchReport(lbxSelect.SelectedItem.ToString());
			if (lbxSelect.SelectedItem.ToString() == TIMECARD_ROLLUP_1) {
				this.btnViewReport.Visible = false;
				this.Height = DEFAULT_HEIGHT + 120;
				gbxTimecardRollup01.Top = 194;
				gbxTimecardRollup01.Visible = true;
			}
			else {
				this.btnViewReport.Visible = true;
				this.Height = 272;
				gbxTimecardRollup01.Top = 240;
				gbxTimecardRollup01.Visible = true;
			}
		}

        private void btnViewReport_Click(object sender, EventArgs e) {
			// Some reports will require additional parameters
			LaunchReport(lbxSelect.SelectedItem.ToString());
		}

		private void btnTimecardRollup01_Click(object sender, EventArgs e) {
			LaunchReport(lbxSelect.SelectedItem.ToString());
		}

		//======================================
		#region "Helper Functions"

		private void LaunchReport(string reportname) {
			//MessageBox.Show(reportname);
			bool isGood = true;
			if (reportname == TIMECARD_ROLLUP_1) {
				// Validate the contents of the textbox controls in the gbxTimecardRollup01 group box
				isGood = ValidateGroupBox(gbxTimecardRollup01);
				if (isGood) {
					short yearNo = Convert.ToInt16(tbxYearNumber.Text);
					short startWeekNo = Convert.ToInt16(tbxStartingWeekNbr.Text);
					short endWeekNo = Convert.ToInt16(tbxEndingWeekNbr.Text);
					if (!(yearNo > 2019 && yearNo < 2030)) {
						MessageBox.Show("Please enter a year between 2019 and 2030.", "Attention");
						return;
					}
					if (startWeekNo < 1 || startWeekNo > 53) {
						MessageBox.Show("Starting week number must be between 1 and 53.", "Attention");
						return;
					}
					if (endWeekNo < 1 || endWeekNo > 53) {
						MessageBox.Show("Ending week number must be between 1 and 53.", "Attention");
						return;
					}
					if (startWeekNo >= endWeekNo) {
						MessageBox.Show("Ending week number must be bigger than starting week number.", "Attention");
						return;
					}
					// Validated okay.  Now get report data from our database adapter
					using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter()) {
						List<ReportTimeCardRollup01> records = dbLib.GetTimecardRollup(yearNo, startWeekNo, endWeekNo);
						ReportTimeCardRollup01[] rollupArray = records.ToArray();
						// Construct the ReportDisplayForm and show it on screen.
						string[] parametersForReport = { tbxYearNumber.Text, tbxStartingWeekNbr.Text, tbxEndingWeekNbr.Text };
						ReportDisplayForm displayer = new ReportDisplayForm(rollupArray, parametersForReport);
						Size targetSize = new Size(1000, 1000);
						displayer.Size = targetSize;
						displayer.Show();
                    }
				}
			}
			if (reportname == EMPLOYEES_REPORT_1) {
				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter()) {
					List<Employee> records = dbLib.GetAllCurrentEmployees();
					ReportDisplayForm displayer = new ReportDisplayForm(records.ToArray());
					Size targetSize = new Size(800, 500);
					displayer.Size = targetSize;
					displayer.Show();
				}
			}

		}
		private bool ValidateGroupBox(GroupBox gbx) {
			List<string> problemMessages = new List<string>();

			//Perform validation on every control in the groupbox
			foreach (Control ctrl in gbx.Controls) {
				if (ctrl.Tag != null) {
					if (ctrl.Tag is ControlTextValidator) {
						var validatr = (ControlTextValidator)ctrl.Tag;
						string validationMsg = validatr.ValidationMsg();
						if (validationMsg.Length > 0) {
							// failed validation...
							problemMessages.Add(validationMsg);
						}
					}
				}
			}

			if (problemMessages.Count > 0) {
				foreach (string problem in problemMessages) {
					MessageBox.Show(problem, "Attention", MessageBoxButtons.OK);
				}
				return false;
			}
			return true;
		}
		#endregion


	}
}
