CREATE TABLE [dbo].[Users_UserRoles] (
    [ClusterId] INT          IDENTITY (1, 1) NOT NULL,
    [UserId]    VARCHAR (36) NOT NULL,
    [RoleId]    VARCHAR (36) NOT NULL,
    CONSTRAINT [PK_Users_UserRoles] PRIMARY KEY NONCLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_Users_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_UserRoles_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);

