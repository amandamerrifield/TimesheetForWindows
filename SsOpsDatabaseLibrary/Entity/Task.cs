using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
	public class Task : UnCategorizedTask
	{
		public Task() : base() { }

		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}

	public class UnCategorizedTask
	{
		public UnCategorizedTask() { }

		public int TaskId { get; set; }
		public string TaskName { get; set; }
		public string BudgetHours { get; set; }
		public string ActualHours { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public override String ToString() {
			return TaskName;
		}
	}
}
