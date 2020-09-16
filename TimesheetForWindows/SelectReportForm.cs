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
        private enum FormState
        {
            Loading,
            ViewingData,
            ViewingPotentialChanges,
            SavingChanges
        }
        private Employee _employee;
        private FormState _currentFormState;
        public SelectReportForm(Employee targetEmployee) {
            InitializeComponent();
            // We will manually control the form location on screen
            this.StartPosition = FormStartPosition.Manual;

            // Get a copy of the employee key
            _employee = targetEmployee;
        }

        private void SelectReportForm_Load(object sender, EventArgs e) {
            _currentFormState = FormState.Loading;
            lbxSelect.Items.Add("First Report");
            lbxSelect.Items.Add("Second Report");
            _currentFormState = FormState.ViewingData;
        }

        private void lbxSelect_SelectedIndexChanged(object sender, EventArgs e) {
            if (_currentFormState == FormState.Loading) {
                return;
            }
            LaunchReport(lbxSelect.SelectedItem.ToString());
        }

        private void btnViewReport_Click(object sender, EventArgs e) {
            LaunchReport(lbxSelect.SelectedItem.ToString());
        }

        private void LaunchReport(string reportname) {
            //TODO: Launch report
            //if (reportname == "report1")
            MessageBox.Show(reportname);
        }
    }
}
