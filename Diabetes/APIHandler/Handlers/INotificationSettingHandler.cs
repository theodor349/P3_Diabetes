﻿using APIDataAccess.Models.NotificationSetting;
using System.Collections.Generic;

namespace APIHandler.Handlers
{
    public interface INotificationSettingHandler
    {
        void CreateStandartSettings(string userId);
        void DeleteByUserId(string userId);
        List<NotificationSettingModel> Get(string userId);
        void Update(UpdateNotificationSettingModel model);
    }
}