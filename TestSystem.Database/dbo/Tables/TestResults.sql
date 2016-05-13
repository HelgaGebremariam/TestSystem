CREATE TABLE [dbo].[TestResults] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [AlgorithmId]          INT            NOT NULL,
    [TP]                   NVARCHAR (10)  NULL,
    [FN]                   NVARCHAR (10)  NULL,
    [TN]                   NVARCHAR (10)  NULL,
    [FP]                   NVARCHAR (10)  NULL,
    [RocCoordinatesSensiv] NVARCHAR (MAX) NULL,
    [TestRunId]            INT            NOT NULL,
    [CorrectRate]          NVARCHAR (10)  NULL,
    [OtherInfo]            NVARCHAR (50)  NULL,
    [RocCoordinatesSpecif] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestResults_ToAlgorithms] FOREIGN KEY ([AlgorithmId]) REFERENCES [dbo].[Algorithms] ([Id]),
    CONSTRAINT [FK_TestResults_ToTestRuns] FOREIGN KEY ([TestRunId]) REFERENCES [dbo].[TestRuns] ([Id])
);

