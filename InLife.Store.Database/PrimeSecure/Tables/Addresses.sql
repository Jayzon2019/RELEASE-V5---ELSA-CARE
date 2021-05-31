CREATE TABLE [PrimeSecure].[Addresses] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_PrimeSecure_Addresses_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [PhoneNumber] VARCHAR (20)       NULL,
    [Address1]    NVARCHAR (100)     NULL,
    [Address2]    NVARCHAR (100)     NULL,
    [Address3]    NVARCHAR (100)     NULL,
    [Town]        NVARCHAR (30)      NULL,
    [City]        NVARCHAR (30)      NULL,
    [Region]      NVARCHAR (30)      NULL,
    [Country]     NVARCHAR (30)      NULL,
    [ZipCode]     VARCHAR (5)        NULL,
    CONSTRAINT [PK_PrimeSecure_Addresses] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE CLUSTERED INDEX [CI_PrimeSecure_Addresses]
    ON [PrimeSecure].[Addresses]([CreatedDate] ASC);

