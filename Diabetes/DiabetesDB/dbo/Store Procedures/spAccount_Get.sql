CREATE PROCEDURE [dbo].[spAccount_Get]
	@id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Account
	WHERE [Id] = @id;
END