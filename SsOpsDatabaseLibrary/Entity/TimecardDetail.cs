using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
	public class TimecardDetail
	{
		public TimecardDetail() { }

		public string Detail_ID { get; set; }
		public string Task_ID { get; set; }
		public string Timecard_ID { get; set; }
		public string Monday_Hrs { get; set; }
		public string Tuesday_Hrs { get; set; }
		public string Wednesday_Hrs { get; set; }
		public string Thursday_Hrs { get; set; }
		public string Friday_Hrs { get; set; }
		public string Saturday_Hrs { get; set; }
		public string Sunday_Hrs { get; set; }
		public string TaskName { get; set; }
	}
}
