CREATE TABLE [dbo].[NotificationSetting]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Owner] NVARCHAR(450) NOT NULL, 
    [Threshold] FLOAT NOT NULL, 
    [ThresholdType] INT NOT NULL, 
    [NotificationType] INT NOT NULL, 
    [Note] NVARCHAR(450) NULL
)
