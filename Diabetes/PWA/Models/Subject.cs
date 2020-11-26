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
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NSLink { get; set; }
        public bool IsEUMeasure { get; set; }
        public PumpData PumpData { get; set; }
        public List<NotificationData> NotificationDatas { get; set; }
    }
}
