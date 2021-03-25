CREATE TABLE [Content].[Heroes] (
    [Id]                    INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]           DATETIMEOFFSET (7) NOT NULL,
    [CreatedBy]             VARCHAR (36)       NULL,
    [UpdatedDate]           DATETIMEOFFSET (7) NULL,
    [UpdatedBy]             VARCHAR (36)       NULL,
    [Title]                 NVARCHAR (300)     NULL,
    [SubTitle]              NVARCHAR (500)     NULL,
    [BackgroundImageLarge]  UNIQUEIDENTIFIER   NULL,
    [BackgroundImageMedium] UNIQUEIDENTIFIER   NULL,
    [BackgroundImageSmall]  UNIQUEIDENTIFIER   NULL,
    [ButtonLabel]           NVARCHAR (30)      NOT NULL,
    [ButtonHref]            VARCHAR (300)      NOT NULL,
    [Alignment]             NVARCHAR (50)      NULL,
    CONSTRAINT [PK_Content_Heroes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

