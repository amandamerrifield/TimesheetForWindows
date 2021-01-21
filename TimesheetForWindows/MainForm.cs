using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using SsOpsDatabaseLibrary;
using SsOpsDatabaseLibrary.Entity;
using System.DirectoryServices.AccountManagement;

namespace TimesheetForWindows
{
    
    public partial class MainForm : Form
    {
        private Form _timecardForm;
        private Form _currentActiveForm;
		private Employee _employee;
		private Form _taskcategoriesform;
		private DefineTasksForm _DefineTasksForm;
		private SelectReportForm _selectReportForm;

		// MainForm Constructor
		public MainForm() : base()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
			var thisMethod = MethodBase.GetCurrentMethod();
			Console.Write("****" + thisMethod.Name + "\n");

			string[] usr = { "Unknown", "Fetterhoff" };

			UserPrincipal upCurrent = UserPrincipal.Current;
			// Windows 10 is not returning DisplayName correctly
			if(String.IsNullOrEmpty(upCurrent.DisplayName) || !(UserPrincipal.Current.DisplayName.Contains(" "))) {
				usr[0] = upCurrent.Name;
			}
			else {
				usr = UserPrincipal.Current.DisplayName.Split(' ');
			}

			this.Text = "Timesheet for Windows  --  " + usr[0] + " " + usr[1];

			using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
			{
				//The current employeeId is the current user's employee ID
				try
				{
					_employee = dbLib.GetEmployeeByName(usr[0], usr[1]);
				}
				catch (Exception ex)
				{
                    string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
                    MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
                    Application.Exit();
                }
			}
			// Instantiate the forms that this MainForm controls.
			_timecardForm = new TimecardForm(_employee);
			_timecardForm.Visible = false;
			_taskcategoriesform = new TaskCategoriesForm();
			_taskcategoriesform.Visible = false;
			_DefineTasksForm = new DefineTasksForm();
			_DefineTasksForm.Visible = false;
			_selectReportForm = new SelectReportForm(_employee);
			_selectReportForm.Visible = false;

            // The current active form is the one the user is working
            _currentActiveForm = null;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            var thisMethod = MethodBase.GetCurrentMethod();
            Console.Write("****" + thisMethod.Name + "\n");

            if(_currentActiveForm != null) _currentActiveForm.Visible = false;
        }

        private void btnEnterHours_Click(object sender, EventArgs e)
        {
			var thisMethod = MethodBase.GetCurrentMethod();
			Console.Write("****" + thisMethod.Name + "\n");

			// Rebuild the timecard form if a new task was created
            if (_DefineTasksForm.IsNewTaskCreated) {
				_DefineTasksForm.IsNewTaskCreated = false;
				_timecardForm = new TimecardForm(_employee);
            }

            // Make the current active form invisible, then show our timecard form
            if (_currentActiveForm != null) _currentActiveForm.Visible = false;
            _currentActiveForm = _timecardForm;
			AssertActiveForm();
		}

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
			if (_currentActiveForm != null) {
				_currentActiveForm.Visible = false;
			}
			_currentActiveForm = _selectReportForm;
			AssertActiveForm();
		}

		private void btnDefineTaskCategories_Click(object sender, EventArgs e)
		{
			if (_currentActiveForm != null)
			{
				_currentActiveForm.Visible = false;
			}
			_currentActiveForm = _taskcategoriesform;
			AssertActiveForm();
		}

		private void btnDefineTasks_Click(object sender, EventArgs e)
		{
			if (_currentActiveForm != null)
			{
				_currentActiveForm.Visible = false;
			}
			_currentActiveForm = _DefineTasksForm;
			AssertActiveForm();
		}

		private void AssertActiveForm() {
			// And now position the active form relative to ourself and make it visible
			Point targetPoint = this.Location;
			targetPoint.X = this.Location.X + 162;
			targetPoint.Y = this.Location.Y + 40;
			_currentActiveForm.Location = targetPoint;
			_currentActiveForm.Visible = true;
		}
	}
}
