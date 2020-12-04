using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataAccess.Models.Relay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIHandler.Handlers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RelayController : ControllerBase
    {
        private readonly IRelayHandler relayHandler;

        public RelayController(IRelayHandler relayHandler)
        {
            this.relayHandler = relayHandler;
        }

        [HttpGet]
        public PumpDataModel GetAttributeData(int attribute, string NSLink, float maxReservoir)
        {
            PumpDataModel.AttributeFlags attributeFlags = (PumpDataModel.AttributeFlags)attribute;
            PumpDataModel pumpDataModel = new PumpDataModel();

            if (attributeFlags.HasFlag(PumpDataModel.AttributeFlags.BloodGlucose))
            {
                pumpDataModel.BloodGlucose = relayHandler.GetBloodGlucose(NSLink);
                pumpDataModel.Status = relayHandler.GetStatus(NSLink);
            }

            if (attributeFlags.HasFlag(PumpDataModel.AttributeFlags.Battery))
            {
                pumpDataModel.BatteryStatus = relayHandler.GetBatteryStatus(NSLink);
            }

            if (attributeFlags.HasFlag(PumpDataModel.AttributeFlags.Insulin))
            {
                pumpDataModel.InsulinStatus = relayHandler.GetInsulinStatus(NSLink, maxReservoir);
            }

            pumpDataModel.LastReceived = relayHandler.GetLastReceived(NSLink);

            return pumpDataModel;
        }
    }
}
