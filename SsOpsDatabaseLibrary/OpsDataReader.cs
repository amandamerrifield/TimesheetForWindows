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
		//   using (OpsDataReader dbReader = new OpsDataReader() {
		//      dbLib.WriteSomething;
		//   }
		public const string TIMESTAMP_FORMAT = "yyyy-MM-dd_HH:mm:ss_fff";
		public const string SHORTDATE_FORMAT = "yyyy-MM-dd";

		private static string UniqueTimeStamp = String.Empty;

		private bool _disposed = false;
		private SqlConnection _dbConn;

		//Default Constructor
		public OpsDataReader()
		{
			try
			{
				_dbConn = new SqlConnection();
				_dbConn.ConnectionString = @"Data Source=BigBox;Initial Catalog=SsOperations;Integrated Security=true;";
				_dbConn.Open();
			}
			catch (Exception ex)
			{
				string errTitle = this.GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}

		//Constructor Overload
		public OpsDataReader(string connectnString)
		{
			try
			{
				//Must have a database connection string
				_dbConn = new SqlConnection();
				_dbConn.ConnectionString = connectnString;
				_dbConn.Open();
			}
			catch(Exception ex)
			{
				string errTitle = this.GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
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
					emp.EmployeeId = reader["EmployeeId"].ToString();
					emp.FirstName = (string)reader["FirstName"];
					emp.LastName = (string)reader["LastName"];
					emp.TaxIdNbr = (string)reader["TaxIdNbr"];
					emp.SalaryYn = (string)reader["SalaryYn"];
					emp.MainPhone = (string)reader["MainPhone"];
					emp.Gender = (string)reader["Gender"];
					emp.HireDate = (string)reader["HiredDt"];
					emp.TerminationDate = (string)reader["TerminationDt"];
				}
				return emp;
			}
			catch (Exception ex)
			{
				string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}
		//public Timecard[] GetTimecardsForEmployee(int employeeId)
		//{
		//	SqlParameter parm;
		//}

		// =================================================
		#region Error Handling Support

		public void LogHardErrorMessage(string classNamAndMethod, string source, string msg)
		{
			//Hard errors are ones that cant be appended to the database errors table
			string[] msgs = new string[]
			{
				"=== " + DateTime.Now.ToString(TIMESTAMP_FORMAT) + " === ",
				"--- " + classNamAndMethod + " encountered an error: ",
				"--- Source : " + source,
				"--- Message: " + msg
			};

			string fileSpec = Directory.GetCurrentDirectory() + @"\ErrorLog.txt";
			// Swallow any errors and work thru
			try
			{
				LogMessages(msgs, fileSpec);
			}
			finally { }
		}

		public void LogMessages(string[] messages, string filespec)
		{
			File.AppendAllLines(filespec, messages);

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
