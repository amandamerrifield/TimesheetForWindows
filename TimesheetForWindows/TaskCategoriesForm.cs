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

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void TaskCategoriesForm_Load(object sender, EventArgs e)
		{
			//Get task categories
			GetTaskCategories();
			this.dataGridView1.DataSource = _taskcategories;
		}

		// ====================================================
		#region Formhelperfunctions

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

		#endregion
	}
}
