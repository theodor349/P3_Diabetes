CREATE PROCEDURE [dbo].[spNotificationSetting_Update]
	@Id int,
	@Threshold float,
	@ThresholdType int,
	@NotificationType int,
	@Note nvarchar(450)
AS
BEGIN
	UPDATE NotificationSetting
	SET [Threshold] = @Threshold, [ThresholdType] = @ThresholdType, [NotificationType] = @NotificationType, [Note] = @Note
	WHERE Id = @Id;
END