using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SsOpsDatabaseLibrary.Entity
{
	public class Timecard
	{
		public enum DetailFields
		{
			Detail_ID,
			Task_ID,
			Timecard_ID,
			Monday_Hrs,
			Tuesday_Hrs,
			Wednesday_Hrs,
			Thursday_Hrs,
			Friday_Hrs,
			Saturday_Hrs,
			Sunday_Hrs
		}
		public Timecard() { }

		public string TimecardId {get;set;}
        public string Year { get; set; }
        public string WeekNumber { get; set; }
        public string EmployeeId { get; set; }
        public DataTable DetailTable { get; set; }
       
    }
}
