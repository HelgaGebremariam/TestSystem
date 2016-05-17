CREATE TABLE [dbo].[Users] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Role]     INT           NOT NULL,
    [UserName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_Users_ToRoles] FOREIGN KEY ([Role]) REFERENCES [dbo].[Roles] ([Id])
);



