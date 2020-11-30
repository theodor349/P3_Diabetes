using APIDataAccess.Models.Relay;
using System;

namespace APIDataAccess.DataAccess
{
    public interface IRelayAccess
    {
        float GetBatteryStatus(string link);
        float GetBloodGlucose(string link);
        DateTime GetLastReceived(string link);
        StatusArrow.ArrowDirection GetStatus(string NSLink);
        int GetInsulinStatus(string NSLink, float maxReservoir);
    }
}