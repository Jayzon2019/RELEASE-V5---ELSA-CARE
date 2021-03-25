CREATE TABLE [dbo].[Files] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                NOT NULL,
    [DateCreated] DATETIMEOFFSET (7) CONSTRAINT [DF_Files_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [MediaType]   VARCHAR (30)       NULL,
    [Data]        NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);






GO
CREATE UNIQUE CLUSTERED INDEX [CI_Files]
    ON [dbo].[Files]([Id] ASC);

