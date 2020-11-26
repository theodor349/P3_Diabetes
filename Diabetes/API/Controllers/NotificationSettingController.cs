using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly INotificationSettingHandler notificationSettingHandler;

        public NotificationSettingController(INotificationSettingHandler notificationSettingHandler)
        {
            this.notificationSettingHandler = notificationSettingHandler;
        }

        [HttpGet]
        public List<NotificationSettingModel> Get(string userId)
        {
            return notificationSettingHandler.Get(userId);
        }

        [HttpPut]
        public ActionResult Update(UpdateNotificationSettingModel notificationSettingModel)
        {
            bool vaild = ValidateModel(notificationSettingModel);
            if (vaild)
            {
                notificationSettingHandler.Update(notificationSettingModel);
                return Ok();
            }
            else
                return BadRequest();
            
        }

        private bool ValidateModel(UpdateNotificationSettingModel updateNotificationSettingModel)
        {
            if (updateNotificationSettingModel.Note == null)
                return false;
            else
                return true;
        }
    }
}
