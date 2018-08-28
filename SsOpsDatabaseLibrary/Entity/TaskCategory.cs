using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
	public class TaskCategory
    {
		public TaskCategory()
		{

		}
		public string CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		public string IsOverheadYN { get; set; }
	}
}
