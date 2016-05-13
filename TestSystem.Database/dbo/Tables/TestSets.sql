CREATE TABLE [dbo].[TestSets] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [CreatorId]       INT             NOT NULL,
    [DateOfCreation]  DATETIME        NOT NULL,
    [TotalRuns]       INT             NOT NULL,
    [Type]            NVARCHAR (50)   NULL,
    [Description]     NVARCHAR (MAX)  NULL,
    [Data]            VARBINARY (MAX) NOT NULL,
    [Name]            NVARCHAR (50)   NOT NULL,
    [Size]            INT             NULL,
    [ExpectedResults] NVARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestSets_ToUsers] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Users] ([Id])
);



