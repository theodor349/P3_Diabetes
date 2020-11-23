CREATE PROCEDURE [dbo].[spPermission_GetByTargetId]
	@id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [TargetId] = @id;
END
