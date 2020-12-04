using APIDataAccess.Models.Relay;
using System;

namespace APIHandler.Handlers
{
    public interface IRelayHandler
    {
        float GetBatteryStatus(string link);
        float GetBloodGlucose(string link);
        int GetInsulinStatus(string NSLink, float maxReservoir);
        DateTime GetLastReceived(string link);
        PumpDataModel.ArrowDirection GetStatus(string NSLink);
        bool ConnectionOk(string NSLink);
    }
}