CREATE PROCEDURE [dbo].[spNotificationSetting_Create]
	@OwnerId nvarchar(450),
	@Threshold float,
	@ThresholdType int,
	@NotificationType int,
	@Note nvarchar(450)
AS
BEGIN
	INSERT 
	INTO NotificationSetting ([Owner], [Threshold], [ThresholdType], [NotificationType], [Note])
	VALUES (@OwnerId, @Threshold, @ThresholdType, @NotificationType, @Note);
END