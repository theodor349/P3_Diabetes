﻿CREATE PROCEDURE [dbo].[spAccount_GetPendingPermissions]
	@id nvarchar(450)
AS
BEGIN
	SELECT * 
	FROM Permission
	WHERE [WatcherId] = @id;
END