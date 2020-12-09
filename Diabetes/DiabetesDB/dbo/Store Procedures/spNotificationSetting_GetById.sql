CREATE PROCEDURE [dbo].[spNotificationSetting_GetById]
	@Id int
AS
BEGIN
	SELECT *
	FROM NotificationSetting
	WHERE Id = @Id;
END
