using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDataAccess.Models.NotificationSetting
{
    public class UpdateNotificationSettingModel
    {
        public UpdateNotificationSettingModel()
        {

        }

        public UpdateNotificationSettingModel(string note)
        {
            Note = note;
        }

        public int Id { get; set; }
        public int Threshold { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Note { get; set; }
    }
}
