﻿using System;
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

        //===================================================
        // Functions that return a list of entity types
        public List<Timecard> GetTimecardsForEmployee(string employeeId)
        {
            SqlParameter parm;
            List<Timecard> timecards = new List<Timecard>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Gsp_GetTimecardsByEmployeeId", _dbConn))
                {
                    parm = new SqlParameter("@employeeId", SqlDbType.VarChar);
                    cmd.Parameters.Add(parm);
                    parm.Value = employeeId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        Timecard tc = new Timecard();
                        tc.DetailTable = null;
                        tc.EmployeeId = (string)reader["EmployeeId"];
						tc.TimecardId = (string)reader["TimecardId"];
                        tc.WeekNumber = (string)reader["WeekNbr"];
                        tc.Year = (string)reader["Year"];

						timecards.Add(tc);
                    }
					// if there were no timecards, returned list.count will be 0.
                }
                return timecards;
            }
            catch (Exception ex)
            {
                string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
        }

        public List<TimecardDetail> GetTimecardDetailsByTimecardId(string tcId)
        {
            SqlParameter parm;
            List<TimecardDetail> listTcd = new List<TimecardDetail>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Gsp_GetTimecardDetailByTimecardId",_dbConn))
                {
                    parm = new SqlParameter("@timecardId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = tcId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        TimecardDetail tcd = new TimecardDetail();
                        tcd.Detail_ID = (string) reader["TcDetailId"];
                        tcd.Task_ID = (string) reader["TaskId"];
                        tcd.Timecard_ID = (string) reader["TimecardId"];
                        tcd.Monday_Hrs = (string) reader["MondayHrs"];
                        tcd.Tuesday_Hrs = (string) reader["TuesdayHrs"];
                        tcd.Wednesday_Hrs = (string) reader["WednesdayHrs"];
                        tcd.Thursday_Hrs = (string) reader["ThursdayHrs"];
                        tcd.Friday_Hrs = (string) reader["FridayHrs"];
                        tcd.Saturday_Hrs = (string) reader["SaturdayHrs"];
                        tcd.Sunday_Hrs = (string) reader["SundayHrs"];

                        listTcd.Add(tcd);
                    }
                }
                return listTcd;
            }
            catch(Exception ex)
            {
                string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }

        }



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
