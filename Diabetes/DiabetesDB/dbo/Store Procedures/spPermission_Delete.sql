CREATE PROCEDURE [dbo].[spPermission_Delete]
	@Id nvarchar(450)
AS
BEGIN
	DELETE
	FROM Permission
	WHERE [Id] = @Id;
END