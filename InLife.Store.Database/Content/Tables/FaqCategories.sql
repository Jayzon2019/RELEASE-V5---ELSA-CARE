CREATE TABLE [Content].[FaqCategories] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) NOT NULL,
    [CreatedBy]   VARCHAR (36)       NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   VARCHAR (36)       NULL,
    [Name]        NVARCHAR (500)     NOT NULL,
    [Description] NVARCHAR (150)     NULL,
    CONSTRAINT [PK_Content_FaqCategories] PRIMARY KEY CLUSTERED ([Id] ASC)
);

