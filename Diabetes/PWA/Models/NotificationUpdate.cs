namespace PWA.Models
{
    public class NotificationUpdate {
        public int ID { get; set; }
        public float Threshold { get; set; }
        public NotificationType Type { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public string Note { get; set; }
    }
}
