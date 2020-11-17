using System;
using System.Collections.Generic;
using System.Text;

namespace APIDataAccess.Models.Relay
{
    public class PumpDataPairModel
    {
        public string UserId { get; set; }
        public PumpDataModel Data { get; set; }
    }
}
