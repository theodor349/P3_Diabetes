CREATE PROCEDURE [dbo].[spPermission_DeleteByUserId]
	@Id nvarchar(450)
AS
BEGIN
	DELETE
	FROM Permission
	WHERE WatcherId = @Id OR [TargetId] = @Id;
END