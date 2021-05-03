CREATE TABLE [Group].[Files] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_Group_Files_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [MediaType]   VARCHAR (120)      NULL,
    [FileName]    VARCHAR (300)      NULL,
    [Data]        NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_Group_Files] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO

CREATE CLUSTERED INDEX [CI_Group_Files]
    ON [Group].[Files]([CreatedDate] ASC);



