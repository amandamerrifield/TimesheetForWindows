using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using SsOpsDatabaseLibrary;
using SsOpsDatabaseLibrary.Entity;
using Task = SsOpsDatabaseLibrary.Entity.Task;

namespace TimesheetForWindows
{
	public partial class TimecardForm : Form
	{
		// This timecard form requires that the timecard under glass exist in the underlying database.
		// So if a user creates a new timecard for the week, then a new timecard record shall be
		// appended/comitted to the database. However, timecard detail rows will not be added to the DB
		// unless the user saves her changes.  When saving changes, it will be up to the DataWriter to
		// detect that new rows are being added along with existing rows having been changed.

		// Enums and variables having form-wide scope
		private enum FormState
		{
			ViewingData,
			ViewingPotentialChanges,
			SavingChanges
		}
		private FormState _currentFormState;
		private Employee _employee;
		private List<Timecard> _timecards;
		private Timecard _thisTimecard;
		private List<TimecardDetail> _thisTcDetail;
		private string _thisWeekNumber = "1";
		private List<SsOpsDatabaseLibrary.Entity.Task> _activeTasks;
		private List<SsOpsDatabaseLibrary.Entity.Task> _filteredTasks;
		private List<SsOpsDatabaseLibrary.Entity.Task> _displayTasks;

		// =======================================================
		// FORM CONSTRUCTOR
		public TimecardForm(Employee emp)
		{
			InitializeComponent();
			// We will manually control the form location on screen
			this.StartPosition = FormStartPosition.Manual;

			// We want to preview the keystrokes being made into any text box
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(TimecardForm_KeyDown);
			this.KeyPreview = true;

			// Get a copy of the employee key
			_employee = emp;
			_activeTasks = new List<SsOpsDatabaseLibrary.Entity.Task>();
			_thisTcDetail = new List<TimecardDetail>();
			_filteredTasks = new List<SsOpsDatabaseLibrary.Entity.Task>();
			_displayTasks = new List<SsOpsDatabaseLibrary.Entity.Task>();
		}

		// ====================================================
		#region FORM EVENT HANDLERS
		//
		// Key_Down Event Handler
		private void TimecardForm_KeyDown(object sender, KeyEventArgs e)
		{
			//_currentFormState = FormState.ViewingPotentialChanges;
			//assertFormState();
		}
		//
		// Form Load Event Handler
		private void TimecardForm_Load(object sender, EventArgs e)
		{
			//Cache all active tasks from the database
			GetActiveTasks();
			foreach (var task in _activeTasks)
			{
				_filteredTasks.Add(task);
			}

			// Get the employee's data onto the form
			this.Text = "TimeCard -- " + _employee.FirstName + " " + _employee.LastName;

			// Get all timecards for this employee
			GetEmployeeTimecards();

			// Get recent weeks go into the Week combobox
			InitializeComboBox();

			// If we have weeks in our combo box then select the most recent monday and determine what week number that is
			if (comboBoxWeek.Items.Count > 0)
			{
				comboBoxWeek.SelectedIndex = comboBoxWeek.Items.Count - 1;
				_thisWeekNumber = comboBoxWeek.SelectedItem.ToString().Substring(19);
			}

			// if we have a timecard for the given week...
			if (_timecards.Count > 0)
			{
				foreach (Timecard tc in _timecards)
				{
					if (tc.WeekNumber == _thisWeekNumber)
					{
						_thisTimecard = tc;
						// Fetch timecard detail from the DB
						GetTimecardDetail();
						// Update datagridview control with fetched details
						dgvTimecardDetail.DataSource = _thisTcDetail;
					}
				}
			}
		}

		// Add Task Button Click
		private void buttonAddTask_Click(object sender, EventArgs e)
		{
			// Putup a modal dialog where the user can pick a task from an existing list of tasks in our database
			// Don't show tasks that are already on the time card
			SsOpsDatabaseLibrary.Entity.Task theSelectedTask = new SsOpsDatabaseLibrary.Entity.Task();

			//Look at each active task and check that it is not found in the timecard detail list. If found, then it will not be available. 
			//Else, the active task must be avail.

			if (_thisTcDetail.Count != 0)
			{
				foreach (var task in _thisTcDetail)
				{
					//remove task from _filteredList
					foreach (var filteredtask in _filteredTasks)
					{
						if (filteredtask.TaskName == task.TaskName)
						{
							_filteredTasks.Remove(filteredtask);
						}
					}
				}
			}

			using (SelectTaskForm stf = new SelectTaskForm(_filteredTasks))
			{
				Point targetPoint = this.Location;
				targetPoint.X = this.Location.X + 170;
				targetPoint.Y = this.Location.Y + 25;

				stf.Width = 272;
				stf.Height = 458;
				stf.Location = targetPoint;

				stf.ShowDialog(this);
				theSelectedTask = stf.GetSelectedTask();
			}
			if (theSelectedTask != null)
			{
				//Add the selected task to the timecard
				_displayTasks.Add(theSelectedTask);
				dgvTimecardDetail.Rows.Add(theSelectedTask);


				
			}
		}

		#endregion

		// ====================================================
		#region DATA STUBS

		// !!##!!## STUB ##!!##!!STUB ##!!##!!STUB ##!!##!!STUB ##!!##!!
		public List<Timecard> GetTimecardsForEmployeeSTUB(string employeeId)
		{
			List<Timecard> tcards = new List<Timecard>();
			for (int x = 5; x > 0; --x)
			{
				Timecard tc = new Timecard();
				tc.DetailTable = null;
				tc.EmployeeId = employeeId;
				tc.TimecardId = Convert.ToString(2000 + x);
				tc.WeekNumber = Convert.ToString(32 + x);
				tc.Year = "2018";

				tcards.Add(tc);
			}
			return tcards;
		}
		// !!##!!## STUB ##!!##!!STUB ##!!##!!STUB ##!!##!!STUB ##!!##!!
		public List<TimecardDetail> GetTcDetailsByTimecardIdSTUB(string tcardId)
		{
			List<TimecardDetail> details = new List<TimecardDetail>();
			for (int x = 0; x < 5; x++)
			{
				TimecardDetail dtl = new TimecardDetail();
				dtl.Detail_ID = Convert.ToString(1010 + x);
				dtl.Task_ID = Convert.ToString(10010 + x);
				dtl.Timecard_ID = tcardId;
				dtl.Monday_Hrs = "9.0";
				dtl.Tuesday_Hrs = "8.0";
				dtl.Wednesday_Hrs = "7.0";
				dtl.Thursday_Hrs = "6.0";
				dtl.Friday_Hrs = "4.0";
				dtl.Saturday_Hrs = "2.0";
				dtl.Sunday_Hrs = "1.0";

				details.Add(dtl);
			}
			return details;
		}

		#endregion

		// ====================================================
		#region FORM HELPER FUNCTIONS

		private void InitializeComboBox()
		{
			comboBoxWeek.Items.Clear();

			string firstDoY = "01/01/" + DateTime.Today.Year.ToString();
			DateTime firstMondayOfYear = DateTime.Parse(firstDoY);
			while (firstMondayOfYear.DayOfWeek != DayOfWeek.Monday)
			{
				firstMondayOfYear = firstMondayOfYear.AddDays(1);
			}
			List<DateTime> mondays = new List<DateTime>();
			for (int weeek = 1; weeek < 52; weeek++)
			{
				DateTime another_monday = firstMondayOfYear.AddDays(7 * weeek);
				if (another_monday.DayOfYear > DateTime.Today.DayOfYear - 30)
				{
					if (another_monday.DayOfYear <= DateTime.Today.DayOfYear)
					{
						comboBoxWeek.Items.Add(another_monday.ToString("yyyy-MM-dd") + " -- Week " + weeek.ToString());
					}
				}
			}
		}

		//private void InitializeDGV()
		//{
		//	try
		//	{
		//		dgvTimecardDetail.Dock = DockStyle.Fill;
		//		dgvTimecardDetail.AutoGenerateColumns = true;
		//	}
		//	catch (Exception ex)
		//	{
		//		// ToDo: ex
		//		System.Threading.Thread.CurrentThread.Abort();
		//	}
		//}

		private void assertFormState()
		{
			switch (_currentFormState)
			{
				case FormState.SavingChanges:
					buttonUpdate.Enabled = false;
					buttonCancel.Enabled = false;
					buttonQuit.Enabled = false;
					break;
				case FormState.ViewingData:
					buttonUpdate.Enabled = false;
					buttonCancel.Enabled = false;
					buttonQuit.Enabled = true;
					break;
				case FormState.ViewingPotentialChanges:
					buttonUpdate.Enabled = true;
					buttonCancel.Enabled = true;
					buttonQuit.Enabled = false;
					break;
			}
		}

		private void AddNewWeekToTimecard()
		{
			//Assert the wait cursor..
			Application.UseWaitCursor = true;

			try
			{
				//Create a new timecard in the DB
				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					Timecard tc = new Timecard();
					tc.EmployeeId = _employee.EmployeeId;
					tc.WeekNumber = Convert.ToString(_thisWeekNumber);
					tc.Year = Convert.ToString(DateTime.Today.Year);
					int newTimecardId = dbLib.CreateTimeCard(tc);
				}
			}
			catch (Exception ex)
			{
				Application.UseWaitCursor = false;
				string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
				MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
				Application.Exit();
			}
			finally
			{
				//Deny the wait cursor
				Application.UseWaitCursor = false;
			}
		}

		private void GetEmployeeTimecards()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					_timecards = dbLib.GetTimecardsForEmployee(_employee.EmployeeId);
				}
			}
			catch (Exception ex)
			{
				Application.UseWaitCursor = false;
				string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
				MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
				Application.Exit();
			}
			finally
			{
				//Deny the wait cursor
				Application.UseWaitCursor = false;
			}
		}

		private void GetTimecardDetail()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					_thisTcDetail = dbLib.GetTimecardDetailsByTimecardId(_thisTimecard.TimecardId);
				}
			}
			catch (Exception ex)
			{
				Application.UseWaitCursor = false;
				string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
				MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
				Application.Exit();
			}
			finally
			{
				//Deny the wait cursor
				Application.UseWaitCursor = false;
			}
		}

		private void AddTimecardForEmployee()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					_thisTimecard = new Timecard();
					_thisTimecard.EmployeeId = _employee.EmployeeId;
					_thisTimecard.WeekNumber = Convert.ToString(_thisWeekNumber);
					_thisTimecard.Year = Convert.ToString(DateTime.Today.Year);
					int newTimecardID = dbLib.CreateTimeCard(_thisTimecard);
					_thisTimecard.TimecardId = Convert.ToString(newTimecardID);
				}
			}
			catch (Exception ex)
			{
				Application.UseWaitCursor = false;
				string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
				MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
				Application.Exit();
			}
			finally
			{
				//Deny the wait cursor
				Application.UseWaitCursor = false;
			}
		}

		private void GetActiveTasks()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					//Active tasks are the ones in the database that haven't ended yet
					var activeTasks = new List<SsOpsDatabaseLibrary.Entity.Task>();
					_activeTasks = dbLib.GetActiveTasks();

				}
			}
			catch (Exception ex)
			{
				Application.UseWaitCursor = false;
				string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
				MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
				Application.Exit();
			}
			finally
			{
				//Deny the wait cursor
				Application.UseWaitCursor = false;
			}
		}
	}
}
        #endregion
