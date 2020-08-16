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
			Loading,
			ViewingData,
			ViewingPotentialChanges,
			SavingChanges
		}
		private FormState _currentFormState;
		private Employee _employee;
		private List<Timecard> _timecards;
		private Timecard _timecardUnderGlass;
		private List<TimecardDetail> _timecardDetailsUnderGlass;
		private string _thisWeekNumber = "1";
		private List<SsOpsDatabaseLibrary.Entity.Task> _activeTasks;
		private List<SsOpsDatabaseLibrary.Entity.Task> _filteredTasks;
		private BindingSource _bindingSource1;

		// =======================================================
		// FORM CONSTRUCTOR
		public TimecardForm(Employee targetEmployee)
		{
			InitializeComponent();
			// We will manually control the form location on screen
			this.StartPosition = FormStartPosition.Manual;

			// We want to preview the keystrokes being made into any text box
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(TimecardForm_KeyDown);
			this.KeyPreview = true;

			// Get a copy of the employee key
			_employee = targetEmployee;

			//Our list of timecard detail lines
			_timecardDetailsUnderGlass = new List<TimecardDetail>();
			_bindingSource1 = new BindingSource();
			
			_activeTasks = new List<Task>();
			_filteredTasks = new List<Task>();
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
			_currentFormState = FormState.Loading;

			//Cache all active tasks from the database
			GetActiveTasks();

			//Get a deep copy of active tasks in our filtered tasks
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
						_timecardUnderGlass = tc;
						// Fetch timecard detail from the DB into _thisTcDetail
						GetTimecardDetails();
					}
				}
			}

			dgvTimecardDetail.DataSource = _bindingSource1;
			_bindingSource1.DataSource = _timecardDetailsUnderGlass;

			_currentFormState = FormState.ViewingData;
		}

		// Add Task Button Click
		private void buttonAddTask_Click(object sender, EventArgs e)
		{
			// Putup a modal dialog where the user can pick a task
			// Don't show tasks that are already on the time card
			Task theSelectedTask = new Task();

			// Filtered tasks are the ones that are not already on the timecard
			if (_timecardDetailsUnderGlass.Count != 0)
			{
				foreach (TimecardDetail tcd in _timecardDetailsUnderGlass)
				{
					foreach (Task task in _activeTasks)
					{
						if (task.TaskName == tcd.TaskName)
						{
							_filteredTasks.Remove(task);
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
				stf.Dispose();
			}

			if (theSelectedTask != null)
			{
				//Add the selected task to the timecard
				TimecardDetail tcDetail = new TimecardDetail();
				tcDetail.TaskName = theSelectedTask.TaskName;
				//adding a row to the binding source will in-turn add the row to our _timecardDetailsUnderGlass list
				_bindingSource1.Add(tcDetail);
				//There are now changes made to this timecard that have not yet been committed to the DB
				_currentFormState = FormState.ViewingPotentialChanges;
			}
		}
		private void comboBoxWeek_SelectedIndexChanged(object sender, EventArgs e) {
			if(_currentFormState != FormState.Loading) {
				//Start with an empty detail list (clearing the binding source causes _timecardDetailsUnderGlass to be cleared)
				_bindingSource1.Clear();
				// Get the target week number
				_thisWeekNumber = comboBoxWeek.SelectedItem.ToString().Substring(19);
				// Find the target timecard using its week number
				_timecardUnderGlass = null;
				// Flag
				foreach(Timecard tc in _timecards) {
					if(tc.WeekNumber == _thisWeekNumber) {
						// We found the timecard!
						_timecardUnderGlass = tc;
						// Fetch timecard details from the DB into a new _timecardDetailsUnderGlass list
						GetTimecardDetails();
						// Cannot use the old bindingSource now b/c its holding a reference to a discarded list
						_bindingSource1 = new BindingSource();
						// BindingSource now bound to new instance of _timecardDetailsUnderGlass list
						_bindingSource1.DataSource = _timecardDetailsUnderGlass;
						// The dgv Datasource must now point to the new binding source instead of the discarded one
						dgvTimecardDetail.DataSource = _bindingSource1;
						_bindingSource1.ResetBindings(false);
						break;
					}
				}
				// Note that if there is no timecard in the database for this week..
				// then we exit here with a cleared binding source/dgv, and _timecardUnderGlass = null;
				// and _thisWeekNumber set to the number of the week that is missing a timecard

				// We can still select and add timecard details on screen, but keep in mind that the 
				// Timecard record must be created and inserted into the database before we attempt
				// to insert detail rows that are joined to it. [KFF]
			}
		}
		private void buttonUpdate_Click(object sender, EventArgs e) {
			//If there are no pending changes then skip all this
			if(_currentFormState != FormState.ViewingPotentialChanges) return;

			//If we do not have a timecard for this week then create and insert
			//In either case, we will update the detail rows for this timecard
			if(_timecardDetailsUnderGlass == null) {
				//ToDo: Insert new timecard

			}
			// We don't get to this point w/o having a timecard under glass


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

		private void GetTimecardDetails()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					_timecardDetailsUnderGlass = dbLib.GetTimecardDetailsByTimecardId(_timecardUnderGlass.TimecardId);
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
					_timecardUnderGlass = new Timecard();
					_timecardUnderGlass.EmployeeId = _employee.EmployeeId;
					_timecardUnderGlass.WeekNumber = Convert.ToString(_thisWeekNumber);
					_timecardUnderGlass.Year = Convert.ToString(DateTime.Today.Year);
					int newTimecardID = dbLib.CreateTimeCard(_timecardUnderGlass);
					_timecardUnderGlass.TimecardId = Convert.ToString(newTimecardID);
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
