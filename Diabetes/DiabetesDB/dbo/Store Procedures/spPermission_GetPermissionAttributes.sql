CREATE PROCEDURE [dbo].[spPermission_GetPermissionAttributes]
	@id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [WatcherId] = @id;
END
