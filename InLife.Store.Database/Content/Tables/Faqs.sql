CREATE TABLE [Content].[Faqs] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) NOT NULL,
    [CreatedBy]   VARCHAR (36)       NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   VARCHAR (36)       NULL,
    [CategoryId]  INT                NOT NULL,
    [Question]    NVARCHAR (300)     NOT NULL,
    [Answer]      NVARCHAR (MAX)     NOT NULL,
    [SortNum]     INT                NULL,
    CONSTRAINT [PK_Content_Faq] PRIMARY KEY CLUSTERED ([Id] ASC)
);

