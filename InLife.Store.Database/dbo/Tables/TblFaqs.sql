CREATE TABLE [dbo].[TblFaqs] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_TblFaq_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   VARCHAR (36)       NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   VARCHAR (36)       NULL,
    [CategoryId]  INT                CONSTRAINT [DF_TblFaq_FaqCatId] DEFAULT ((0)) NOT NULL,
    [Question]    NVARCHAR (300)     CONSTRAINT [DF_TblFaq_FaqQuestion] DEFAULT ('') NOT NULL,
    [Answer]      NVARCHAR (800)     CONSTRAINT [DF_TblFaq_FaqAnswer] DEFAULT ('') NOT NULL,
    [IsActive]    BIT                CONSTRAINT [DF_TblFaq_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]  BIT                CONSTRAINT [DF_TblFaq_IsArchived] DEFAULT ((0)) NOT NULL,
    [SortNum]     INT                NULL,
    CONSTRAINT [PK_TblFaq] PRIMARY KEY CLUSTERED ([Id] ASC)
);

