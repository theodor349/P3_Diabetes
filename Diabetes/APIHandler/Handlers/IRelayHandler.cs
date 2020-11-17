using APIDataAccess.Models.Relay;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface IRelayHandler
    {
        List<PumpDataPairModel> Get(string userId);
        bool IsConnectionOK(string link);
    }
}