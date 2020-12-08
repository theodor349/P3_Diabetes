CREATE PROCEDURE [dbo].[spPermission_GetPendingPermissions]
	@Id nvarchar(450)
AS
BEGIN 
	SELECT * 
	FROM Permission
	WHERE (WatcherID = @Id OR TargetID = @Id) AND Accepted = 0;
END