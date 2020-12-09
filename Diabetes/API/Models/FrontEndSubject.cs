using APIDataAccess.Models.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FrontEndSubject
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PumpDataModel PumpData { get; set; }
        public List<NotificationData> NotificationDatas { get; set; }
    }
}
