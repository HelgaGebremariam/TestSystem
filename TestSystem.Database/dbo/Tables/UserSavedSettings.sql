CREATE TABLE [dbo].[UserSavedSettings] (
    [Id]          INT           NOT NULL,
    [UserID]      INT           NOT NULL,
    [ObjectName]  NVARCHAR (50) NOT NULL,
    [ObjectValue] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserSavedSettings_ToUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([Id])
);

