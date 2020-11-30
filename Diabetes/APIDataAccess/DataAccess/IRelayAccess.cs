﻿using APIDataAccess.Models.Relay;
using System;

namespace APIDataAccess.DataAccess
{
    public interface IRelayAccess
    {
        float GetBatteryStatus(string link);
        float GetBloodGlucose(string link);
        DateTime GetLastReceived(string link);
        string GetInsulinStatus(string link);
        StatusArrow.ArrowDirection GetStatus(string NSLink);
    }
}