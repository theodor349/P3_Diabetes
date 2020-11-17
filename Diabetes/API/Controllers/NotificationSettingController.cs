using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDataAccess.Models.NotificationSetting;
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
        [HttpGet]
        public List<NotificationSettingModel> Get(string userId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ActionResult Update(UpdateNotificationSettingModel notificationSetting)
        {
            throw new NotImplementedException();
        }
    }
}
