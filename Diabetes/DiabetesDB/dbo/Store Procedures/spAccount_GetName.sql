CREATE PROCEDURE [dbo].[spAccount_GetName]
	@Id nvarchar(450)
AS
BEGIN
	SELECT [FirstName], [LastName]
	FROM Account
	WHERE [Id] = @Id;
END