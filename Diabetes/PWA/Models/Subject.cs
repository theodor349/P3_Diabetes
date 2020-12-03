using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public class Subject
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PumpData PumpData { get; set; }
        public List<NotificationData> NotificationDatas { get; set; }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }
    }
}
