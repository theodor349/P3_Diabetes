namespace PWA.Models
{
    public enum ThresholdType { Low, High }
    public enum NotificationType { Message, Warning }
    public class NotificationData {
        public int ID { get; set; }
        public string OwnerID { get; set; }
        public float Threshold { get; set; }
        public NotificationType Type { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string IconClassName { get; set; }
    }
}
