CREATE PROCEDURE [dbo].[spGetRolesByUserId]
	@UserId NVARCHAR (450)
AS
BEGIN
	SELECT RoleId
	FROM dbo.Users
	JOIN dbo.UserRoles
	ON (dbo.Users.Id = dbo.UserRoles.UserId)
	WHERE dbo.Users.Id = @UserId;
END