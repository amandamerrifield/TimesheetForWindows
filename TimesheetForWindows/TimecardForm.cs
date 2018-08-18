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
	public partial class TimecardForm : Form
	{
		private enum FormState
		{
			ViewingData,
			ViewingPotentialChanges,
			SavingChanges
		}
		private FormState _currentFormState;

		//
		// Form Constructor
		//
		public TimecardForm()
		{
			InitializeComponent();
		}
	}
}
