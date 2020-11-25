using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public class PumpData
    {
        public float BloodGlucose { get; set; }
        public float BatteryStatus { get; set; }
        public float Delta { get; set; }
        public float InsulinStatus { get; set; }
        public int Minutes { get; set; }
    }
}
