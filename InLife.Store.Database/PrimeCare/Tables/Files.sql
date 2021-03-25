CREATE TABLE [PrimeCare].[Files] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_PrimeCare_Files_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [MediaType]   VARCHAR (30)       NULL,
    [Data]        NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_PrimeCare_Files] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE CLUSTERED INDEX [CI_PrimeCare_Files]
    ON [PrimeCare].[Files]([CreatedDate] ASC);

