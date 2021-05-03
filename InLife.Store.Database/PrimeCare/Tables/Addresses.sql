CREATE TABLE [PrimeCare].[Addresses] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [PhoneNumber] VARCHAR (20)       NULL,
    [Address1]    NVARCHAR (100)     NULL,
    [Address2]    NVARCHAR (100)     NULL,
    [Address3]    NVARCHAR (100)     NULL,
    [City]        NVARCHAR (30)      NULL,
    [Region]      NVARCHAR (30)      NULL,
    [Country]     NVARCHAR (30)      NULL,
    [ZipCode]     VARCHAR (5)        NULL,
    [DateCreated] DATETIMEOFFSET (7) CONSTRAINT [DF_Addresses_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

