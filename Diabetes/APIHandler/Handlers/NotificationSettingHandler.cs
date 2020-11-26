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
        private readonly INotificationSettingAccess notificationSettingAccess;
        private readonly INotificationsSettingsUtils notificationsSettingsUtils;

        public NotificationSettingHandler(INotificationSettingAccess notificationSettingAccess, INotificationsSettingsUtils notificationsSettingsUtils)
        {
            this.notificationSettingAccess = notificationSettingAccess;
            this.notificationsSettingsUtils = notificationsSettingsUtils;
        }

        private float ConvertThresholdValue(string threshold)
        {
            return float.Parse(threshold);
        }

        private ThresholdType ConvertThresholdType(string thresholdType)
        {
            if (thresholdType == "High")
                return ThresholdType.High;
            else
                return ThresholdType.Low; 
        }

        private NotificationType ConvertNotificationType(string notificationType)
        {
            if (notificationType == "Warning")
                return NotificationType.Warning;
            else
                return NotificationType.Message;
        }

        private CreateNotificationSettingModel CreateDefault(string ownerID, string type)
        {
            float threshold = ConvertThresholdValue(notificationsSettingsUtils.GetDefaultValue(type, "Threshold"));
            ThresholdType thresholdType = ConvertThresholdType(notificationsSettingsUtils.GetDefaultValue(type, "Thresholdtype"));
            NotificationType notificationType = ConvertNotificationType(notificationsSettingsUtils.GetDefaultValue(type, "NotificationType"));
            string note = notificationsSettingsUtils.GetDefaultValue(type, "Note");

            return new CreateNotificationSettingModel(ownerID, threshold, thresholdType, notificationType, note);
        }

        public List<NotificationSettingModel> Get(string userId)
        {
            return notificationSettingAccess.Get(userId);
        }

        public void Update(UpdateNotificationSettingModel model)
        {
            notificationSettingAccess.Update(model);
        }

        public void DeleteByUserId(string userId)
        {
            notificationSettingAccess.DeleteByUserId(userId);
        }

        public void CreateStandardSettings(string userId)
        {
            CreateNotificationSettingModel model1 = CreateDefault(userId, "High");

            CreateNotificationSettingModel model2 = CreateDefault(userId, "Low");

            notificationSettingAccess.Create(model1);
            notificationSettingAccess.Create(model2);
        }
    }
}
