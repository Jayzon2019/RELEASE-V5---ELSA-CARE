CREATE TABLE [dbo].[TblUserRoles] (
    [UserRoleId] INT            IDENTITY (1, 1) NOT NULL,
    [UserRole]   NVARCHAR (100) CONSTRAINT [DF_TblUserRoles_UserRole] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_TblUserRoles] PRIMARY KEY CLUSTERED ([UserRoleId] ASC)
);

