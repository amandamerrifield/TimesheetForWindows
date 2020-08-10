using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
    public class Employee
    {
        public Employee() { }

		public string EmployeeId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TaxIdNbr { get; set; }
		public string MainPhone { get; set; }
		public string Gender { get; set; }
		public string HireDate { get; set; }
		public string TermDate { get; set; }

	}
}
