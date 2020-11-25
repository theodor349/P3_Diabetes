using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.Models.Permission
{
    public class UpdatePermissionDBModel
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
        public long startTime { get; set; }
        public long endTime { get; set; }
    }
}
