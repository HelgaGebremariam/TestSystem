CREATE TABLE [dbo].[UserInfo] (
    [Id]       INT            NOT NULL,
    [Email]    NVARCHAR (50)  NULL,
    [FullName] NVARCHAR (50)  NULL,
    [Company]  NVARCHAR (50)  NULL,
    [Other]    NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserInfo_ToUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
);

