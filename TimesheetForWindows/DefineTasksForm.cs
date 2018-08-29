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
	public partial class DefineTasksForm : Form
	{
		private List<SsOpsDatabaseLibrary.Entity.Task> _defineTasks;

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

		private void DefineTasksForm_Load(object sender, EventArgs e)
		{
			GetAllTasks();
			this.dataGridView1.DataSource = _defineTasks;
		}
	}
}
