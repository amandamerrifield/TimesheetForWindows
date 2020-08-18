using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SsOpsDatabaseLibrary.Entity
{
	public class Timecard {
		public enum DetailFields {
			Monday_Hrs,
			Tuesday_Hrs,
			Wednesday_Hrs,
			Thursday_Hrs,
			Friday_Hrs,
			Saturday_Hrs,
			Sunday_Hrs
		}
		public Timecard() { }

		public string TimecardId { get; set; }
		public string Year { get; set; }
		public string WeekNumber { get; set; }
		public string EmployeeId { get; set; }
		//public DataTable DetailTable { get; set; } Too heavy and more complex than necessary
		public List<TimecardDetail> DetailList {get; set; }

		// Override ToString() for use in combo boxes
		public override string ToString()
		{
			return Year + " Week " + WeekNumber;
		}

	}
}
