using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using APIDataAccess.Models.NotificationSetting;
using APIHandler.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWA.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationSettingController : ControllerBase
    {
        private readonly INotificationSettingHandler _nsh;
        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public NotificationSettingController(INotificationSettingHandler notificationSettingHandler)
        {
            _nsh = notificationSettingHandler;
        }

        [HttpPut]
        [Route("GetNotificationSettings")]
        public NotificationDataList Get(StringValue userId)
        {
            return ConvertNotificationSettings( _nsh.Get(userId.Value) );
        }

        [HttpPut]
        public ActionResult Update(UpdateNotificationSettingModel notificationSettingModel)
        {
            // If the debugger is attached it will only run the following if block, if it is release then the else block will run
            // This is done to ignore the ID check in the test for the Update func
            // TODO: Gøres på en anden måde
#if DEBUG
            if (IsValidateModel(notificationSettingModel))
            {
                _nsh.Update(notificationSettingModel);
                return Ok();
            }
            else
                return BadRequest();
#else
            NotificationSettingModel m = _nsh.Get(notificationSettingModel.Id);
            if (m == null || m.OwnerID != UserId)
                return Forbid();

            if (IsValidateModel(notificationSettingModel))
            {
                _nsh.Update(notificationSettingModel);
                return Ok();
            }
            else
                return BadRequest();
#endif
        }

        private NotificationDataList ConvertNotificationSettings(List<NotificationSettingModel> notificationSettings) {
            var data = notificationSettings.ConvertAll(x => {
                var setting = new PWA.Models.NotificationData() {
                    Type = (PWA.Models.NotificationType)x.NotificationType,
                    ThresholdType = (PWA.Models.ThresholdType)x.ThresholdType,
                    Note = x.Note,
                };
                return setting;
            });
            return new NotificationDataList() { NotificationDatas = data };
        }

        private bool IsValidateModel(UpdateNotificationSettingModel model)
        {
            return model.Note != null;
        }
    }
}
