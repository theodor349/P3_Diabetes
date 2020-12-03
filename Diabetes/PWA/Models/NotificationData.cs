using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public enum NotificationType { Message, Warning }
    public enum ThresholdType { Low, High }
    public class NotificationData
    {
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string IconClassName { get; set; }
        public float Threshold { get; set; }
        public ThresholdType ThresholdType { get; set; }
    }
}
