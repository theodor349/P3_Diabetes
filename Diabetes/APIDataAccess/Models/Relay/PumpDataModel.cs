using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.Relay
{
    public class PumpDataModel
    {
        public float BloodGlucose { get; set; }
        public float InsulinStatus { get; set; }
        public float BatteryStatus { get; set; }
        public DateTime NSConnectionStatus { get; set; }
    }
}
