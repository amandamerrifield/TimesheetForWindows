using TextValidationLibrary;
using SsOpsDatabaseLibrary;
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
using static TextValidationLibrary.TextBoxValidator;

namespace TimesheetForWindows
{
	public partial class DefineTasksForm : Form
	{
		private List<SsOpsDatabaseLibrary.Entity.Task> _tasks;
		private List<SsOpsDatabaseLibrary.Entity.TaskCategory> _categories;
		private bool _isNewTask;

		// ==============================================
		#region PUBLIC PROPERTIES WITH INTERNAL BACKING VARIABLES

		public bool IsNewTaskCreated {
			get { return _isNewTask; }
			set { _isNewTask = value; }
		}
		#endregion

		// ==============================================
		// CONSTRUCTOR
		public DefineTasksForm()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.Manual;
			_tasks = new List<SsOpsDatabaseLibrary.Entity.Task>();
			_isNewTask = false;
		}

		// ==============================================
		#region FORM HELPER FUNCTIONS

		private void GetAllTasks()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					_tasks = dbLib.GetAllTasks();
				}
				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					_categories = dbLib.GetTaskCategories();
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
		private bool ValidateForm()
		{
			List<string> problemMessages = new List<string>();

			//Validate the combobox
			if (cbxCategories.SelectedIndex == -1)
			{
				problemMessages.Add("Please select a category using the 'to Category' dropdown list");
			}

			//Perform validation on every control that has a validator in its tag
			foreach (Control ctrl in Controls)
			{
				if (ctrl.Tag != null)
				{
					if (ctrl.Tag is TextBoxValidator)
					{
						var validatr = (TextBoxValidator)ctrl.Tag;
						string validationMsg = validatr.ValidationMsg();
						if (validationMsg.Length > 0)
						{
							// failed validation...
							problemMessages.Add(validationMsg);
						}
					}
				}
			}

			if (problemMessages.Count > 0)
			{
				foreach (string problem in problemMessages)
				{
					MessageBox.Show(problem, "Attention", MessageBoxButtons.OK);
				}
				return false;
			}
			return true;
		}

		private void AppendTask(SsOpsDatabaseLibrary.Entity.Task newtask) {
			try {
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter()) {
					// Call OpsDataReader to get the details for the selected week
					int x = dbLib.CreateTask(newtask);
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

		private string DateFormat_UK(string dateFormat_US) {
			// Convert mm/dd/yyyy to yyyy-mm-dd
			StringBuilder sbldr = new StringBuilder(dateFormat_US.Substring(6));
			sbldr.Append("-" + dateFormat_US.Substring(0, 2));
			sbldr.Append("-" + dateFormat_US.Substring(3, 2));
			return sbldr.ToString();
		}

		#endregion


		// ==============================================
		#region FORM EVENT HANDLERS

		private void DefineTasksForm_Load(object sender, EventArgs e)
		{
			GetAllTasks();
			this.dataGridView1.DataSource = _tasks;

			foreach (var taskCat in _categories)
			{
				cbxCategories.Items.Add(taskCat);
			}

			//Get some validators going here
			bool required = true;
			bool notRequired = false;
			textBox1.Tag = new TextBoxValidator(textBox1, "Add New Task", required, ValidationStyle.NoPunctuation);
			textBox2.Tag = new TextBoxValidator(textBox2, "Budget Hours", required, ValidationStyle.DigitsNotZero);
			textBox4.Tag = new TextBoxValidator(textBox4, "Start Date", required, ValidationStyle.DateAny);
			textBox5.Tag = new TextBoxValidator(textBox5, "End Date", notRequired, ValidationStyle.DateFuture);

		}

		private void btnClearChanges_Click(object sender, EventArgs e)
		{
			textBox1.Clear();
			textBox2.Clear();
			textBox3.Clear();
			textBox4.Clear();
			textBox5.Clear();
			cbxCategories.SelectedIndex = -1;
		}

		private void btnCancelChanges_Click(object sender, EventArgs e)
		{
			btnClearChanges_Click(sender, e);
			this.Visible = false;
		}

		private void btnSaveChanges_Click(object sender, EventArgs e)
		{
			//Remove any whitespace from the text in the control
			textBox1.Text = textBox1.Text.Trim();
			textBox2.Text = textBox2.Text.Trim();
			textBox4.Text = textBox4.Text.Trim();
			textBox5.Text = textBox5.Text.Trim();

			//If we fail validation, abort the save procedure and return
			if (!ValidateForm()) return;

			textBox1.Text = textBox1.Text.Substring(0, 1).ToUpper() + textBox1.Text.Substring(1);

			// 
			//MessageBox.Show("TEST PASSED!!", ProductName, MessageBoxButtons.OK);
			foreach (SsOpsDatabaseLibrary.Entity.Task task in _tasks) {
				if (task.TaskName.ToUpper() == textBox1.Text.ToUpper()) {
					MessageBox.Show("This task already exists in the grid.", "Attention", MessageBoxButtons.OK);
					textBox1.Focus();
					textBox1.SelectionStart = 0;
					textBox1.SelectionLength = textBox1.Text.Length;
					return;
				}
			}
			//Uppercase the first character of each to ensure title case
			textBox1.Text = textBox1.Text.Substring(0, 1).ToUpper() + textBox1.Text.Substring(1);

			//We have to connect each of the columns in the table to recognize the text from the textbox should be in there now
			SsOpsDatabaseLibrary.Entity.Task tsk = new SsOpsDatabaseLibrary.Entity.Task();
			tsk.BudgetHours = textBox2.Text;
			tsk.TaskName = textBox1.Text;
			tsk.StartDate = DateFormat_UK(textBox4.Text);
			tsk.EndDate = (String.IsNullOrEmpty(textBox5.Text) ? null : DateFormat_UK(textBox5.Text));
			tsk.ActualHours = "0";
  
			TaskCategory tc = (TaskCategory)cbxCategories.SelectedItem;
			tsk.CategoryId = tc.CategoryId;
			AppendTask(tsk);
			_isNewTask = true;

			//Once task is appended, clear the text boxes
			textBox1.Clear();
			textBox2.Clear();
			textBox4.Clear();
			textBox5.Clear();

			//Get the list of all tasks
			GetAllTasks();
			dataGridView1.DataSource = _tasks;
			//refresh the data so you can see the new task
			dataGridView1.Refresh();

		}

		#endregion

	}
}
