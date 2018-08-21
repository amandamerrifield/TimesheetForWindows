using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace TimesheetForWindows
{
    
    public partial class MainForm : Form
    {
		//private string _dataFilePath;
                
        private Form _timecardForm;
        private Form _currentActiveForm;
		private int _employeeId = 0;

        // MainForm Constructor
        public MainForm() : base()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
			var thisMethod = MethodBase.GetCurrentMethod();
			Console.Write("****" + thisMethod.Name + "\n");

			//The current employeeId is the urrent user's employee ID
			_employeeId = 100;

			// Instantiate the forms that the MainForm controls.
			_timecardForm = new TimecardForm(_employeeId);
			_timecardForm.Visible = false;

            // The current active form is the one the user is working
            _currentActiveForm = null;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            var thisMethod = MethodBase.GetCurrentMethod();
            Console.Write("****" + thisMethod.Name + "\n");

            if(_currentActiveForm != null) _currentActiveForm.Visible = false;
        }

		//private void MainForm_Activate(object sender, System.EventArgs e) {}
		//private void MainForm_Deactivate(object sender, System.EventArgs e) {}
		//private void MainForm_Enter(object sender, System.EventArgs e) {}
		//private void MainForm_GotFocus(object sender, System.EventArgs e) { }
		//private void MainForm_LostFocus(object sender, System.EventArgs e) {}
		//private void MainForm_Move(object sender, System.EventArgs e) {}
		//private void MainForm_Shown(object sender, System.EventArgs e) {}

        private void btnEnterHours_Click(object sender, EventArgs e)
        {
			var thisMethod = MethodBase.GetCurrentMethod();
			Console.Write("****" + thisMethod.Name + "\n");

            // Make the current active form invisible, then show our timecard form
            if (_currentActiveForm != null) _currentActiveForm.Visible = false;

            // The new current active form is our employee form
            _currentActiveForm = _timecardForm;

            // And now it is positioned relative to ourself and made visible
            Point targetPoint = this.Location;
            targetPoint.X = this.Location.X + 170;
            targetPoint.Y = this.Location.Y + 25;
            _currentActiveForm.Location = targetPoint;
            _currentActiveForm.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {

        }


        

    }
}
