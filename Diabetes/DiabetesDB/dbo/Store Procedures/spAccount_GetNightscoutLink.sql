CREATE PROCEDURE [dbo].[spAccount_GetNightscoutLink]
	@Id nvarchar(450)
AS
BEGIN
	SELECT [NSLink]
	FROM Account
	WHERE [Id] = @Id;
END
