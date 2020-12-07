CREATE PROCEDURE [dbo].[spNotificationSetting_DeleteByUserId]
	@Id nvarchar(450)
AS
BEGIN
	DELETE
	FROM NotificationSetting
	WHERE [Owner] = @Id;
END
