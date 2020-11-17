using APIDataAccess.Models.Relay;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Handlers
{
    public class RelayHandler : IRelayHandler
    {
        public List<PumpDataPairModel> Get(string userId)
        {
            throw new NotImplementedException();
        }

        public bool IsConnectionOK(string link)
        {
            throw new NotImplementedException();
        }
    }
}
