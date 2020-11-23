CREATE PROCEDURE [dbo].[spPermission_DeleteByUserId]
	@id nvarchar(450)
AS
BEGIN
	DELETE
	FROM Permission
	WHERE [WatcherId] = @id OR [TargetId] = @id;
END