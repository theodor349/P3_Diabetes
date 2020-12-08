using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIDataAccess.Models.NotificationSetting;
using APIHandler.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public List<NotificationSettingModel> Get(string userId)
        {
            return _nsh.Get(userId);
        }

        [HttpPut]
        public ActionResult Update(UpdateNotificationSettingModel notificationSettingModel)
        {
            if (IsValidateModel(notificationSettingModel))
            {
                _nsh.Update(notificationSettingModel);
                return Ok();
            }
            else
                return BadRequest();
            
        }

        private bool IsValidateModel(UpdateNotificationSettingModel model)
        {
            return model.Note != null;
        }
    }
}
