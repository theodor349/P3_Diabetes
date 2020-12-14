CREATE PROCEDURE [dbo].[spAccount_UpdateAccount]
	@Id nvarchar(450),
	@FirstName nvarchar(450),
	@LastName nvarchar(450),
	@IsEU bit
AS
BEGIN
	UPDATE Account
	SET [FirstName] = @FirstName, [LastName] = @LastName, [UnitOfMeasure] = @IsEU
	WHERE [Id] = @Id;
END