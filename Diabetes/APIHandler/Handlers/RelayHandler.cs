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
            return relayAccess.GetStatus(link);
        }
        public int GetInsulinStatus(string link, float maxReservoir)
        {
            return relayAccess.GetInsulinStatus(link, maxReservoir);
        }
        public bool ConnectionOk(string NSLink)
        {
            return relayAccess.ConnectionOk(NSLink);
        }

        public PumpDataModel GetAttributeData(int attribute, string NSLink, float maxReservoir) {
            AttributeFlags attributeFlags = (AttributeFlags)attribute;
            PumpDataModel pumpDataModel = new PumpDataModel();

            if (attributeFlags.HasFlag(AttributeFlags.BloodGlucose)) {
                pumpDataModel.BloodGlucose = GetBloodGlucose(NSLink);
                pumpDataModel.Status = GetStatus(NSLink);
            }

            if (attributeFlags.HasFlag(AttributeFlags.Battery)) {
                pumpDataModel.BatteryStatus = GetBatteryStatus(NSLink);
            }

            if (attributeFlags.HasFlag(AttributeFlags.Insulin)) {
                pumpDataModel.InsulinStatus = GetInsulinStatus(NSLink, maxReservoir);
            }

            pumpDataModel.LastReceived = GetLastReceived(NSLink);

            return pumpDataModel;
        }

        [Flags]
        public enum AttributeFlags {
            BloodGlucose = 1,
            Insulin = 2,
            Battery = 4,
        }
    }
}
