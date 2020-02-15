CREATE TABLE [dbo].[Roles] (
    [Id]   NVARCHAR (450) NOT NULL,
    [CreatedAt]    DATETIME2 (7)  NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_Name]
    ON [dbo].[Roles]([Name] ASC);