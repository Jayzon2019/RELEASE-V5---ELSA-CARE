CREATE TABLE [dbo].[Files] (
    [Id]        VARCHAR (36) NOT NULL,
    [ClusterId] INT          NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [CI_Files]
    ON [dbo].[Files]([Id] ASC);

