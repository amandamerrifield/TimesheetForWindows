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
	public partial class SelectTaskForm : Form
	{
		private bool _canceled = false;
		// Constructor
		public SelectTaskForm(List<SsOpsDatabaseLibrary.Entity.Task> tasks)
		{
			InitializeComponent();
			
			// We will manually control the form location on screen
			this.StartPosition = FormStartPosition.Manual;

			if (tasks.Count < 1)
			{
				throw new Exception("Error! We must have at least one task");
			}

			listBox1.Items.AddRange(tasks.ToArray<SsOpsDatabaseLibrary.Entity.Task>());
		}



		public SsOpsDatabaseLibrary.Entity.Task[] GetSelectedTasks()
		{
			SsOpsDatabaseLibrary.Entity.Task[] outTsks = new SsOpsDatabaseLibrary.Entity.Task[listBox1.SelectedItems.Count];
			listBox1.SelectedItems.CopyTo(outTsks,0);

			if (_canceled) return null;
			return outTsks;
		}


		private void button1_Click(object sender, EventArgs e)
		{
			_canceled = false;
			this.Visible = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_canceled = true;
			this.Visible = false;
		}

		private void SelectTaskForm_Load(object sender, EventArgs e)
		{
			listBox1.SelectedIndex = 0;
		}
	}
}
