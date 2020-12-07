CREATE PROCEDURE [dbo].[spAccount_GetByPhoneNumber]
	@PhoneNumber nvarchar(450)
AS
BEGIN 
	SELECT *
	FROM Account
	WHERE PhoneNumber = @PhoneNumber;
END