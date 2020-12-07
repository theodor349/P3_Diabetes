CREATE PROCEDURE [dbo].[spAccount_UpdateAccount]
	@Id nvarchar(450),
	@FirstName nvarchar(450),
	@LastName nvarchar(450),
	@PhoneNumber nvarchar(450)
AS
BEGIN
	UPDATE Account
	SET [FirstName] = @FirstName, [LastName] = @LastName, [PhoneNumber] = @PhoneNumber
	WHERE [Id] = @Id;
END