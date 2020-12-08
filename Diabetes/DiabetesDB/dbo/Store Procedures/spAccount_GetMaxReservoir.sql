CREATE PROCEDURE [dbo].[spAccount_GetMaxReservoir]
	@Id nvarchar(450)
AS
BEGIN
	SELECT [MaxReservoir]
	FROM Account
	WHERE [Id] = @Id;
END
