CREATE TABLE [dbo].[Account]
(
	[Id] NVARCHAR(450) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [NSLink] NVARCHAR(450) NULL, 
    [UnitOfMeasure] BIT NOT NULL
)
