CREATE TABLE [dbo].[TblFaqCategories] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (500)     CONSTRAINT [DF_TblFaqCategories_FaqCategory] DEFAULT ('') NOT NULL,
    [Description] NVARCHAR (150)     NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_TblFaqCategories_CreatedDate] DEFAULT ('') NOT NULL,
    [CreatedBy]   VARCHAR (36)       NULL,
    [UpdatedDate] DATETIME           NULL,
    [UpdatedBy]   INT                NULL,
    [IsActive]    BIT                CONSTRAINT [DF_TblFaqCategories_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]  BIT                CONSTRAINT [DF_TblFaqCategories_IsArchived] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TblFaqCategories] PRIMARY KEY CLUSTERED ([Id] ASC)
);



