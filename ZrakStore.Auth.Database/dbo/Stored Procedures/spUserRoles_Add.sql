﻿CREATE PROCEDURE [dbo].[spUserRoles_Add]
	@UserId NVARCHAR (450),
	@RoleId NVARCHAR (450) = 'user'
AS
BEGIN
	INSERT INTO UserRoles (UserId, RoleId)
	VALUES (@UserId, @RoleId);
END