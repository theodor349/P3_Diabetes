namespace APIDataAccess.Models.NotificationSetting
{
    public enum ThresholdType { Low, High }
    public enum NotificationType { Message, Warning }
    public class NotificationSettingModel
    {
        public int ID { get; set; }
        public string OwnerID { get; set; }
        public float Threshold { get; set; }
        public ThresholdType ThresholdType { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Note { get; set; }
    }
}
