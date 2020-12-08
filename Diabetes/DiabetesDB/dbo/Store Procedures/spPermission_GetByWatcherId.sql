CREATE PROCEDURE [dbo].[spPermission_GetByWatcherId]
	@Id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [WatcherId] = @Id;
END
