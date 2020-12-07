CREATE PROCEDURE [dbo].[spNotificationSetting_GetByUser]
	@Id nvarchar(450)
AS
BEGIN
	SELECT *
	FROM NotificationSetting
	WHERE [Owner] = @Id;
END