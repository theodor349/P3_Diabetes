CREATE PROCEDURE [dbo].[spPermission_AcceptPermissionRequest]
	@Id int
AS
BEGIN
	UPDATE Permission
	SET [Accepted] = 1
	WHERE [Id] = @Id; 	
END