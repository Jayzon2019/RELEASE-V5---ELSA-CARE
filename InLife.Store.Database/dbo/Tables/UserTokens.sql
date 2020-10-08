CREATE TABLE [dbo].[UserTokens] (
    [ClusterId]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId]        VARCHAR (36)   NOT NULL,
    [LoginProvider] VARCHAR (128)  NOT NULL,
    [Name]          VARCHAR (128)  NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY NONCLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);




GO
CREATE CLUSTERED INDEX [CI_UserTokens]
    ON [dbo].[UserTokens]([ClusterId] ASC);

