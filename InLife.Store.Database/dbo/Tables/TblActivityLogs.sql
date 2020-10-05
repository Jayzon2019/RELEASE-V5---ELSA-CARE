CREATE TABLE [dbo].[TblActivityLogs] (
    [Id]                  INT                IDENTITY (1, 1) NOT NULL,
    [ActivityBy]          NVARCHAR (300)     NULL,
    [ActionPerfomed]      NVARCHAR (200)     NULL,
    [ActivityDescription] NVARCHAR (500)     NULL,
    [IpAddress]           NVARCHAR (100)     NULL,
    [ActivityDate]        DATETIMEOFFSET (7) CONSTRAINT [DF_TblActivityLogs_ActivityDate] DEFAULT (sysdatetimeoffset()) NULL,
    [ActivityById]        VARCHAR (36)       NULL,
    CONSTRAINT [PK_TblActivityLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);



