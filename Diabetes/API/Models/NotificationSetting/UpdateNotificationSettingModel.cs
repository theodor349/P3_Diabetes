using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.NotificationSetting
{
    public class UpdateNotificationSettingModel
    {
        public int ID { get; set; }
        public int ThresHold { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Note { get; set; }
    }
}
