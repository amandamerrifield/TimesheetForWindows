using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SsOpsDatabaseLibrary;
using SsOpsDatabaseLibrary.Entity;

namespace TimesheetForWindows
{
	public partial class TimecardForm : Form
	{
		// Enums and variables having form-wide scope
		private enum FormState
		{
			ViewingData,
			ViewingPotentialChanges,
			SavingChanges
		}
		private FormState _currentFormState;
		private Employee _employee;

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
		}

		// ====================================================
		#region FORM EVENT HANDLERS
	    //
		// Key_Down Event Handler
		//
		private void TimecardForm_KeyDown(object sender, KeyEventArgs e)
		{
			_currentFormState = FormState.ViewingPotentialChanges;
			assertFormState();
		}
		//
		// Form Load Event Handler
		private void TimecardForm_Load(object sender, EventArgs e)
		{
			// Get the employee's data onto the form
			this.Text = "TimeCard -- " + _employee.FirstName +  " "  + _employee.LastName;
			//To Do call opsdatareader to get timecards for this employee
			//To Do set default to this week 
		}
		#endregion

		// ====================================================
		#region FORM HELPER FUNCTIONS

		private void InitializeDGV()
		{
			try
			{
				dgvTimecardDetail.Dock = DockStyle.Fill;
				dgvTimecardDetail.AutoGenerateColumns = true;

			}
			catch (Exception ex)
			{

				System.Threading.Thread.CurrentThread.Abort();
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

		#endregion


	}
}
