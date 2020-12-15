using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.Relay
{
    public class PumpDataModel
    {
        public float BloodGlucose { get; set; } = -1f;
        public float InsulinStatus { get; set; } = -1f;
        public float BatteryStatus { get; set; } = -1f;
        public DateTime LastReceived { get; set; }
        public ArrowDirection Status { get; set; }

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
    }
}
