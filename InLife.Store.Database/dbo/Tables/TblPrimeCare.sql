CREATE TABLE [dbo].[TblPrimeCare] (
    [Id]                       INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]              DATETIMEOFFSET (7) CONSTRAINT [DF_TblPrimeCare_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]                VARCHAR (36)       CONSTRAINT [DF_TblPrimeCare_CreatedBy] DEFAULT ('') NULL,
    [UpdatedDate]              DATETIMEOFFSET (7) NULL,
    [UpdatedBy]                VARCHAR (36)       NULL,
    [PrimeCareFile]            NVARCHAR (MAX)     CONSTRAINT [DF_TblPrimeCare_PrimeCareFile] DEFAULT ('') NOT NULL,
    [PrimeCareFileDescription] NVARCHAR (500)     NULL,
    [IsActive]                 BIT                CONSTRAINT [DF_TblPrimeCare_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]               BIT                NOT NULL,
    [PrimeCareFileName]        NVARCHAR (200)     NULL,
    CONSTRAINT [PK_TblPrimeCare] PRIMARY KEY CLUSTERED ([Id] ASC)
);

