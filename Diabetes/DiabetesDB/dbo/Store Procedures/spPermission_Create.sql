CREATE PROCEDURE [dbo].[spPermission_Create]
	@WatcherId nvarchar(450),
	@TargetId nvarchar(450),
	@StartDate datetime2(7), 
	@ExpireDate datetime2(7),
	@Days int,
	@WeeksActive int,
	@WeeksDeactive int,
	@Attributes int
AS
BEGIN
	Insert INTO Permission ([WatcherId], [TargetId], [StartDate], [ExpireDate], [Days], [WeeksActive], [WeeksDeactive], [Attributes], Accepted)
	VALUES (@WatcherId, @TargetId, @StartDate, @ExpireDate, @Days, @WeeksActive, @WeeksDeactive, @Attributes, 0);
END