CREATE PROCEDURE [dbo].[spAccount_CreateAccount]
    @Id nvarchar(450),
    @FirstName nvarchar(450),
    @LastName nvarchar(450),
    @Email nvarchar(450),
    @PhoneNumber nvarchar(450),
    @Password nvarchar(450),
    @NSLink nvarchar(450),
    @IsEUMeasure bit
AS
BEGIN
    INSERT
    INTO Account ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [NSLink], [UnitOfMeasure])
    VALUES (@Id, @FirstName, @LastName, @Email, @PhoneNumber, @NSLink, @IsEUMeasure);
END