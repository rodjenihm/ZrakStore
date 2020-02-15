CREATE PROCEDURE [dbo].[spUsers_GetByUsername]
	@Username NVARCHAR (450)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM [dbo].[Users]
	WHERE Username = @Username;
END