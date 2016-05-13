CREATE TABLE [dbo].[TestRuns] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [AlgorithmId]    INT           NOT NULL,
    [TestSetId]      INT           NOT NULL,
    [UserId]         INT           NOT NULL,
    [DateOfRun]      DATETIME      NOT NULL,
    [RocCurveCalc]   BIT           NOT NULL,
    [Status]         NVARCHAR (25) NOT NULL,
    [RunsNumber]     NVARCHAR (20) NULL,
    [RocClassNumber] NVARCHAR (10) NULL,
    [TrainRatio]     NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestRuns_ToAlgorithms] FOREIGN KEY ([AlgorithmId]) REFERENCES [dbo].[Algorithms] ([Id]),
    CONSTRAINT [FK_TestRuns_ToTestSets] FOREIGN KEY ([TestSetId]) REFERENCES [dbo].[TestSets] ([Id]),
    CONSTRAINT [FK_TestRuns_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

