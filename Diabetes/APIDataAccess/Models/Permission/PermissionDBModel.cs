using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace APIDataAccess.Models.Permission
{
    public class PermissionDBModel
    {
        public int Id { get; set; }
        public string WatcherID { get; set; }
        public string TargetID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Days { get; set; }
        public int WeeksActive { get; set; }
        public int WeeksDeactive { get; set; }
        public int Attributes { get; set; }
        public bool Accepted { get; set; }

        public bool IsActive(DateTime currDate) {
            return Accepted && WithinStartExpireDate(StartDate, ExpireDate, currDate) && IsIntervalActive(currDate);
        }

        #region Interval Functions  

        bool IsIntervalActive(DateTime currDate) {
            if (WeeksActive == 0) {
                return true;
            } else {
                if (WithinActiveInterval(StartDate, ExpireDate, currDate, WeeksActive, WeeksDeactive) &&
                    WithinActiveDay(Days, currDate)) {
                    return true;
                }
            }
            return false;
        }

        private bool WithinActiveDay(int days, DateTime now) {
            int day = GetDayOfWeek(now);
            int bitDay = day == 0 ? 1 : (int)Math.Pow(2, day);
            int res = (days & bitDay);
            return bitDay == res;
        }

        private int GetDayOfWeek(DateTime now) {
            int day = (int)now.DayOfWeek - 1;
            if (day == -1)
                day = 6;
            return day;
        }

        private bool WithinActiveInterval(DateTime startDate, DateTime expireDate, DateTime currDate, int weeksActive, int weeksDeactive) {
            int startWeek = GetIso8601WeekOfYear(startDate);
            int expireWeek = GetIso8601WeekOfYear(expireDate);
            int currWeek = GetIso8601WeekOfYear(currDate);

            int intervalStart = startWeek;
            int intervalEnd = startWeek + weeksActive;
            int nextInterval = weeksActive + weeksDeactive;
            while (intervalStart <= expireWeek) {
                if (currWeek >= intervalStart && currWeek < intervalEnd)
                    return true;
                intervalStart += nextInterval;
                intervalEnd += nextInterval;
            }

            return false;
        }

        // https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
        private int GetIso8601WeekOfYear(DateTime time) {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private bool WithinStartExpireDate(DateTime startDate, DateTime expireDate, DateTime currDate) {
            long startTime = startDate.Ticks;
            long endTime = expireDate.Ticks;
            long now = currDate.Ticks;
            return (now >= startTime) && (now < endTime);
        }

        #endregion
    }
}
