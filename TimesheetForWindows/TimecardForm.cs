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
		// Thomas Fetterhoff 01/25/2021

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
		private List<TimecardDetail> _tcDetailsUnderGlass;
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
			_tcDetailsUnderGlass = new List<TimecardDetail>();
			_bindingSource1 = new BindingSource();
			
			_activeTasks = new List<Task>();
			_filteredTasks = new List<Task>();
		}

		// =====================================================
		#region FORM EVENT HANDLERS
		// -----------------------------------------------------
		// Form -- Keyboard Key_Down Event Handler
		private void TimecardForm_KeyDown(object sender, KeyEventArgs e)
		{
			//_currentFormState = FormState.ViewingPotentialChanges;
			//assertFormState();
		}
		// -----------------------------------------------------
		// Form -- Load Event Handler
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
						break;
					}
				}
			}

			dgvTimecardDetail.DataSource = _bindingSource1;
			_bindingSource1.DataSource = _tcDetailsUnderGlass;
			//Do not display these columns in the grid
			dgvTimecardDetail.Columns["IsBlank"].Visible = false;
			dgvTimecardDetail.Columns["TaskId"].Visible = false;
			dgvTimecardDetail.Columns["TimecardId"].Visible = false;
			dgvTimecardDetail.Columns["DetailId"].Visible = false;

			_currentFormState = FormState.ViewingData;
			AssertFormState();

			// Get the employee's data onto the form
			UpdateWeekHoursTotalOnTitleBar();
		}
		// -----------------------------------------------------
		// Add Task Button -- Click Event Handler
		private void buttonAddTask_Click(object sender, EventArgs e)
		{
			// Putup a modal dialog where the user can pick a task
			// Don't show tasks that are already on the time card
			
			SsOpsDatabaseLibrary.Entity.Task[] theSelectedTasks;

			// Filtered tasks are the ones that are not already on the timecard
			if (_tcDetailsUnderGlass.Count != 0)
			{
				foreach (TimecardDetail tcd in _tcDetailsUnderGlass)
				{
					foreach (Task task in _activeTasks)
					{
						if (task.TaskName == tcd.Task_Name)
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
				theSelectedTasks = stf.GetSelectedTasks();
				stf.Dispose();
			}

			if (theSelectedTasks != null)
			{
				//Add the selected task to the timecard
				TimecardDetail tcDetail;
				foreach(SsOpsDatabaseLibrary.Entity.Task tsk in theSelectedTasks) {
					tcDetail = new TimecardDetail();
					tcDetail.Task_Name = tsk.TaskName;
					tcDetail.TaskId = Convert.ToInt32(tsk.TaskId);
					_bindingSource1.Add(tcDetail);
				}
				//tcDetail.Task_Name= theSelectedTask.TaskName;
				//tcDetail.TaskId = Convert.ToInt32(theSelectedTask.TaskId);

				//adding a row to the binding source will in-turn add the row to our _timecardDetailsUnderGlass list
				//_bindingSource1.Add(tcDetail);
				//There are now changes made to this timecard that have not yet been committed to the DB
				_currentFormState = FormState.ViewingPotentialChanges;
				AssertFormState();
			}
		}
		// -----------------------------------------------------
		// Week Under Glass ComboBox -- Selection Changed Event Handler
		private void comboBoxWeek_SelectedIndexChanged(object sender, EventArgs e) {
			if(_currentFormState != FormState.Loading) {
				//Start with an empty detail list (clearing the binding source causes _tcDetailsUnderGlass to be cleared)
				_bindingSource1.Clear();

				// Get the target week number
				_thisWeekNumber = comboBoxWeek.SelectedItem.ToString().Substring(19);
				// Find the target timecard using its week number
				_timecardUnderGlass = null;
				foreach(Timecard tc in _timecards) {
					if(tc.WeekNumber == _thisWeekNumber) {
						// We found the timecard!
						_timecardUnderGlass = tc;
						// Fetch timecard details from the DB into a new _tcDetailsUnderGlass list
						GetTimecardDetails();
						// BindingSource now gets bound to a new instance of _tcDetailsUnderGlass list
						_bindingSource1.DataSource = _tcDetailsUnderGlass;
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

				//Update the week's hours
				UpdateWeekHoursTotalOnTitleBar();

				//We just selected a new timecard, so we are Viewing Data
				_currentFormState = FormState.ViewingData;
				AssertFormState();
			}
		}
		// -----------------------------------------------------
		// Save Changes Button -- Click Event Handler
		private void buttonUpdate_Click(object sender, EventArgs e) {
			//If there are no pending changes then skip all this
			if (_currentFormState != FormState.ViewingPotentialChanges) return;

			//If we do not have a timecard for this week then create and insert in the DB
			bool isNewlyCreatedTimecard = false;
			if(_timecardUnderGlass == null) {
				CreateNewTimecard();
				isNewlyCreatedTimecard = true;
			}
			// We don't get to this point w/o having _timecardUnderGlass in the database

			// First discard all timecard detail entries that have zero or blank for every day this week
			List<TimecardDetail> toBeRemoved = new List<TimecardDetail>();
			foreach (TimecardDetail tcd in _tcDetailsUnderGlass) {
				if(tcd.IsBlank) {
					toBeRemoved.Add(tcd);
				}
			}
			foreach(TimecardDetail xx in toBeRemoved) {
				//_tcDetailsUnderGlass.Remove(xx);
				_bindingSource1.Remove(xx);
			}

			// Now make sure that all the timecard details in the dgv are also in the new _timecardUnderGlass instance
			_timecardUnderGlass.DetailList = new List<TimecardDetail>();
			foreach (TimecardDetail tcd in _tcDetailsUnderGlass) {
				_timecardUnderGlass.DetailList.Add(tcd);
			}
			UpdateWeekHoursTotalOnTitleBar();

			// Update the timecard detail rows in the database that are joined to this timecard
			// Any timecard details that are IN the DB but NOT in _timecardUnderGlass.DetailList will be deleted
			// Any timecard details that are IN the DB AND IN _timecardUnderGlass.DetailList will be updated
			// Finally, any time card detail that is missing in the DB will be inserted into the DB [KFF]
			UpdateTimecardDetails(isNewlyCreatedTimecard);

			// Changes were saved, so now we are viewing data
			_currentFormState = FormState.ViewingData;
			AssertFormState();
		}

		#endregion

		// ====================================================
		#region FORM HELPER FUNCTIONS

		// ----------------------------------------------------
		private void UpdateWeekHoursTotalOnTitleBar() {

			//Start the accumulate hours counter for this timecard
			decimal accumulator = 0;

			foreach (TimecardDetail tcd in _tcDetailsUnderGlass) {
				accumulator += string.IsNullOrWhiteSpace(tcd.Monday_Hrs) ? 0 : Convert.ToDecimal(tcd.Monday_Hrs);
				accumulator += string.IsNullOrWhiteSpace(tcd.Tuesday_Hrs) ? 0 : Convert.ToDecimal(tcd.Tuesday_Hrs);
				accumulator += string.IsNullOrWhiteSpace(tcd.Wednesday_Hrs) ? 0 : Convert.ToDecimal(tcd.Wednesday_Hrs);
				accumulator += string.IsNullOrWhiteSpace(tcd.Thursday_Hrs) ? 0 : Convert.ToDecimal(tcd.Thursday_Hrs);
				accumulator += string.IsNullOrWhiteSpace(tcd.Friday_Hrs) ? 0 : Convert.ToDecimal(tcd.Friday_Hrs);
				accumulator += string.IsNullOrWhiteSpace(tcd.Saturday_Hrs) ? 0 : Convert.ToDecimal(tcd.Saturday_Hrs);
				accumulator += string.IsNullOrWhiteSpace(tcd.Sunday_Hrs) ? 0 : Convert.ToDecimal(tcd.Sunday_Hrs);
			}
			this.Text = "TimeCard -- " + _employee.FirstName + " " + _employee.LastName + " -- Total Hours this week: " + accumulator.ToString();
		}

		// ----------------------------------------------------
		// Initialize the Week Selection Drop Down
		private void InitializeComboBox()
		{
			comboBoxWeek.BeginUpdate();
			comboBoxWeek.Items.Clear();

			int weeek = 1;
			string firstDoY = "01/01/" + DateTime.Today.Year.ToString();
			DateTime firstMondayOfYear = DateTime.Parse(firstDoY);
			while (firstMondayOfYear.DayOfWeek != DayOfWeek.Monday)
			{
				firstMondayOfYear = firstMondayOfYear.AddDays(1);
			}
			List<String> mondays = new List<String>();
			DateTime another_monday = firstMondayOfYear;
			while (another_monday.DayOfYear <= DateTime.Today.DayOfYear) {
				mondays.Add(another_monday.ToString("yyyy/MM/dd") + " -- Week " + weeek.ToString());
				weeek += 1;
				another_monday = another_monday.AddDays(7);
			}

			if(mondays.Count > 4) {
				string[] last4mondays = new string[4];
				// Array version of Substring -- SubArray() Lol!
				Array.Copy(mondays.ToArray(), mondays.Count -4, last4mondays, 0, 4);
				comboBoxWeek.Items.AddRange(last4mondays);
			}
			else {
				comboBoxWeek.Items.AddRange(mondays.ToArray());
			}
			comboBoxWeek.EndUpdate();
		}

		// ------------------------------------------------
		// Enforce the current State of the Form against the buttons
		private void AssertFormState()
		{
			switch (_currentFormState)
			{
				case FormState.SavingChanges:
					buttonUpdate.Enabled = false;
					buttonCancel.Enabled = false;
					break;
				case FormState.ViewingData:
					buttonUpdate.Enabled = false;
					buttonCancel.Enabled = false;
					break;
				case FormState.ViewingPotentialChanges:
					buttonUpdate.Enabled = true;
					buttonCancel.Enabled = true;
					break;
			}
		}

		// ------------------------------------------------
		// Create a new timecard in the DB for the given Employee and Week, then
		// copy the new TimecardId that is returned by the DB insert function
		private void CreateNewTimecard()
		{
			//Assert the wait cursor..
			Application.UseWaitCursor = true;

			try
			{
				//Create a new timecard in the DB
				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					//Instantiate a new timecard and save it in the database
					_timecardUnderGlass = new Timecard();
					_timecardUnderGlass.EmployeeId = _employee.EmployeeId;
					_timecardUnderGlass.WeekNumber = _thisWeekNumber;
					_timecardUnderGlass.Year = DateTime.Today.ToString("yyyy");
					int newlyMintedTimecardID = dbLib.CreateTimeCard(_timecardUnderGlass);
					_timecardUnderGlass.TimecardId = newlyMintedTimecardID;
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
		// -----------------------------------------------
		// Get All this Employee's Timecard Records into _Timecards
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
		// -----------------------------------------------
		// Get All this Employee's Timecard Detail Records into _timecardDetailsUnderGlass
		private void GetTimecardDetails()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					_tcDetailsUnderGlass = dbLib.GetTimecardDetailsByTimecardId(_timecardUnderGlass.TimecardId);
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
		// ----------------------------------------------------
		// Get All the task records from the DB that are still in use.
		private void GetActiveTasks()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
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
		// ----------------------------------------------------------------
		// Update the TimecardDetail records for _timecardUnderGlass
		// If the timecard was newly created, it has no existing detail records.
		// Insert any records that are within _timecardUnderGlass.DetailList.
		// Otherwise, update or remove the existing records such that they match
		// the records within _timecardUnderGlass.DetailList
		private void UpdateTimecardDetails(bool isNewTimecard) {
			try {
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter()) {
					//If we get here with an empty timecard then delete all detail records for this timecard and exit
					if (_timecardUnderGlass.DetailList.Count == 0 && isNewTimecard ) return;

					//Get any existing rows from the DB
					List<TimecardDetail> existingDetails = dbLib.GetTimecardDetailsByTimecardId(_timecardUnderGlass.TimecardId);

					//If there is nothing in the database to update or delete then this is an insert only operation
					if(existingDetails.Count == 0) {
						if (_timecardUnderGlass.DetailList.Count == 0) return;
						//Insert all items in the _timecardUnderGlass.DetailList into the DB and return
						dbLib.CreateTimeCardDetail(_timecardUnderGlass);
						return;
					}
					// The database has existing records
					//Look for deletes first, then Updates, and lastly inserts
					List<TimecardDetail> pendingDeletes = new List<TimecardDetail>();

					//Now find the records that are to be deleted (records that do not exist in the grid)
					bool isNotFoundUnderGlass;
					foreach (TimecardDetail existingTcd in existingDetails) {
						isNotFoundUnderGlass = true;
						foreach (TimecardDetail tcd in _timecardUnderGlass.DetailList) {
							if (tcd.Task_Name == existingTcd.Task_Name) {
								isNotFoundUnderGlass = false;
								break;
							}
						}
						if (isNotFoundUnderGlass) {
							pendingDeletes.Add(existingTcd);
                        }
                    }
                    // If we have records to delete then do it now
                    if (pendingDeletes.Count > 0) {
                        dbLib.DeleteTimeCardDetail(pendingDeletes);
                    }

                    // The UpdateTimecardDetails function will update all records that are already in the DB
                    dbLib.UpdateTimeCardDetail(_timecardUnderGlass);
                    // CreateTimecardDetail will insert any records NOT yet in the DB
                    dbLib.CreateTimeCardDetail(_timecardUnderGlass);
                }
            }
            catch (Exception ex) {
				Application.UseWaitCursor = false;
				string errHead = GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() failed. \n\n";
				MessageBox.Show(errHead + "Source: " + ex.Source + "\n\n" + ex.Message, ProductName + " " + ProductVersion, MessageBoxButtons.OK);
				Application.Exit();
			}
			finally {
				//Deny the wait cursor
				Application.UseWaitCursor = false;
			}
		}

        private void buttonCancel_Click(object sender, EventArgs e) {
			GetTimecardDetails();
			_bindingSource1.DataSource = _tcDetailsUnderGlass;
			bool isNewDataLayout = false;
			_bindingSource1.ResetBindings(isNewDataLayout);
			// Changes were cancelled, so now we are viewing data
			_currentFormState = FormState.ViewingData;
			AssertFormState();
        }

        private void buttonQuit_Click(object sender, EventArgs e) {
			buttonCancel_Click(sender, e);
			this.Visible = false;
		}

        private void dgvTimecardDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
			if (_currentFormState == FormState.ViewingData) {
				_currentFormState = FormState.ViewingPotentialChanges;
				AssertFormState();
			}
        }
    }
}
        #endregion
