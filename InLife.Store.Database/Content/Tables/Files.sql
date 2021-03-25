CREATE TABLE [Content].[Files] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) NOT NULL,
    [CreatedBy]   VARCHAR (36)       NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [UpdatedBy]   VARCHAR (36)       NULL,
    [MediaType]   VARCHAR (30)       NULL,
    [Data]        NVARCHAR (MAX)     NULL,
    [Description] NVARCHAR (500)     NULL,
    [FileName]    VARCHAR (200)      NULL,
    CONSTRAINT [PK_Content_Files] PRIMARY KEY CLUSTERED ([Id] ASC)
);

