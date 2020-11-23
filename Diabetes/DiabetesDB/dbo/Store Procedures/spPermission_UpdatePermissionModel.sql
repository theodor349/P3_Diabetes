CREATE PROCEDURE [dbo].[spPermission_UpdatePermissionModel]
	@id nvarchar(450),
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
	WHERE [id] = @id; 	
END
