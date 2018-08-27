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
    public sealed class OpsDatabaseAdapter : IDisposable
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
        public OpsDatabaseAdapter()
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

        //=========================================================
        #region Functions that return a single Entity

        public Employee GetEmployeeByName(string firstName, string lastName)
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

        #endregion

        //=========================================================
        #region Functions that return List<Entity>

        public List<Timecard> GetTimecardsForEmployee(string employeeId)
        {
            SqlParameter parm;
            List<Timecard> timecards = new List<Timecard>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Gsp_GetTimecardsByEmployeeId", _dbConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    parm = new SqlParameter("@employeeId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = Convert.ToInt32(employeeId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Timecard tc = new Timecard();
                        tc.DetailTable = null;
                        tc.EmployeeId = employeeId;
                        tc.TimecardId = Convert.ToString(reader["TimecardId"]);
                        tc.WeekNumber = Convert.ToString(reader["WeekNbr"]);
                        tc.Year = Convert.ToString(reader["Year"]);

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
                using (SqlCommand cmd = new SqlCommand("Gsp_GetTimecardDetailByTimecardId", _dbConn))
                {
					cmd.CommandType = CommandType.StoredProcedure;

					parm = new SqlParameter("@timecardId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = tcId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TimecardDetail tcd = new TimecardDetail();
                        tcd.Detail_ID = Convert.ToString(reader["TcDetailId"]);
                        tcd.Task_ID = Convert.ToString(reader["TaskId"]);
                        tcd.Timecard_ID = Convert.ToString(reader["TimecardId"]);

						// We want hours format to be 99.9 and we have 4 chars, so if decimal point in position 1, then string m/b 3 chars long.
						tcd.Monday_Hrs = Convert.ToString(reader["MondayHrs"]).Substring(0,4);
						if (tcd.Monday_Hrs.Substring(1, 1) == ".") tcd.Monday_Hrs = tcd.Monday_Hrs.Substring(0, 3);

						tcd.Tuesday_Hrs = Convert.ToString(reader["TuesdayHrs"]).Substring(0, 4);
						if (tcd.Tuesday_Hrs.Substring(1, 1) == ".") tcd.Tuesday_Hrs = tcd.Tuesday_Hrs.Substring(0, 3);

						tcd.Wednesday_Hrs = Convert.ToString(reader["WednesdayHrs"]).Substring(0, 4);
						if (tcd.Wednesday_Hrs.Substring(1, 1) == ".") tcd.Wednesday_Hrs = tcd.Wednesday_Hrs.Substring(0, 3);

						tcd.Thursday_Hrs = Convert.ToString(reader["ThursdayHrs"]).Substring(0, 4);
						if (tcd.Thursday_Hrs.Substring(1, 1) == ".") tcd.Thursday_Hrs = tcd.Thursday_Hrs.Substring(0, 3);

						tcd.Friday_Hrs = Convert.ToString(reader["FridayHrs"]).Substring(0, 4);
						if (tcd.Friday_Hrs.Substring(1, 1) == ".") tcd.Friday_Hrs = tcd.Friday_Hrs.Substring(0, 3);

						tcd.Saturday_Hrs = Convert.ToString(reader["SaturdayHrs"]).Substring(0, 4);
						if (tcd.Saturday_Hrs.Substring(1, 1) == ".") tcd.Saturday_Hrs = tcd.Saturday_Hrs.Substring(0, 3);

						tcd.Sunday_Hrs = Convert.ToString(reader["SundayHrs"]).Substring(0, 4);
						if (tcd.Sunday_Hrs.Substring(1, 1) == ".") tcd.Sunday_Hrs = tcd.Sunday_Hrs.Substring(0, 3);

						tcd.TaskName = (string)reader["TaskName"];

                        listTcd.Add(tcd);
                    }
                }
                return listTcd;
            }
            catch (Exception ex)
            {
                string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }

        }

        #endregion

        // ========================================================
        #region Public Functions that return a newly generated primary key

        public Int32 CreateTimeCard(Timecard tcard)
        {
            Int32 retVal = 0;
            SqlParameter parm;

            try
            {
                using (SqlCommand cmd = new SqlCommand("Isp_CreateTimecard", _dbConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    parm = new SqlParameter("@Year", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = Convert.ToInt16(tcard.Year);

                    parm = new SqlParameter("@WeekNbr", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = Convert.ToInt16(tcard.WeekNumber);

                    parm = new SqlParameter("@EmployeeId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = Convert.ToInt16(tcard.EmployeeId);

                    parm = new SqlParameter("@NewIdentity", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = 0;

                    retVal = (Int32)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                string errTitle = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
            return retVal;
        }

        #endregion

        // =======================================================
        #region Public Functions that return an updated DataTable

        public void CreateTimeCardDetail(ref Timecard tcard)
        {
            SqlParameter parm;

            try
            {
                //Must have EmployeeID, TimecardId, and each row must have a TaskId
                bool isMissingKey = false;
                isMissingKey = (String.IsNullOrEmpty(tcard.EmployeeId) || String.IsNullOrEmpty(tcard.TimecardId));
                foreach (DataRow row in tcard.DetailTable.Rows)
                {
                    if (String.IsNullOrEmpty((string)row[(int)Timecard.DetailFields.Task_ID]))
                    {
                        isMissingKey = true;
                        break;
                    }
                }
                if (isMissingKey)
                {
                    throw new Exception("Unable to create timecard detail.  A key value is missing.");
                }

                // Okay to insert these new rows. Do it in a transaction (all or none)
                using (SqlCommand cmd = new SqlCommand("sp_InsertTimeCardDetail", _dbConn))
                {
                    SqlTransaction xaction = _dbConn.BeginTransaction();

                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DataRow row in tcard.DetailTable.Rows)
                    {
                        parm = new SqlParameter("@TcDetailId", SqlDbType.Int);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToInt32(row[(int)Timecard.DetailFields.Detail_ID]);

                        parm = new SqlParameter("@TaskId", SqlDbType.Int);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToInt32(row[(int)Timecard.DetailFields.Task_ID]);

                        parm = new SqlParameter("@TimecardId", SqlDbType.Int);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToInt32(row[(int)Timecard.DetailFields.Timecard_ID]);

                        parm = new SqlParameter("@MondayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Monday_Hrs]);

                        parm = new SqlParameter("@TuesdayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Tuesday_Hrs]);

                        parm = new SqlParameter("@WednesdayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Wednesday_Hrs]);

                        parm = new SqlParameter("@ThursdayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Thursday_Hrs]);

                        parm = new SqlParameter("@FridayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Friday_Hrs]);

                        parm = new SqlParameter("@SaturdayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Saturday_Hrs]);

                        parm = new SqlParameter("@SundayHrs", SqlDbType.SmallMoney);
                        cmd.Parameters.Add(parm);
                        parm.Value = Convert.ToDecimal(row[(int)Timecard.DetailFields.Sunday_Hrs]);

                        // Update caller's row with newly created ID
                        row[(int)Timecard.DetailFields.Detail_ID] = (Int32)cmd.ExecuteScalar();
                    }
                    // Commit the transaction to the database
                    xaction.Commit();
                }
            }
            catch (Exception ex)
            {
                string errTitle = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
        }

        #endregion


        // ========================================================
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

        // ========================================================
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
