CREATE TABLE [dbo].[ReferenceCodes] (
    [Id]          VARCHAR (14) NOT NULL,
    [CreatedDate] DATETIME     CONSTRAINT [DF_ReferenceCodes_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_ReferenceCodes] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE CLUSTERED INDEX [CI_ReferenceCodes]
    ON [dbo].[ReferenceCodes]([CreatedDate] ASC);

