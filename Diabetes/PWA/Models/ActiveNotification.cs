using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Models
{
    public enum NotificationButtonType { Snooze, Dismiss}
    public class ActiveNotification
    {
        public ActiveNotification()
        {
            Active = true;
        }

        public Subject Subject { get; set; }
        public NotificationData Data { get; set; }
        public bool Active { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return Subject.ID + "-" + Id;
        }
    }
}
