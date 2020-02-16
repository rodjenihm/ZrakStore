CREATE PROCEDURE [dbo].[spUsers_Add]
	@Id NVARCHAR (450),
	@Username NVARCHAR (450),
	@PasswordHash NVARCHAR (MAX)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Users (Id, CreatedAt, Username, PasswordHash)
	VALUES (@Id, GETDATE(), @Username, @PasswordHash);
END