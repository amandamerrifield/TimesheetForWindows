﻿using SsOpsDatabaseLibrary.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextValidationLibrary;
using static TextValidationLibrary.TextBoxValidator;
using System.CodeDom;
using SsOpsDatabaseLibrary;

namespace TimesheetForWindows
{
    public partial class SelectReportForm : Form
    {

		private const string TIMECARD_ROLLUP_1 = "Timecard Rollup Report";
		private const string EMPLOYEES_REPORT_1 = "List of All Current Employees";
        private const string TIMECARD_ROLLUP_02 = "Timecard Rollup Report For Employee by ID Number";
        private const string ACTIVETASK_BUDGET_SUMMARY = "All Active Tasks Budget Summary";

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
            lbxSelect.Items.Add(TIMECARD_ROLLUP_02);
            lbxSelect.Items.Add(ACTIVETASK_BUDGET_SUMMARY);

            lbxSelect.SelectedIndex = 0;


			//Put validators in all the textbox tags
			bool required = true;

			tbxYearNumber.Tag = new TextBoxValidator(tbxYearNumber, "Year Number", required, ValidationStyle.DigitsNotZero);
			tbxStartingWeekNbr.Tag = new TextBoxValidator(tbxStartingWeekNbr, "Starting Week Number", required, ValidationStyle.DigitsNotZero);
			tbxEndingWeekNbr.Tag = new TextBoxValidator(tbxEndingWeekNbr, "Ending Week Number", required, ValidationStyle.DigitsNotZero);

            //Second groupbox
            tbxYearNumber02.Tag = new TextBoxValidator(tbxYearNumber02, "Year Number", required, ValidationStyle.DigitsNotZero);
            tbxStartWeekNbr02.Tag = new TextBoxValidator(tbxStartWeekNbr02, "Starting Week Number", required, ValidationStyle.DigitsNotZero);
            tbxEndingWeekNbr02.Tag = new TextBoxValidator(tbxEndingWeekNbr02, "Ending Week Number", required, ValidationStyle.DigitsNotZero);
            tbxEmployeeIdNbr.Tag = new TextBoxValidator(tbxEmployeeIdNbr, "Employee Id Number", required, ValidationStyle.DigitsNotZero);
        }

        private void lbxSelect_SelectedIndexChanged(object sender, EventArgs e) {
            switch (lbxSelect.SelectedItem.ToString())
            {
                case TIMECARD_ROLLUP_1:
                    this.btnViewReport.Visible = false;
                    gbxRollup02.Visible = false;

                    this.Height = DEFAULT_HEIGHT + 120;
                    gbxTimecardRollup01.Top = 194;
                    gbxTimecardRollup01.Visible = true;
                    break;
                case EMPLOYEES_REPORT_1:
                    gbxRollup02.Visible = false;
                    gbxTimecardRollup01.Visible = false;
                    this.btnViewReport.Visible = true;
                    this.Height = DEFAULT_HEIGHT;
                    break;
                case ACTIVETASK_BUDGET_SUMMARY:
                    gbxRollup02.Visible = false;
                    gbxTimecardRollup01.Visible = false;
                    this.btnViewReport.Visible = true;
                    this.Height = DEFAULT_HEIGHT;
                    break;
                default:
                    gbxTimecardRollup01.Visible = false;
                    this.btnViewReport.Visible = false;
                    gbxRollup02.Visible = true;
                    gbxRollup02.Top = 194;
                    this.Height = DEFAULT_HEIGHT + 150;
                    break;

            }
        }

        private void btnViewReport_Click(object sender, EventArgs e) {
			// Some reports will require additional parameters
			LaunchReport(lbxSelect.SelectedItem.ToString());
		}

		private void btnTimecardRollup01_Click(object sender, EventArgs e) {
			LaunchReport(lbxSelect.SelectedItem.ToString());
		}
        private void btnLaunchTimecardRollup02_Click(object sender, EventArgs e)
        {
            LaunchReport(lbxSelect.SelectedItem.ToString());
        }
        //======================================
        #region "Helper Functions"

        private void LaunchReport(string reportname) {
			//MessageBox.Show(reportname);
			bool isGood = true;
            short yearNo;
            short startWeekNo;
            short endWeekNo;
            int employeeIdNo;

            if (reportname == TIMECARD_ROLLUP_1) {
				// Validate the contents of the textbox controls in the gbxTimecardRollup01 group box
				isGood = ValidateGroupBox(gbxTimecardRollup01);
                yearNo = Convert.ToInt16(tbxYearNumber.Text);
                startWeekNo = Convert.ToInt16(tbxStartingWeekNbr.Text);
                endWeekNo = Convert.ToInt16(tbxEndingWeekNbr.Text);
                if (isGood) {
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
            if (reportname == ACTIVETASK_BUDGET_SUMMARY)
            {
                using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
                {
                    List<SsOpsDatabaseLibrary.Entity.Task> tasks = dbLib.GetActiveTasksBudgetSummary();
                    ReportDisplayForm displayer = new ReportDisplayForm(tasks.ToArray());
                    Size targetSize = new Size(1000, 1000);
                    displayer.Size = targetSize;
                    displayer.Show();
                }
            }
            if (reportname == TIMECARD_ROLLUP_02)
            {
                isGood = ValidateGroupBox(gbxRollup02);
                yearNo = Convert.ToInt16(tbxYearNumber02.Text);
                startWeekNo = Convert.ToInt16(tbxStartWeekNbr02.Text);
                endWeekNo = Convert.ToInt16(tbxEndingWeekNbr02.Text);
                employeeIdNo = Convert.ToInt32(tbxEmployeeIdNbr.Text);
                if (isGood)
                {
                    if (!(yearNo > 2019 && yearNo < 2030))
                    {
                        MessageBox.Show("Please enter a year between 2019 and 2030.", "Attention");
                        return;
                    }
                    if (startWeekNo < 1 || startWeekNo > 53)
                    {
                        MessageBox.Show("Starting week number must be between 1 and 53.", "Attention");
                        return;
                    }
                    if (endWeekNo < 1 || endWeekNo > 53)
                    {
                        MessageBox.Show("Ending week number must be between 1 and 53.", "Attention");
                        return;
                    }
                    if (startWeekNo >= endWeekNo)
                    {
                        MessageBox.Show("Ending week number must be bigger than starting week number.", "Attention");
                        return;
                    }
                    if (employeeIdNo < 100 || employeeIdNo > 999)
                    {
                        MessageBox.Show("Employee ID number should be greater than 100 and less than 1000", "Attention");
                        return;
                    }
					//MessageBox.Show("Validation Passed");

					using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter()) {
						List<ReportTimeCardRollup01> records = dbLib.GetTimecardRollupForEmployee(yearNo, startWeekNo, endWeekNo, employeeIdNo);
						ReportTimeCardRollup01[] rollupArray = records.ToArray();
                        Employee emp = dbLib.GetEmployeeById(employeeIdNo);
                        string employeeFullName = emp.FirstName + " " + emp.LastName;  
						// Construct the ReportDisplayForm and show it on screen.
						string[] parametersForReport = { tbxYearNumber02.Text, tbxStartWeekNbr02.Text, tbxEndingWeekNbr02.Text, tbxEmployeeIdNbr.Text, employeeFullName };
						ReportDisplayForm displayer = new ReportDisplayForm(rollupArray, parametersForReport);
						Size targetSize = new Size(1000, 1000);
						displayer.Size = targetSize;
						displayer.Show();
					}
				}
            }
		}
		private bool ValidateGroupBox(GroupBox gbx) {
			List<string> problemMessages = new List<string>();

			//Perform validation on every control in the groupbox
			foreach (Control ctrl in gbx.Controls) {
				if (ctrl.Tag != null && ctrl.Visible == true) {
					if (ctrl.Tag is TextBoxValidator) {
						var validatr = (TextBoxValidator)ctrl.Tag;
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
