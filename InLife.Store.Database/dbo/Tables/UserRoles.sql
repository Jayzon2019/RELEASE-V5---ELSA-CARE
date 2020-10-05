CREATE TABLE [dbo].[UserRoles] (
    [Id]               VARCHAR (36)   NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

