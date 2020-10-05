CREATE TABLE [dbo].[TblEmailCredentials] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserName]   NVARCHAR (300) NULL,
    [Password]   NVARCHAR (300) NULL,
    [Smtp]       NVARCHAR (200) NULL,
    [port]       INT            NULL,
    [IsBodyHtml] BIT            NULL,
    [EnableSSL]  BIT            NULL,
    CONSTRAINT [PK_TblEmailCredentials] PRIMARY KEY CLUSTERED ([Id] ASC)
);



