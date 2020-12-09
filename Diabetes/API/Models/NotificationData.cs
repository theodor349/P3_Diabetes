namespace API.Models
{
    public enum FrontendNotificationType { Message, Warning }
    public enum FrontendThresholdType { Low, High }
    public class NotificationData
    {
        public FrontendNotificationType Type { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string IconClassName { get; set; }
        public float Threshold { get; set; }
        public FrontendThresholdType ThresholdType { get; set; }
    }
}