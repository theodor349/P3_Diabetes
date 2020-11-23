CREATE PROCEDURE [dbo].[spPermission_Delete]
	@id nvarchar(450)
AS
BEGIN
	DELETE
	FROM Permission
	WHERE [Id] = @id;
END