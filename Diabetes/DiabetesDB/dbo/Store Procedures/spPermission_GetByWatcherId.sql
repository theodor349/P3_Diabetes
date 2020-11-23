CREATE PROCEDURE [dbo].[spPermission_GetByWatcherId]
	@id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [WatcherId] = @id;
END
