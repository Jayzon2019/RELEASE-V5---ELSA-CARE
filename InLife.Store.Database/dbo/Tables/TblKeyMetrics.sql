CREATE TABLE [dbo].[TblKeyMetrics] (
    [Id]           INT                IDENTITY (1, 1) NOT NULL,
    [PageName]     NVARCHAR (300)     NULL,
    [PageViews]    INT                NULL,
    [Sessions]     NVARCHAR (300)     NULL,
    [PageViewedAt] DATETIMEOFFSET (7) NULL,
    [PageLeftAt]   DATETIMEOFFSET (7) NULL,
    [MachineName]  NVARCHAR (300)     NULL,
    [Ip]           NVARCHAR (300)     NULL,
    CONSTRAINT [PK_TblKeyMetrics] PRIMARY KEY CLUSTERED ([Id] ASC)
);



