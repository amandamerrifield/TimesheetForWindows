using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
	public class ReportTimeCardRollup01 : TimecardDetail
	{
		// ====================================
		// Internal Variables
		// ====================================
		public string TaskCategory { get; set; }
		private decimal _totalHoursWeek;

		// ================================
		// Constructor
		// ================================
		public ReportTimeCardRollup01() : base() {
			TaskCategory = "..";
			_totalHoursWeek = 0;
		}

		//=======================================
		// Alternate Constructor
		//=======================================
		public ReportTimeCardRollup01(string taskName) : base(taskName) {
			TaskCategory = "..";
			_totalHoursWeek = 0;
		}

		//=======================================
		// Read Only Property 
		//=======================================
		public decimal TotalHoursForWeek {
			get {
				RefreshWeeklyTotal();
				return _totalHoursWeek;
			}
		}

		// =====================================
		// Refresh the Total Hours
		// =====================================
		public void RefreshWeeklyTotal() {
			_totalHoursWeek = base.GetValueForDay(Timecard.DetailFields.Monday_Hrs);
			_totalHoursWeek += base.GetValueForDay(Timecard.DetailFields.Tuesday_Hrs);
			_totalHoursWeek += base.GetValueForDay(Timecard.DetailFields.Wednesday_Hrs);
			_totalHoursWeek += base.GetValueForDay(Timecard.DetailFields.Thursday_Hrs);
			_totalHoursWeek += base.GetValueForDay(Timecard.DetailFields.Friday_Hrs);
			_totalHoursWeek += base.GetValueForDay(Timecard.DetailFields.Saturday_Hrs);
			_totalHoursWeek += base.GetValueForDay(Timecard.DetailFields.Sunday_Hrs);
		}

	}

}
