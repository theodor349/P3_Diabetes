using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public enum NotificationType { Message, Warning }
    public class NotificationData
    {
        public NotificationType Type { get; set; }
        public string Mesage { get; set; }
    }
}
