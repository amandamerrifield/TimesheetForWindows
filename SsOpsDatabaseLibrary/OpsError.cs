using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary
{
	public class OpsError
	{
		private string _timeStamp;
		private string _userName;
		private string _errSource;
		private string _errDescription;

		public OpsError(Exception ex)
		{
			_errSource = ex.Source;
			_errDescription = ex.Message;
			_timeStamp = DateTime.Now.ToString(OpsDatabaseAdapter.TIMESTAMP_FORMAT);
			_userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
		}

	}
}
