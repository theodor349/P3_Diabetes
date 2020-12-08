CREATE PROCEDURE [dbo].[spPermission_Get]
	@Id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [Id] = @Id;
END