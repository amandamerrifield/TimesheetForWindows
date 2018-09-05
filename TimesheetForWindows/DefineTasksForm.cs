using DecoratorsLibrary;
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
using static DecoratorsLibrary.ControlTextValidator;

namespace TimesheetForWindows
{
	public partial class DefineTasksForm : Form
	{
		private List<SsOpsDatabaseLibrary.Entity.Task> _defineTasks;
		private List<SsOpsDatabaseLibrary.Entity.TaskCategory> _categories;

		public DefineTasksForm()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.Manual;
			_defineTasks = new List<SsOpsDatabaseLibrary.Entity.Task>();
		}
		private void GetAllTasks()
		{
			try
			{
				//Assert wait cursor
				Application.UseWaitCursor = true;

				using (OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
				{
					// Call OpsDataReader to get the details for the selected week
					_defineTasks = dbLib.GetAllTasks();
				}
				using(OpsDatabaseAdapter dbLib = new OpsDatabaseAdapter())
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
			if(cbxCategories.SelectedIndex == -1)
			{
				problemMessages.Add("Please select a category using the 'to Category' dropdown list");
			}

			//Perform validation on every control that has a validator in its tag
			foreach (Control ctrl in Controls)
			{
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


		private void DefineTasksForm_Load(object sender, EventArgs e)
		{
			GetAllTasks();
			this.dataGridView1.DataSource = _defineTasks;

			foreach(var taskCat in _categories)
			{
				cbxCategories.Items.Add(taskCat);
			}

			//Get some validators going here
			bool required = true;
			bool notRequired = false;
			textBox1.Tag = new ControlTextValidator(textBox1, "Add New Task", required, ValidationStyle.NoPunctuation);
			textBox2.Tag = new ControlTextValidator(textBox2, "Budget Hours", required, ValidationStyle.DigitsNotZero);
			textBox4.Tag = new ControlTextValidator(textBox4, "Start Date", required, ValidationStyle.DateAny);
			textBox5.Tag = new ControlTextValidator(textBox5, "End Date", notRequired, ValidationStyle.DateFuture);

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

			MessageBox.Show("TEST PASSED!!", ProductName, MessageBoxButtons.OK);

		}
	}
}
