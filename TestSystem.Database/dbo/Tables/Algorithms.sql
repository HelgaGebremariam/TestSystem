CREATE TABLE [dbo].[Algorithms] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatorId]      INT            NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [Name]           NVARCHAR (30)  NOT NULL,
    [DateOfCreation] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Algorithms_ToUsers] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Users] ([Id])
);

