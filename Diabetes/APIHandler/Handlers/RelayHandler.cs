using APIDataAccess.DataAccess;
using APIDataAccess.Models.Relay;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Handlers
{
    public class RelayHandler : IRelayHandler
    {
        RelayAccess relayAccess = new RelayAccess();

        public float GetBatteryStatus(string link)
        {
            return relayAccess.GetBatteryStatus(link);
        }
        public float GetBloodGlucose(string link)
        {
            return relayAccess.GetBloodGlucose(link);
        }
        public DateTime GetLastReceived(string link)
        {
            return relayAccess.GetLastReceived(link);
        }
        public PumpDataModel.ArrowDirection GetStatus(string link)
        {
            return  relayAccess.GetStatus(link);
        }
        public int GetInsulinStatus(string link, float maxReservoir)
        {
            return relayAccess.GetInsulinStatus(link, maxReservoir);
        }
    }
}
