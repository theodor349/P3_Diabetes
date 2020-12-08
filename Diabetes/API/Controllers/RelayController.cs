using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataAccess.Models.Relay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIHandler.Handlers;
using APIDataAccess.Models.Permission;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RelayController : ControllerBase
    {
        private readonly IRelayHandler relayHandler;
        private readonly IPermissionHandler permissionHandler;
        private readonly IAccountHandler accountHandler;

        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public RelayController(IRelayHandler relayHandler, IPermissionHandler permissionHandler, IAccountHandler accountHandler)
        {
            this.relayHandler = relayHandler;
            this.permissionHandler = permissionHandler;
            this.accountHandler = accountHandler;
        }

        public List<PumpDataModel> GetNightscoutData() {
            List<PumpDataModel> results = new List<PumpDataModel>();
            List<PermissionDBModel> permissions = permissionHandler.GetByWatcherId(UserId);
            Dictionary<string, int> permissionAttributes = permissionHandler.GetPermissionAttributes(permissions);

            foreach(KeyValuePair<string, int> entry in permissionAttributes) {

                string NSLink = accountHandler.GetNightscoutLink(entry.Key);
                float maxReservoir = accountHandler.GetMaxReservoir(entry.Key);
                results.Add(relayHandler.GetAttributeData(entry.Value, NSLink, maxReservoir));
            }
            return results;
        }
    }
}
