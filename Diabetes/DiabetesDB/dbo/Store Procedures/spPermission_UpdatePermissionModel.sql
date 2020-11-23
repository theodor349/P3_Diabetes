CREATE PROCEDURE [dbo].[spPermission_UpdatePermissionModel]
	@Id int,
	@StartDate datetime2(7), 
	@ExpireDate datetime2(7),
	@Days int,
	@WeeksActive int,
	@WeeksDeactive int,
	@Attributes int
AS
BEGIN
	UPDATE Permission
	SET [StartDate] = @StartDate,
	[ExpireDate] = @ExpireDate,
	[Days] = @Days,
	[WeeksActive] = @WeeksActive,
	[WeeksDeactive] = @WeeksDeactive,
	[Attributes] = @Attributes
	WHERE [Id] = @Id; 	
END
