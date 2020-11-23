CREATE PROCEDURE [dbo].[spPermission_GetByUserId]
	@id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [Id] = @id;
END
