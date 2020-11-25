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
            this.OwnerID = OwnerID;
            this.ThresHold = ThresHold;
            ThresholdType = thresholdType;
            NotificationType = notificationType;
            this.Note = Note;
        }

        public string OwnerID { get; set; }
        public float ThresHold { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Note { get; set; }
    }
}
