CREATE TABLE [dbo].[TblUsers] (
    [Id]             INT                IDENTITY (1, 1) NOT NULL,
    [DOB]            DATETIME           NULL,
    [GenderId]       INT                CONSTRAINT [DF_TblUsers_GenderId] DEFAULT ((0)) NOT NULL,
    [FirstName]      NVARCHAR (150)     CONSTRAINT [DF_TblUsers_FirstName] DEFAULT ('') NOT NULL,
    [LastName]       NVARCHAR (150)     NULL,
    [UserRoleId]     INT                CONSTRAINT [DF_TblUsers_UserRoleId] DEFAULT ((0)) NOT NULL,
    [Email]          NVARCHAR (300)     CONSTRAINT [DF_TblUsers_Email] DEFAULT ('') NOT NULL,
    [Password]       NVARCHAR (MAX)     CONSTRAINT [DF_TblUsers_Password] DEFAULT ('') NOT NULL,
    [UserImg]        NVARCHAR (500)     NULL,
    [Phone]          NVARCHAR (100)     NULL,
    [CreatedDate]    DATETIMEOFFSET (7) CONSTRAINT [DF_TblUsers_CreatedDate_1] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]      VARCHAR (36)       NULL,
    [UpdatedDate]    DATETIMEOFFSET (7) NULL,
    [UpdatedBy]      VARCHAR (36)       NULL,
    [IsActive]       BIT                CONSTRAINT [DF_TblUsers_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]     BIT                CONSTRAINT [DF_TblUsers_IsArchive] DEFAULT ((0)) NOT NULL,
    [RestCode]       NVARCHAR (50)      CONSTRAINT [DF_TblUsers_RestCode] DEFAULT ('') NOT NULL,
    [RestDate]       DATETIME           CONSTRAINT [DF_TblUsers_RestDate] DEFAULT ('') NOT NULL,
    [ActivationCode] NVARCHAR (50)      CONSTRAINT [DF_TblUsers_ActivationCode] DEFAULT ('') NOT NULL,
    [ActivationDate] DATETIME           CONSTRAINT [DF_TblUsers_ActivationDate] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_TblUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);





