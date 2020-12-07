CREATE PROCEDURE [dbo].[spAccount_GetByEmail]
	@Email nvarchar(450)
AS
BEGIN 
	SELECT *
	FROM Account
	WHERE PhoneNumber = @Email;
END
