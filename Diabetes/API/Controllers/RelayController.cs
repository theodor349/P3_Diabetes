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
using API.Models;

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
        private readonly INotificationSettingHandler _notificationSettingHandler;

        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public RelayController(IRelayHandler relayHandler, IPermissionHandler permissionHandler, IAccountHandler accountHandler, INotificationSettingHandler notificationSettingHandler)
        {
            this.relayHandler = relayHandler;
            this.permissionHandler = permissionHandler;
            this.accountHandler = accountHandler;
            _notificationSettingHandler = notificationSettingHandler;
        }

        [HttpGet]
        public SubjectList GetNightscoutData() {
            List<FrontEndSubject> results = new List<FrontEndSubject>();
            List<PermissionDBModel> permissions = permissionHandler.GetByWatcherId(UserId);
            Dictionary<string, int> permissionAttributes = permissionHandler.GetPermissionAttributes(permissions);

            foreach(KeyValuePair<string, int> entry in permissionAttributes) {

                string NSLink = accountHandler.GetNightscoutLink(entry.Key);
                if (!string.IsNullOrWhiteSpace(NSLink))
                {
                    float maxReservoir = accountHandler.GetMaxReservoir(entry.Key);
                    var pumpData = relayHandler.GetAttributeData(entry.Value, NSLink, maxReservoir);
                    var name = accountHandler.GetName(entry.Key);
                    var notificationSettings = new List<NotificationData>();
                    foreach (var n in _notificationSettingHandler.Get(entry.Key))
                    {
                        notificationSettings.Add(new NotificationData()
                        {
                            Note = n.Note,
                            Threshold = n.Threshold,
                            ThresholdType = (ThresholdType)n.ThresholdType,
                            Title = n.ThresholdType == APIDataAccess.Models.NotificationSetting.ThresholdType.High ? "High blood sugar" : "Low blood sugar",
                            Type = (NotificationType)n.NotificationType,
                            IconClassName = "",
                        });
                    }
                    results.Add(new FrontEndSubject()
                    {
                        ID = entry.Key,
                        FirstName = name.FirstName,
                        LastName = name.LastName,
                        PumpData = pumpData,
                        NotificationDatas = notificationSettings
                    }); ;
                }
            }
            return new SubjectList() { Subjects = results };
        }
    }
}
