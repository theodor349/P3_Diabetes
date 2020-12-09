using APIDataAccess.DataAccess;
using APIDataAccess.Models.NotificationSetting;
using APIHandler.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler.Handlers
{
    public class NotificationSettingHandler : INotificationSettingHandler
    {
        private readonly INotificationSettingAccess _nsa;
        private readonly INotificationsSettingsUtils _nsu;

        public NotificationSettingHandler(INotificationSettingAccess notificationSettingAccess, INotificationsSettingsUtils notificationsSettingsUtils)
        {
            _nsa = notificationSettingAccess;
            _nsu = notificationsSettingsUtils;
        }

        private CreateNotificationSettingModel CreateDefault(string ownerID, string type)
        {
            float threshold = float.Parse(_nsu.GetDefaultValue(type, "Threshold"));
            ThresholdType thresholdType = StringToEnum<ThresholdType>(_nsu.GetDefaultValue(type, "ThresholdType"));
            NotificationType notificationType = StringToEnum<NotificationType>(_nsu.GetDefaultValue(type, "NotificationType"));
            string note = _nsu.GetDefaultValue(type, "Note");

            return new CreateNotificationSettingModel(ownerID, threshold, thresholdType, notificationType, note);
        }

        public List<NotificationSettingModel> Get(string userId)
        {
            return _nsa.Get(userId);
        }

        public void Update(UpdateNotificationSettingModel model)
        {
            _nsa.Update(model);
        }

        public void DeleteByUserId(string userId)
        {
            _nsa.DeleteByUserId(userId);
        }

        public void CreateStandardSettings(string userId)
        {
            CreateNotificationSettingModel model1 = CreateDefault(userId, "High");

            CreateNotificationSettingModel model2 = CreateDefault(userId, "Low");

            _nsa.Create(model1);
            _nsa.Create(model2);
        }

        private T StringToEnum<T>(string enumName) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), enumName);
        }

        public NotificationSettingModel Get(int id)
        {
            return _nsa.Get(id);
        }
    }
}
