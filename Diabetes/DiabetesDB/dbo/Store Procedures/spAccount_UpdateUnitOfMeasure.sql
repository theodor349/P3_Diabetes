CREATE PROCEDURE [dbo].[spAccount_UpdateUnitOfMeasure]
	@Id nvarchar(450),
	@NewMeasure bit
AS
BEGIN 
	UPDATE Account
	SET [UnitOfMeasure] = @NewMeasure
	WHERE [Id] = @Id;
END