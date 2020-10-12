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

		private Employee _employee;

		private const int DEFAULT_HEIGHT = 334;

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
            lbxSelect.Items.Add(TIMECARD_ROLLUP_1);
            lbxSelect.Items.Add("TBD Report");
			lbxSelect.SelectedIndex = 0;


			//Get textbox tags
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
            // Some reports will require additional user parameters
			if(lbxSelect.SelectedItem.ToString() == "TBD Report") {
				MessageBox.Show("TBD Report is running");
			}

        }

		private void btnTimecardRollup01_Click(object sender, EventArgs e) {
			LaunchReport(lbxSelect.SelectedItem.ToString());
		}

		//======================================
		#region "Helper Functions"

		private void LaunchReport(string reportname) {
			//MessageBox.Show(reportname);
			bool isGood = true;
			if (reportname == "Timecard Rollup Report") {
				isGood = ValidateGroupBox(gbxTimecardRollup01);
				// Create a ValidateControlBox and pass the control box
				if (isGood) {
					int yearNo = Convert.ToInt32(tbxYearNumber.Text);
					short startWeekNo = Convert.ToInt16(tbxStartingWeekNbr.Text);
					short endWeekNo = Convert.ToInt16(tbxEndingWeekNbr.Text);
					if (!(yearNo > 2019 && yearNo < 2030)) {
						MessageBox.Show("Enter a year between 2019 and 2030.", "Attention");
						return;
					}
					if(startWeekNo >= endWeekNo) {
						MessageBox.Show("Ending week number should be bigger than starting week number.", "Attention");
						return;
					}
					using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter()) {
						List<ReportTimeCardRollup01> records = dbLib.GetTimecardRollup(tbxYearNumber.Text, startWeekNo, endWeekNo);
						//TODO: Bind the data
                    }
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
