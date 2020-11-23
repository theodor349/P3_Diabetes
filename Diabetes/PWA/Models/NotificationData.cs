using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public enum NotificationType { Message, Warning }
    public enum NotificationButtonType { Snooze, Dismiss}
    public class NotificationData
    {
        public string Name { get; set; }
        public NotificationType Type { get; set; }
        public string Mesage { get; set; }
    }
}
