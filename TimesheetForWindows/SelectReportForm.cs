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

namespace TimesheetForWindows
{
    public partial class SelectReportForm : Form
    {

		private const string TIMECARD_ROLLUP_1 = "Timecard Rollup Report";

		private Employee _employee;

        public SelectReportForm(Employee targetEmployee) {
            InitializeComponent();
            // We will manually control the form location on screen
            this.StartPosition = FormStartPosition.Manual;

            // Get a copy of the employee key
            _employee = targetEmployee;

			// Set the default form height
			this.Height = 334;
        }

        private void SelectReportForm_Load(object sender, EventArgs e) {
            lbxSelect.Items.Add(TIMECARD_ROLLUP_1);
            lbxSelect.Items.Add("TBD Report");
        }

        private void lbxSelect_SelectedIndexChanged(object sender, EventArgs e) {
			//LaunchReport(lbxSelect.SelectedItem.ToString());
			if (lbxSelect.SelectedItem.ToString() == TIMECARD_ROLLUP_1) {
				this.btnViewReport.Visible = false;
				this.Height = 422;
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
			if(lbxSelect.SelectedItem.ToString() == TIMECARD_ROLLUP_1) {


			}

        }

        private void LaunchReport(string reportname) {
			//MessageBox.Show(reportname);
			if(reportname == "Timecard Rollup Report") {
				// Ask the user for a date range and the year
				// ToDo: Validate the contents of the textboxes before we run the report
				// Should it be a Digits Validator?

			}

        }

		private void btnTimecardRollup01_Click(object sender, EventArgs e) {

		}
	}
}
