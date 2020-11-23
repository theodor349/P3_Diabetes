using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.Permission
{
    public class RequestPermissionDBModel
    {
        public string WatcherID { get; set; }
        public string TargetID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Days { get; set; }
        public int WeeksActive { get; set; }
        public int WeeksDeactive { get; set; }
        public int Attributes { get; set; }
    }
}
