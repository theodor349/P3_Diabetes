CREATE PROCEDURE [dbo].[spPermission_AcceptPermissionRequest]
	@Id int,
	@Accepted bit
AS
BEGIN
	UPDATE Permission
	SET [Accepted] = @Accepted
	WHERE [Id] = @Id; 	
END