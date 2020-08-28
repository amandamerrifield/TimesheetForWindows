using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsOpsDatabaseLibrary.Entity
{
	public class TimecardDetail
	{

        // ====================================
        // Internal Variables
        // ====================================
        public int DetailId { get; set; }
        public int TaskId { get; set; }
        public int TimecardId { get; set; }
        public string Task_Name { get; set; }

        private decimal[] dailyHours;

        // ================================
        // Constructor
        // ================================
        public TimecardDetail() {
            //initialiize new instance with all blank entries
            dailyHours = new decimal[7];
            for (int i = 0; i < dailyHours.Length; i++) {
                dailyHours[i] = 0;
            }
            DetailId = 0;
            TaskId = 0;
            TimecardId = 0;
            Task_Name = "..";
        }
        //=======================================
        //Alternate Constructor
        //======================================
        public TimecardDetail(string taskName) {
            dailyHours = new decimal[7];
            for (int i = 0; i < dailyHours.Length; i++) {
                dailyHours[i] = 0;
            }
            DetailId = 0;
            TaskId = 0;
            TimecardId = 0;
            Task_Name = taskName;
        }
        // ==============================
        // Override To-String Function to return Task_Name instead of .NET default which is class name
        // ==============================
        public override String ToString() {
            return Task_Name;
        }
        //=================================
        // Return true if this is a blank timecarddetail
        //=================================
        public bool IsBlank {
            get {
                for (int i = 0; i < dailyHours.Length; i++) {
                    if (dailyHours[i] > 0) {
                        return false;
                    }
                }
                return true;
            }
        }
        //===============================
        // Return a string suitable for UI display purposes
        // ==============================
        public string GetDisplayValueForDay(Timecard.DetailFields field) {
            if (dailyHours[(int)field] == 0) {
                return string.Empty;
            }
            return dailyHours[(int)field].ToString("F1", CultureInfo.InvariantCulture);
        }

        //===============================
        // Update the given daily hours with a string that came from the UI
        //===============================
        public void PutDisplayValueForDay(Timecard.DetailFields field, string valu) {
            decimal min = Decimal.Parse("0.0");
            decimal max = Decimal.Parse("99.9");
            decimal forced;
            if (Decimal.TryParse(valu, out forced)) {
                if (forced >= max) {
                    forced = max;
                }
                if (forced <= min) {
                    forced = min;
                }
                dailyHours[(int)field] = forced;
                return;
            }
            dailyHours[(int)field] = min;
        }
        //============================
        //return a decimal value typically for use in writing to DB
        //============================
        public decimal GetValueForDay(Timecard.DetailFields field) {
            return dailyHours[(int)field];
        }
        //=============================
        // Update the given daily hours with decimal value
        // ============================
        public void PutValueForDay(Timecard.DetailFields field, decimal hrs) {
            decimal max = Decimal.Parse("99.9");
            if (hrs > max) {
                dailyHours[(int)field] = max;
            }
            if (hrs < 0) {
                dailyHours[(int)field] = 0;
            }
            dailyHours[(int)field] = hrs;
        }
        //===============================
        // Provide public read/write properties for binding to display grid
        //===============================
        public string Monday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Monday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Monday_Hrs, value); }
        }
        public string Tuesday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Tuesday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Tuesday_Hrs, value); }
        }
        public string Wednesday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Wednesday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Wednesday_Hrs, value); }
        }
        public string Thursday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Thursday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Thursday_Hrs, value); }
        }
        public string Friday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Friday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Friday_Hrs, value); }
        }
        public string Saturday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Saturday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Saturday_Hrs, value); }
        }
        public string Sunday_Hrs {
            get { return GetDisplayValueForDay(Timecard.DetailFields.Sunday_Hrs); }
            set { PutDisplayValueForDay(Timecard.DetailFields.Sunday_Hrs, value); }
        }
    }
}
