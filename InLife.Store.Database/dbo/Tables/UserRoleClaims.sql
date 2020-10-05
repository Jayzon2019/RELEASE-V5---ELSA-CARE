CREATE TABLE [dbo].[UserRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]     VARCHAR (36)   NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRoleClaims_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([Id]) ON DELETE CASCADE
);

