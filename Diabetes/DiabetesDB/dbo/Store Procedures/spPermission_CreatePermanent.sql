CREATE PROCEDURE [dbo].[spPermission_CreatePermanent]
	@UserId nvarchar(450)
AS
BEGIN
	DECLARE @start datetime2  = '2016-10-23';
	DECLARE @end datetime2  = '9999-10-23';
	Insert INTO Permission ([WatcherId], [TargetId], [StartDate], [ExpireDate], [Days], [WeeksActive], [WeeksDeactive], [Attributes], [Accepted])
	VALUES (@UserId, @UserId, @start, @end, 0, 0, 0, 7, 1);	
END
