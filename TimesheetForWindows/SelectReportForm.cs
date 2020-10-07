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

        private Employee _employee;
        public SelectReportForm(Employee targetEmployee) {
            InitializeComponent();
            // We will manually control the form location on screen
            this.StartPosition = FormStartPosition.Manual;

            // Get a copy of the employee key
            _employee = targetEmployee;
        }

        private void SelectReportForm_Load(object sender, EventArgs e) {
            lbxSelect.Items.Add("Timecard Rollup Report");
            lbxSelect.Items.Add("TBD Report");
        }

        private void lbxSelect_SelectedIndexChanged(object sender, EventArgs e) {
            //LaunchReport(lbxSelect.SelectedItem.ToString());
        }

        private void btnViewReport_Click(object sender, EventArgs e) {
            LaunchReport(lbxSelect.SelectedItem.ToString());
        }

        private void LaunchReport(string reportname) {
			//MessageBox.Show(reportname);
			if(reportname == "Timecard Rollup Report") {
				// Ask the user for a date range and the year

			}

        }
    }
}
