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
    public sealed class OpsDataReader : IDisposable
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

		public OpsDataReader()
		{
			_dbConn = new SqlConnection();
			_dbConn.ConnectionString = @"Data Source=BigBox\SQLExpress;Initial Catalog=SsOperations;Integrated Security=true;";
			_dbConn.Open();
		}

		public OpsDataReader(string connectnString)
		{
			//Must have a database connection string
			_dbConn = new SqlConnection();
			_dbConn.ConnectionString = connectnString;
			//Open a connection to the DB for the life of this instance
			_dbConn.Open();
		}

		// =================================================
		// Functions that return a single record
		//public DataRow GetEmployeeById(int employeeId) 	{}
		public Employee GetEmployeeByName(string firstName, string lastName )
		{
			SqlParameter parm;
			Employee emp;
			try
			{
				using (SqlCommand cmd = new SqlCommand("Gsp_GetEmployeeByName", _dbConn))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					parm = new SqlParameter("@firstName", SqlDbType.VarChar);
					cmd.Parameters.Add(parm);
					parm.Value = firstName;

					parm = new SqlParameter("@lastName", SqlDbType.VarChar);
					cmd.Parameters.Add(parm);
					parm.Value = lastName;

					SqlDataReader reader = cmd.ExecuteReader();
					if (!reader.Read())
					{
						throw new InvalidOperationException("No record was found for " + firstName + " " + lastName);
					}
					emp = new Employee();
					emp.EmployeeId = reader["EmloyeeId"].ToString();
					emp.FirstName = (string)reader["FirstName"];
					emp.LastName = (string)reader["LastName"];
					emp.TaxIdNbr = (string)reader["TaxIdNbr"];
					emp.SalaryYn = (string)reader["SalaryYn"];
					emp.MainPhone = (string)reader["MainPhone"];
					emp.Gender = (string)reader["Gender"];
					emp.HireDate = (string)reader["HireDt"];
					emp.TerminationDate = (string)reader["TerminationDt"];
				}
				return emp;
			}
			catch (Exception ex)
			{
				string errTitle = System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}

		// =================================================
		#region Error Handling Support

		public void LogHardErrorMessage(string methodName, string source, string msg)
		{
			//Hard errors are ones that cant be appended to the database errors table
			StringBuilder sbldr = new StringBuilder("=== " + DateTime.Now.ToString(TIMESTAMP_FORMAT) + " === " + "\n");
			sbldr.Append("--- " + methodName + " encountered an error: \n");
			sbldr.Append("--- Source: " + source + "\n");
			sbldr.Append("--- " + msg);

			string fileSpec = Directory.GetCurrentDirectory() + @"\ErrorLog.txt";
			// Swallow any errors and work thru
			try
			{
				LogMessage(sbldr.ToString(), fileSpec);
			}
			finally { }
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
				if (disposing)
				{
					if (_dbConn != null)
					{
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
