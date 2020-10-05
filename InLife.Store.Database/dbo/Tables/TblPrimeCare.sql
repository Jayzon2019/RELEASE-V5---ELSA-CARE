CREATE TABLE [dbo].[TblPrimeCare] (
    [PrimeCareId]              INT            IDENTITY (1, 1) NOT NULL,
    [PrimeCareFile]            NVARCHAR (MAX) CONSTRAINT [DF_TblPrimeCare_PrimeCareFile] DEFAULT ('') NOT NULL,
    [PrimeCareFileDescription] NVARCHAR (500) NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_TblPrimeCare_CreatedDate] DEFAULT ('') NOT NULL,
    [CreatedBy]                INT            CONSTRAINT [DF_TblPrimeCare_CreatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate]              DATETIME       NULL,
    [UpdatedBy]                INT            NULL,
    [IsActive]                 BIT            CONSTRAINT [DF_TblPrimeCare_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]               BIT            NOT NULL,
    [PrimeCareFileName]        NVARCHAR (200) NULL,
    CONSTRAINT [PK_TblPrimeCare] PRIMARY KEY CLUSTERED ([PrimeCareId] ASC)
);

