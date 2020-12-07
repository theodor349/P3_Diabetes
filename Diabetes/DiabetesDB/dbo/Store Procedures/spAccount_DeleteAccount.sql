CREATE PROCEDURE [dbo].[spAccount_DeleteAccount]
	@Id int
AS
BEGIN
	DELETE 
	FROM Permission
	WHERE Id = @Id;
END