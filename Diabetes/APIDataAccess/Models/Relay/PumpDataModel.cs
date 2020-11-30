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
        public DateTime LastReceived { get; set; }
        public ArrowDirection Status { get; set; }
        public AttributeFlags Flags { get; set; }

        public enum ArrowDirection
        {
            SingleUp,
            DoubleUp,
            SingleDown,
            DoubleDown,
            Flat,
            FortyFiveUp,
            FortyFiveDown,
            Null,
        }

        [Flags]
        public enum AttributeFlags
        {
            BloodGlucose = 1,
            Insulin = 2,
            Battery = 4,
        }
    }

    
}
