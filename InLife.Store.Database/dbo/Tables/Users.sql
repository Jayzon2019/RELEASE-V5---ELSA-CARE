CREATE TABLE [dbo].[Users] (
    [Id]                   VARCHAR (36)       NOT NULL,
    [ClusterId]            INT                IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [FirstName]            NVARCHAR (50)      NULL,
    [MiddleName]           NVARCHAR (50)      NULL,
    [LastName]             NVARCHAR (50)      NULL,
    [DateActivated]        DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

