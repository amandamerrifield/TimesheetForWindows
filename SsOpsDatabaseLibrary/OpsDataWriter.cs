using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using SsOpsDatabaseLibrary.Entity;


namespace SsOpsDatabaseLibrary
{
    public sealed class OpsDataWriter : IDisposable
    {
		// This class is designed for usage pattern as follows:
		//   using (OpsDataWriter dbWriter = new OpsDataWriter(connectionString) {
		//      dbLib.WriteSomething;
		//   }

		public const string TIMESTAMP_FORMAT = "yyyy-MM-dd_HH:mm:ss_fff";
		public const string SHORTDATE_FORMAT = "yyyy-MM-dd";

		private static string UniqueTimeStamp = String.Empty;
		private bool _disposed = false;
		private SqlConnection _dbConn;

		//Constructor
		public OpsDataWriter(string connectnString) {
			//Must have a database connection string
			_dbConn = new SqlConnection();
			_dbConn.ConnectionString = connectnString;
			//Open a connection to the DB for the life of this instance
			_dbConn.Open();
		}

		// =================================================
		#region Public Functions that return a newly generated primary key

		public Int32 CreateTimeCard(Timecard tcard)
		{
			Int32 retVal = 0;
			SqlParameter parm;

			using(SqlCommand cmd = new SqlCommand("sp_InsertTimeCard", _dbConn))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				parm = new SqlParameter("@Year", SqlDbType.Int);
				cmd.Parameters.Add(parm);
				parm.Value = Convert.ToInt16(tcard.Year);

				parm = new SqlParameter("@WeekNbr", SqlDbType.Int);
				cmd.Parameters.Add(parm);
				parm.Value = Convert.ToInt16(tcard.Year);

				parm = new SqlParameter("@EmployeeId", SqlDbType.Int);
				cmd.Parameters.Add(parm);
				parm.Value = Convert.ToInt16(tcard.Year);

				retVal = (Int32) cmd.ExecuteScalar();
			}

			return retVal;
		}

		#endregion

		// =================================================
		#region Error Handling Support

		public void LogHardErrorMessage(string title, string msg)
		{
			//Hard errors are ones that cant be appended to the database errors table
			string post = "=== " + DateTime.Now.ToString(TIMESTAMP_FORMAT) + " ===" + "\n" + title + "\n" + msg + "\n";
			string fileSpec = Directory.GetCurrentDirectory() + @"\ErrorLog.txt";
			// Swallow any errors and work thru
			try
			{
				LogMessage(post, fileSpec);
			}
			finally {}
		}

		public void LogMessage(string message, string fileSpec)
		{
			TextWriter tw = new StreamWriter(fileSpec);
			tw.WriteLine(message);
			tw.Close();
		}

		#endregion

		// =================================================
		#region IDisposable Support

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if(disposing)
				{
					if (_dbConn != null) {
						if (_dbConn.State == ConnectionState.Open) _dbConn.Close();
						_dbConn.Dispose();
					}
				}
				_dbConn = null;
			}
			_disposed = true;
		}
		#endregion
	}
}
