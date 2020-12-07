CREATE PROCEDURE [dbo].[spAccount_UpdateNightScoutLink]
	@Id nvarchar(450),
	@NewLink nvarchar(450)
AS
BEGIN 
	UPDATE Account
	SET [NSLink] = @NewLink
	WHERE [Id] = @Id;
END
