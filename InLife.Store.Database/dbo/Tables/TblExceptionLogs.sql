CREATE TABLE [dbo].[TblExceptionLogs] (
    [Id]       INT                IDENTITY (1, 1) NOT NULL,
    [ExMsg]    NVARCHAR (500)     CONSTRAINT [DF_TblExceptionLogs_ExMsg] DEFAULT ('') NOT NULL,
    [ExSource] NVARCHAR (MAX)     CONSTRAINT [DF_TblExceptionLogs_ExSource] DEFAULT ('') NOT NULL,
    [ExUrl]    NVARCHAR (200)     CONSTRAINT [DF_TblExceptionLogs_ExUrl] DEFAULT ('') NOT NULL,
    [ExDate]   DATETIMEOFFSET (7) CONSTRAINT [DF_TblExceptionLogs_ExDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [ExInner]  NVARCHAR (MAX)     CONSTRAINT [DF_TblExceptionLogs_ExInner] DEFAULT ('') NULL,
    CONSTRAINT [PK_TblExceptionLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);



