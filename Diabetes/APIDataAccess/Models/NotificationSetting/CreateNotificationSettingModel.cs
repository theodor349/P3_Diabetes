using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.NotificationSetting
{
    public class CreateNotificationSettingModel
    {
        public CreateNotificationSettingModel()
        {

        }
        public CreateNotificationSettingModel(string OwnerID, float ThresHold, ThresholdType thresholdType, NotificationType notificationType, string Note)
        {
            this.OwnerId = OwnerID;
            this.Threshold = ThresHold;
            ThresholdType = thresholdType;
            NotificationType = notificationType;
            this.Note = Note;
        }

        public string OwnerId { get; set; }
        public float Threshold { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Note { get; set; }
    }
}
