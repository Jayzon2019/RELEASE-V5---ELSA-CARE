CREATE TABLE [dbo].[UserSessions] (
    [Id]           UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]    INT                IDENTITY (1, 1) NOT NULL,
    [UserId]       VARCHAR (36)       NULL,
    [Value]        VARBINARY (4000)   NULL,
    [LastActivity] DATETIMEOFFSET (7) NULL,
    [Expires]      DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_UserSessions] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE CLUSTERED INDEX [CI_UserSessions]
    ON [dbo].[UserSessions]([ClusterId] ASC);

