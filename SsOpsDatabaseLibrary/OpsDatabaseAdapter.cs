using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using SsOpsDatabaseLibrary.Entity;
using System.Diagnostics;
using System.Globalization;

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
				//_dbConn.ConnectionString = @"Data Source=BigBox;Initial Catalog=SsOperations;Integrated Security=true;";
				_dbConn.ConnectionString = @"Server=184.168.194.70;Database=SSCorpMSSQL03;User Id=Karl;Password=superkf#1;";
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
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.FirstName = (string)reader["FirstName"];
                    emp.LastName = (string)reader["LastName"];
                    emp.TaxIdNbr = (string)reader["TaxIdNbr"];
                    emp.MainPhone = (string)reader["MainPhone"];
                    emp.Gender = (string)reader["Gender"];
                    emp.HireDate = (string)reader["HireDt"];
                    emp.TermDate = (string)reader["TermDt"];
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
        public Employee GetEmployeeById(int employeeId)
        {
            SqlParameter parm;
            Employee emp;
            try
            {
                using (SqlCommand cmd = new SqlCommand("Gsp_GetEmployeeById", _dbConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    parm = new SqlParameter("@employeeId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = employeeId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No record was found for Employee ID number " + employeeId);
                    }
                    emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.FirstName = (string)reader["FirstName"];
                    emp.LastName = (string)reader["LastName"];
                    emp.TaxIdNbr = (string)reader["TaxIdNbr"];
                    emp.MainPhone = (string)reader["MainPhone"];
                    emp.Gender = (string)reader["Gender"];
                    emp.HireDate = (string)reader["HireDt"];
                    emp.TermDate = (string)reader["TermDt"];
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

		public List<Entity.Task> GetActiveTasks()
		{
			//SqlParameter parm;
			List<Entity.Task> tasks = new List<Entity.Task>();
			try
			{
				using (SqlCommand cmd = new SqlCommand("Gsp_GetActiveTasks", _dbConn))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Entity.Task et = new Entity.Task();
						et.TaskId = Convert.ToInt32(reader["TaskId"]);
						et.CategoryId = Convert.ToInt32(reader["TaskCategoryId"]);
						et.TaskName = (string) reader["TaskName"];
						et.BudgetHours = Convert.ToString(reader["BudgetHours"]);
						et.ActualHours = Convert.ToString(reader["ActualHours"]);
						et.StartDate = Convert.ToString(reader["StartDate"]);
						et.EndDate = Convert.ToString(reader["EndDate"]);

						tasks.Add(et);
					}
					reader.Close();
				}
				return tasks;
			}
			catch (Exception ex)
			{
				string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}

		public List<Entity.Task> GetAllTasks()
		{
			//SqlParameter parm;
			List<Entity.Task> tasks = new List<Entity.Task>();
			try
			{
				using (SqlCommand cmd = new SqlCommand("Gsp_GetAllTasks", _dbConn))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Entity.Task et = new Entity.Task();
						et.CategoryId = Convert.ToInt32(reader["CategoryId"]);
						et.CategoryName = Convert.ToString(reader["CategoryName"]);
						et.TaskId = Convert.ToInt32(reader["TaskId"]);
						et.TaskName = (string)reader["TaskName"];
						et.BudgetHours = Convert.ToString(reader["BudgetHours"]);
						et.ActualHours = Convert.ToString(reader["ActualHours"]);
						et.StartDate = Convert.ToString(reader["StartDate"]);
						et.EndDate = Convert.ToString(reader["EndDate"]);

						if (!et.BudgetHours.Contains(".")) et.BudgetHours += ".0";
						if (!et.ActualHours.Contains(".")) et.ActualHours += ".0";

						tasks.Add(et);
					}
					reader.Close();
				}
				return tasks;
			}
			catch (Exception ex)
			{
				string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}
        public List<Entity.Task> GetAllActiveTasks()
        {
            List<Entity.Task> allActiveTasks = new List<Entity.Task>();
            try
            {
                using (SqlCommand cmd =  new SqlCommand("Gsp_GetAllActiveTasks", _dbConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Entity.Task et = new Entity.Task();
                        et.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        et.CategoryName = Convert.ToString(reader["CategoryName"]);
                        et.TaskId = Convert.ToInt32(reader["TaskId"]);
                        et.TaskName = (string)reader["TaskName"];
                        et.BudgetHours = Convert.ToString(reader["BudgetHours"]);
                        et.ActualHours = Convert.ToString(reader["ActualHours"]);
                        et.StartDate = Convert.ToString(reader["StartDate"]);
                        et.EndDate = Convert.ToString(reader["EndDate"]);

                        if (!et.BudgetHours.Contains(".")) et.BudgetHours += ".0";
                        if (!et.ActualHours.Contains(".")) et.ActualHours += ".0";

                        allActiveTasks.Add(et);
                    }
                    reader.Close();
                }
                return allActiveTasks;
            }
            catch (Exception ex)
            {
                string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
        }

        public List<Entity.Employee> GetAllCurrentEmployees() {
            SqlParameter parm;
            List<Entity.Employee> emps = new List<Employee>();

            try {
                using (SqlCommand cmd = new SqlCommand("Gsp_GetAllCurrentEmployees", _dbConn)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    parm = new SqlParameter("@DateCursor", SqlDbType.Char);
                    cmd.Parameters.Add(parm);
                    parm.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read()){
                        Employee emp = new Employee();
                        emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                        emp.FirstName = Convert.ToString(reader["FirstName"]);
                        emp.LastName = Convert.ToString(reader["LastName"]);
                        emp.TaxIdNbr = Convert.ToString(reader["TaxIdNbr"]);
                        emp.MainPhone = Convert.ToString(reader["MainPhone"]);
                        emp.Gender = Convert.ToString(reader["Gender"]);
                        emp.HireDate = Convert.ToString(reader["HireDt"]);
                        emp.TermDate = String.Empty;

                        emps.Add(emp);
                    }
                    reader.Close();
                }
                return emps;
            }
            catch(Exception ex) {
                string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
        }

		public List<Timecard> GetTimecardsForEmployee(int employeeId)
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
						tc.DetailList = new List<TimecardDetail>();
                        tc.EmployeeId = employeeId;
                        tc.TimecardId = Convert.ToInt32(reader["TimecardId"]);
                        tc.WeekNumber = Convert.ToString(reader["WeekNbr"]);
                        tc.Year = Convert.ToString(reader["YearNbr"]);

                        timecards.Add(tc);
                    }
					reader.Close();
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

        public List<TimecardDetail> GetTimecardDetailsByTimecardId(int tcId)
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
                        tcd.DetailId = Convert.ToInt32(reader["TcDetailId"]);
                        tcd.TaskId = Convert.ToInt32(reader["TaskId"]);
                        tcd.TimecardId = Convert.ToInt32(reader["TimecardId"]);

                        tcd.PutValueForDay(Timecard.DetailFields.Monday_Hrs, Convert.ToDecimal(reader["MondayHrs"]));
                        tcd.PutValueForDay(Timecard.DetailFields.Tuesday_Hrs, Convert.ToDecimal(reader["TuesdayHrs"]));
                        tcd.PutValueForDay(Timecard.DetailFields.Wednesday_Hrs, Convert.ToDecimal(reader["WednesdayHrs"]));
                        tcd.PutValueForDay(Timecard.DetailFields.Thursday_Hrs, Convert.ToDecimal(reader["ThursdayHrs"]));
                        tcd.PutValueForDay(Timecard.DetailFields.Friday_Hrs, Convert.ToDecimal(reader["FridayHrs"]));
                        tcd.PutValueForDay(Timecard.DetailFields.Saturday_Hrs, Convert.ToDecimal(reader["SaturdayHrs"]));
                        tcd.PutValueForDay(Timecard.DetailFields.Sunday_Hrs, Convert.ToDecimal(reader["SundayHrs"]));

                        tcd.Task_Name = (string)reader["TaskName"];

                        listTcd.Add(tcd);
                    }
					reader.Close();
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

		public List<TaskCategory> GetTaskCategories() {

			List<TaskCategory> cats = new List<TaskCategory>();
			try {
				using (SqlCommand cmd = new SqlCommand("Gsp_GetTaskCategories", _dbConn)) {
					cmd.CommandType = CommandType.StoredProcedure;
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read()) {
						TaskCategory tc = new TaskCategory();
						tc.CategoryDescription = (string)reader["CategoryDescription"];
						tc.CategoryId = Convert.ToInt32(reader["CategoryId"]);
						tc.CategoryName = (string)reader["CategoryName"];
						tc.IsOverheadYN = (string)reader["IsOverheadYn"];
						cats.Add(tc);
					}
					reader.Close();
				}
				return cats;
			}
			catch (Exception ex) {
				string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}
        public List<ReportTimeCardRollup01> GetTimecardRollupForEmployee(Int16 yearNbr, Int16 begWeekNbr, Int16 endWeekNbr, Int32 employeeId)
        {
            SqlParameter parm;
            List<ReportTimeCardRollup01> tcrList = new List<ReportTimeCardRollup01>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Gsp_GetTimecardRollupForEmployee", _dbConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    parm = new SqlParameter("@YearNumber", SqlDbType.Char);
                    cmd.Parameters.Add(parm);
                    parm.Value = yearNbr.ToString();

                    parm = new SqlParameter("@BeginingWeekNbr", SqlDbType.Char);
                    cmd.Parameters.Add(parm);
                    parm.Value = begWeekNbr.ToString();

                    parm = new SqlParameter("@EndingWeekNbr", SqlDbType.Char);
                    cmd.Parameters.Add(parm);
                    parm.Value = endWeekNbr.ToString();

                    parm = new SqlParameter("@EmployeeId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = employeeId.ToString();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReportTimeCardRollup01 tcr = new ReportTimeCardRollup01();
                        tcr.TaskCategory = (string)reader["CategoryName"];
                        tcr.Task_Name = (string)reader["TaskName"];
                        tcr.PutValueForDay(Timecard.DetailFields.Monday_Hrs, (decimal)reader["Monday"]);
                        tcr.PutValueForDay(Timecard.DetailFields.Tuesday_Hrs, (decimal)reader["Tuesday"]);
                        tcr.PutValueForDay(Timecard.DetailFields.Wednesday_Hrs, (decimal)reader["Wednesday"]);
                        tcr.PutValueForDay(Timecard.DetailFields.Thursday_Hrs, (decimal)reader["Thursday"]);
                        tcr.PutValueForDay(Timecard.DetailFields.Friday_Hrs, (decimal)reader["Friday"]);
                        tcr.PutValueForDay(Timecard.DetailFields.Saturday_Hrs, (decimal)reader["Saturday"]);
                        tcr.PutValueForDay(Timecard.DetailFields.Sunday_Hrs, (decimal)reader["Sunday"]);

                        tcr.RefreshWeeklyTotal();

                        tcrList.Add(tcr);
                    }
                    reader.Close();
                }
                return tcrList;
            }
            catch (Exception ex)
            {
                string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
        }



        public List<ReportTimeCardRollup01> GetTimecardRollup(Int16 yearNbr, Int16 begWeekNbr, Int16 endWeekNbr) {
			SqlParameter parm;
			List<ReportTimeCardRollup01> tcrList = new List<ReportTimeCardRollup01>();
			try {
				using (SqlCommand cmd = new SqlCommand("Gsp_GetTimecardRollup", _dbConn)) {
					cmd.CommandType = CommandType.StoredProcedure;

					parm = new SqlParameter("@YearNumber", SqlDbType.Char);
					cmd.Parameters.Add(parm);
					parm.Value = yearNbr.ToString();

					parm = new SqlParameter("@BeginingWeekNbr", SqlDbType.Char);
					cmd.Parameters.Add(parm);
					parm.Value = begWeekNbr.ToString();

					parm = new SqlParameter("@EndingWeekNbr", SqlDbType.Char);
					cmd.Parameters.Add(parm);
					parm.Value = endWeekNbr.ToString();

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read()) {
						ReportTimeCardRollup01 tcr = new ReportTimeCardRollup01();
						tcr.TaskCategory = (string)reader["CategoryName"];
						tcr.Task_Name = (string) reader["TaskName"];
						tcr.PutValueForDay(Timecard.DetailFields.Monday_Hrs, (decimal)reader["Monday"]);
						tcr.PutValueForDay(Timecard.DetailFields.Tuesday_Hrs, (decimal)reader["Tuesday"]);
						tcr.PutValueForDay(Timecard.DetailFields.Wednesday_Hrs, (decimal)reader["Wednesday"]);
						tcr.PutValueForDay(Timecard.DetailFields.Thursday_Hrs, (decimal)reader["Thursday"]);
						tcr.PutValueForDay(Timecard.DetailFields.Friday_Hrs, (decimal)reader["Friday"]);
						tcr.PutValueForDay(Timecard.DetailFields.Saturday_Hrs, (decimal)reader["Saturday"]);
						tcr.PutValueForDay(Timecard.DetailFields.Sunday_Hrs, (decimal)reader["Sunday"]);

						tcr.RefreshWeeklyTotal();

						tcrList.Add(tcr);
					}
					reader.Close();
				}
				return tcrList;
			}
			catch (Exception ex) {
				string errTitle = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}



		#endregion

		// ======================================================== 
		#region Public Functions that return a newly generated primary key

		public int CreateTimeCard(Timecard tcard)
        {
			int retVal = 0;
            SqlParameter parm;

            try
            {
                using (SqlCommand cmd = new SqlCommand("Isp_CreateTimecard", _dbConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    parm = new SqlParameter("@YearNbr", SqlDbType.Int);
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

		public int CreateTaskCategory(TaskCategory tc)
		{
			int retVal = 0;
			SqlParameter parm;

			try
			{
				using (SqlCommand cmd = new SqlCommand("Isp_CreateTaskCategory", _dbConn))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					parm = new SqlParameter("@CategoryDescription", SqlDbType.VarChar);
					cmd.Parameters.Add(parm);
					parm.Value = tc.CategoryDescription;


					parm = new SqlParameter("@CategoryName", SqlDbType.VarChar);
					cmd.Parameters.Add(parm);
					parm.Value = tc.CategoryName;

					parm = new SqlParameter("@IsOverheadYN", SqlDbType.NChar);
					cmd.Parameters.Add(parm);
					parm.Value = tc.IsOverheadYN;

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

        public int CreateTask(Entity.Task tsk) {
            int retVal = 0;
            SqlParameter parm;

            try {
                using (SqlCommand cmd = new SqlCommand("Isp_CreateTask", _dbConn)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    parm = new SqlParameter("@TaskCategoryId", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = tsk.CategoryId;

                    parm = new SqlParameter("@TaskName", SqlDbType.VarChar);
                    cmd.Parameters.Add(parm);
                    parm.Value = tsk.TaskName;

                    parm = new SqlParameter("@BudgetHours", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = tsk.BudgetHours;

                    parm = new SqlParameter("@ActualHours", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = tsk.ActualHours ;

                    parm = new SqlParameter("@StartDate", SqlDbType.NChar);
                    cmd.Parameters.Add(parm);
                    parm.Value = tsk.StartDate;

                    parm = new SqlParameter("@EndDate", SqlDbType.NChar);
                    cmd.Parameters.Add(parm);
					if(String.IsNullOrEmpty(tsk.EndDate)) {
						parm.Value = DBNull.Value;
					}
					else {
						parm.Value = tsk.EndDate;
					}

                    parm = new SqlParameter("@NewIdentity", SqlDbType.Int);
                    cmd.Parameters.Add(parm);
                    parm.Value = 0;

                    retVal = (Int32)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex) {
                string errTitle = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
            return retVal;
        }


        #endregion

        // ================================================================
        #region Public Functions that update the entity that you pass to it

        public void CreateTimeCardDetail(Timecard tcard) {
			SqlParameter parm;

			try {
				//Must have EmployeeID, TimecardId, and each row must have a TaskId
				bool isMissingKey = false;
				isMissingKey = (tcard.EmployeeId == 0 || tcard.TimecardId == 0);
				foreach (TimecardDetail detail in tcard.DetailList) {
					if (detail.TaskId == 0) {
						isMissingKey = true;
						break;
					}
				}
				if (isMissingKey) {
					throw new Exception("Unable to create timecard detail.  A key value is missing.");
				}
				//Must have something to insert
				bool isNothingToInsert = true;
				foreach (TimecardDetail tcd in tcard.DetailList) {
					if (tcd.DetailId == 0) {
						isNothingToInsert = false;
						break;
					}
				}
				if (isNothingToInsert) { return; }

				// Okay to insert these new rows. Do it in a transaction (all rows or none)
				using (SqlCommand cmd = new SqlCommand("Isp_InsertTimeCardDetail", _dbConn)) {
					SqlTransaction xaction = _dbConn.BeginTransaction();
                    cmd.Transaction = xaction;
					cmd.CommandType = CommandType.StoredProcedure;

					foreach (TimecardDetail tcd in tcard.DetailList) {
						//Do not attempt to insert detail rows that are already in the DB
						if (tcd.DetailId == 0) {
                            cmd.Parameters.Clear();
                            
							parm = new SqlParameter("@TaskId", SqlDbType.Int);
							cmd.Parameters.Add(parm);
							parm.Value = tcd.TaskId;

							parm = new SqlParameter("@TimecardId", SqlDbType.Int);
							cmd.Parameters.Add(parm);
							parm.Value = tcard.TimecardId;

							parm = new SqlParameter("@MondayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Monday_Hrs);

							parm = new SqlParameter("@TuesdayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Tuesday_Hrs);

							parm = new SqlParameter("@WednesdayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Wednesday_Hrs);

							parm = new SqlParameter("@ThursdayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Thursday_Hrs);

							parm = new SqlParameter("@FridayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Friday_Hrs);

							parm = new SqlParameter("@SaturdayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Saturday_Hrs);

							parm = new SqlParameter("@SundayHrs", SqlDbType.SmallMoney);
							cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Sunday_Hrs);

                            parm = new SqlParameter("@NewIdentity", SqlDbType.Int);
                            cmd.Parameters.Add(parm);
                            parm.Value = 0;

                            // Update caller's row with newly created ID
                            tcd.DetailId = Convert.ToInt32(cmd.ExecuteScalar());
						}
					}
					// Commit the transaction to the database
					xaction.Commit();
				}
			}
			catch (Exception ex) {
				string errTitle = System.Reflection.MethodBase.GetCurrentMethod().Name;
				LogHardErrorMessage(errTitle, ex.Source, ex.Message);
				throw;
			}
		}
        public void UpdateTimeCardDetail(Timecard tcard) {
            SqlParameter parm;

            try {
                //Must have EmployeeID, TimecardId, and each row must have a TaskId
                bool isMissingKey = false;
                isMissingKey = (tcard.EmployeeId == 0 || tcard.TimecardId == 0);
                foreach (TimecardDetail detail in tcard.DetailList) {
                    if (detail.TaskId == 0) {
                        isMissingKey = true;
                        break;
                    }
                }
                if (isMissingKey) {
                    throw new Exception("Unable to update timecard detail.  A key value is missing.");
                }
                //Must have something to insert
                bool isNothingToUpdate = true;
                foreach (TimecardDetail tcd in tcard.DetailList) {
                    if (tcd.DetailId != 0) {
                        isNothingToUpdate = false;
                        break;
                    }
                }
                if (isNothingToUpdate) { return; }

                // Okay to update these existing rows. Do it in a transaction (all rows or none)
                using (SqlCommand cmd = new SqlCommand("Usp_UpdateTimeCardDetail", _dbConn)) {
                    SqlTransaction xaction = _dbConn.BeginTransaction();
                    cmd.Transaction = xaction;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (TimecardDetail tcd in tcard.DetailList) {
                        //Do not attempt to update detail rows that are NOT in the DB
                        if (tcd.DetailId != 0) {

                            cmd.Parameters.Clear();

                            parm = new SqlParameter("@TcDetailId", SqlDbType.Int);
                            cmd.Parameters.Add(parm);
                            parm.Value = Convert.ToInt32(tcd.DetailId);


                            parm = new SqlParameter("@MondayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Monday_Hrs);

                            parm = new SqlParameter("@TuesdayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Tuesday_Hrs);

                            parm = new SqlParameter("@WednesdayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Wednesday_Hrs);

                            parm = new SqlParameter("@ThursdayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Thursday_Hrs);

                            parm = new SqlParameter("@FridayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Friday_Hrs);

                            parm = new SqlParameter("@SaturdayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Saturday_Hrs);

                            parm = new SqlParameter("@SundayHrs", SqlDbType.SmallMoney);
                            cmd.Parameters.Add(parm);
                            parm.Value = tcd.GetValueForDay(Timecard.DetailFields.Sunday_Hrs);

                            cmd.ExecuteScalar();
                        }
                    }
                    // Commit the transaction to the database
                    xaction.Commit();
                }
            }
            catch (Exception ex) {
                string errTitle = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHardErrorMessage(errTitle, ex.Source, ex.Message);
                throw;
            }
        }

        #endregion

        // ================================================================
        #region Public Functions that return Void
        public void DeleteTimeCardDetail(List<TimecardDetail> detailsToDelete) {
            SqlParameter parm;

            try {
                bool isMissingKey = false;
                foreach (TimecardDetail detail in detailsToDelete) {
                    if (detail.DetailId == 0) {
                        isMissingKey = true;
                        break;
                    }
                }
                if (isMissingKey) {
                    throw new Exception("Unable to delete timecard detail.  A key value is missing.");
                }
                using (SqlCommand cmd = new SqlCommand("Dsp_DeleteTimeCardDetail", _dbConn)) {
                    foreach(TimecardDetail tcd in detailsToDelete) {
                        cmd.Parameters.Clear();
                        parm = new SqlParameter("@TcDetailId", SqlDbType.Int);
                        cmd.Parameters.Add(parm);
                        parm.Value = tcd.DetailId;
                        cmd.ExecuteScalar();
                    }
                }
                return;
            }
            catch (Exception ex) {
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
                "--- Message: " + msg,
				String.Empty
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
