using APIDataAccess.Internal.DataAccess;
using APIDataAccess.Models.Permission;
using System;
using System.Collections.Generic;
using APIDataAccess.DataAccess;
using System.Globalization;

namespace APIHandler.Handlers
{
    public class PermissionHandler : IPermissionHandler
    {
        private readonly IPermissionAccess _pa;

        public PermissionHandler(IPermissionAccess permissionAccess)
        {
            _pa = permissionAccess;
        }

        public PermissionDBModel Get(int id)
        {
            return _pa.Get(id);
        }

        public List<PermissionDBModel> GetByTargetId(string targetId)
        {
            return _pa.GetByTargetId(targetId);
        }

        public List<PermissionDBModel> GetByWatcherId(string watcherId)
        {
            return _pa.GetByWatcherId(watcherId);
        }

        public List<RequestPermissionDBModel> GetPendingPermissions(string userId)
        {
            return _pa.GetPendingPermissions(userId);
        }

        public Dictionary<string, int> GetPermissionAttributes(PermissionDBModel[] permissions)
        {
            Dictionary<string, int> flagDictionary = new Dictionary<string, int>();

            foreach (PermissionDBModel permission in permissions)
            {
                if (IsActive(permission, DateTime.Now))
                {
                    if (flagDictionary.ContainsKey(permission.TargetID))
                    {
                        flagDictionary[permission.TargetID] = flagDictionary[permission.TargetID] | permission.Attributes;
                    }
                    else
                    {
                        flagDictionary.Add(permission.TargetID, permission.Attributes);
                    }
                }
            }

            return flagDictionary;
        }

        public bool IsActive(PermissionDBModel permission, DateTime currDate)
        {
            if (!permission.Accepted)
                return false;
            // Time frame
            if (!WithinStartExpireDate(permission.StartDate, permission.ExpireDate, currDate))
                return false;
            if (permission.WeeksActive == 0)
                return true;
            // Recurrence
            if (!WithinActiveInterval(permission.StartDate, permission.ExpireDate, currDate, permission.WeeksActive, permission.WeeksDeactive))
                return false;
            if (!WithinActiveDay(permission.Days, currDate))
                return false;

            return true;
        }

        private bool WithinActiveDay(int days, DateTime now)
        {
            int day = GetDayOfWeek(now);
            int bitDay = day == 0 ? 1 : (int)Math.Pow(2, day);
            int res = (days & bitDay);
            return bitDay == res;
        }

        private int GetDayOfWeek(DateTime now)
        {
            int day = (int)now.DayOfWeek - 1;
            if (day == -1)
                day = 6;
            return day;
        }

        private bool WithinActiveInterval(DateTime startDate, DateTime expireDate, DateTime currDate, int weeksActive, int weeksDeactive)
        {
            int startWeek = GetIso8601WeekOfYear(startDate);
            int expireWeek = GetIso8601WeekOfYear(expireDate);
            int currWeek = GetIso8601WeekOfYear(currDate);

            int intervalStart = startWeek;
            int intervalEnd = startWeek + weeksActive;
            int nextInterval = weeksActive + weeksDeactive;
            while (intervalStart <= expireWeek)
            {
                if (currWeek >= intervalStart && currWeek < intervalEnd)
                    return true;
                intervalStart += nextInterval;
                intervalEnd += nextInterval;
            }

            return false;
        }

        // https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
        private int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private bool WithinStartExpireDate(DateTime startDate, DateTime expireDate, DateTime currDate)
        {
            long startTime = startDate.Ticks;
            long endTime = expireDate.Ticks;
            long now = currDate.Ticks;
            return (now >= startTime) && (now < endTime);
        }

        public int RequestPermission(RequestPermissionDBModel request)
        {
            return _pa.Create(request);
        }

        public int Update(UpdatePermissionModel updatedPermission)
        {
            return _pa.Update(updatedPermission);
        }

        public int Delete(int id)
        {
            return _pa.Delete(id);
        }

        public int DeleteByUserId(string userId)
        {
            return _pa.DeleteByUserId(userId);
        }

        public int AcceptPermissionRequest(int id)
        {
            return _pa.AcceptPermissionRequest(id);
        }
    }
}
