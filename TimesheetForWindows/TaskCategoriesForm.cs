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
using DecoratorsLibrary;
using static DecoratorsLibrary.ControlTextValidator;

namespace TimesheetForWindows
{
	public partial class TaskCategoriesForm : Form
	{
		private List<TaskCategory> _taskcategories;

		public TaskCategoriesForm()
		{
			InitializeComponent();
			// We will manually control the form location on screen
			this.StartPosition = FormStartPosition.Manual;
			_taskcategories = new List<TaskCategory>();
		}


		// ====================================================
		#region FORM HELPER FUNCTIONS

		private bool ValidateForm()
		{
			List<string> problemMessages = new List<string>();

			//Perform validation on every control that has a validator in its tag
			foreach (Control ctrl in Controls)
			{
				//if(whatsThere.GetType() == typeof(ControlTextValidator)) {} This is an altenate test of type
				if (ctrl.Tag != null)
				{
					if (ctrl.Tag is ControlTextValidator)
					{
						var validatr = (ControlTextValidator)ctrl.Tag;
						string validationMsg = validatr.ValidationMsg();
						if (validationMsg.Length > 0)
						{
							// failed validation...
							problemMessages.Add(validationMsg);
						}
					}
				}
			}

			if(problemMessages.Count > 0)
			{
				foreach(string problem in problemMessages)
				{
					MessageBox.Show(problem, "Attention", MessageBoxButtons.OK);
				}
				return false;
			}
			return true;
		}

		private void GetTaskCategories()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					_taskcategories = dbLib.GetTaskCategories();
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

		private void AppendTaskCategory(TaskCategory tcat)
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					int x = dbLib.CreateTaskCategory(tcat);
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

		#endregion

		// =================================================
		#region FORM EVENT HANDLERS

		private void TaskCategoriesForm_Load(object sender, EventArgs e)
		{
			//Get task categories
			GetTaskCategories();
			this.dataGridView1.DataSource = _taskcategories;

			//Get some validators going here
			bool required = true;
			textBox1.Tag = new ControlTextValidator(textBox1,"Add New Category Name", required, ValidationStyle.NoValidation);
			textBox2.Tag = new ControlTextValidator(textBox2, "Add New Category Description", required, ValidationStyle.NoValidation);

		}

		private void btnClearChanges_Click(object sender, EventArgs e)
		{
			textBox1.Clear();
			textBox2.Clear();
			checkBox1.Checked = false;
		}

		private void btnCancelChanges_Click(object sender, EventArgs e)
		{
			btnClearChanges_Click(sender, e);
			Visible = false;
		}

		private void btnSaveChanges_Click(object sender, EventArgs e)
		{
			//Remove any whitespace from the text in the control
			textBox1.Text = textBox1.Text.Trim();
			textBox2.Text = textBox2.Text.Trim();

			//If we fail validation, skip all the other stuff and return
			if (! ValidateForm()) return;

			foreach (TaskCategory cat in _taskcategories)
			{
				if (cat.CategoryName.ToUpper() == textBox1.Text.ToUpper())
				{
					MessageBox.Show("This category already exists in the grid.","Attention", MessageBoxButtons.OK);
					textBox1.Focus();
					textBox1.SelectionStart = 0;
					textBox1.SelectionLength = textBox1.Text.Length;
					return;
				}
			}
			//Uppercase the first character of each to ensure title case
			textBox1.Text = textBox1.Text.Substring(0, 1).ToUpper() + textBox1.Text.Substring(1);
			textBox2.Text = textBox2.Text.Substring(0, 1).ToUpper() + textBox2.Text.Substring(1);

			TaskCategory tc = new TaskCategory();
			tc.CategoryDescription = textBox2.Text;
			tc.CategoryName = textBox1.Text;
			tc.IsOverheadYN = (checkBox1.Checked) ? "Y" : "N";
			AppendTaskCategory(tc);
			textBox1.Clear();
			textBox2.Clear();

			GetTaskCategories();
			dataGridView1.DataSource = _taskcategories;
			dataGridView1.Refresh();
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
		#endregion
	}
}
