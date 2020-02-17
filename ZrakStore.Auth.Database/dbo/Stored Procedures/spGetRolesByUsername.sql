CREATE PROCEDURE [dbo].[spGetRolesByUsername]
	@Username NVARCHAR (450)
AS
BEGIN
	SELECT RoleId
	FROM dbo.Users
	JOIN dbo.UserRoles
	ON (dbo.Users.Id = dbo.UserRoles.UserId)
	WHERE dbo.Users.UserName = @Username;
END