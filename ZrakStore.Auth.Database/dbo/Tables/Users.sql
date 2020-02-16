CREATE TABLE [dbo].[Users] (
    [Id]           NVARCHAR (450) NOT NULL,
    [CreatedAt]    DATETIME2 (7)  NOT NULL,
    [Username]     NVARCHAR (450) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username]
    ON [dbo].[Users]([Username] ASC);