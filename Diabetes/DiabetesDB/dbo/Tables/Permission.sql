CREATE TABLE [dbo].[Permission]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [WatcherID] NVARCHAR(450) NOT NULL, 
    [TargetID] NVARCHAR(450) NOT NULL, 
    [StartDate] DATETIME2 NOT NULL, 
    [ExpireDate] DATETIME2 NOT NULL, 
    [Days] INT NOT NULL, 
    [WeeksActive] INT NOT NULL, 
    [WeeksDeactive] INT NOT NULL, 
    [Attributes] INT NOT NULL, 
    [Accepted] BIT NOT NULL
)
