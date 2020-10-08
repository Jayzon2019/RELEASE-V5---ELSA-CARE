CREATE TABLE [dbo].[UserLogins] (
    [ClusterId]           INT            IDENTITY (1, 1) NOT NULL,
    [LoginProvider]       VARCHAR (128)  NOT NULL,
    [ProviderKey]         VARCHAR (128)  NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              VARCHAR (36)   NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY NONCLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);




GO
CREATE CLUSTERED INDEX [CI_UserLogins]
    ON [dbo].[UserLogins]([ClusterId] ASC);

