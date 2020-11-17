using System;

namespace APIDataAccess.DataAccess
{
    public interface IRelayAccess
    {
        float GetBatteryStatus(string link);
        float GetBloodGlucose(string link);
        DateTime GetConnectionStatus(string link);
        float GetInsulinStatus(string link);
        bool IsConnectionOK(string link);
    }
}