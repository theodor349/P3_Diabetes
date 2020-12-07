CREATE PROCEDURE [dbo].[spPermission_AcceptPermissionRequest]
	@Id int,
	@Accepted bit
AS
BEGIN
	UPDATE Permission
	SET [Accepted] = 1
	WHERE [Id] = @Id; 	
END