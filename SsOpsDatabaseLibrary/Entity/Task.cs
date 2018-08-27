using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
    public class Task
    {
        public Task() { }

        public string TaskId { get; set; }
        public string TaskCategoryId { get; set; }
        public string TaskName { get; set; }
        public string BudgetHours { get; set; }
        public string ActualHours { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
		public override String ToString()
		{
			return TaskName;
		}
	}
}
