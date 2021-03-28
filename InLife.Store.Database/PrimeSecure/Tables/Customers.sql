CREATE TABLE [PrimeSecure].[Customers] (
    [Id]            UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate]   DATETIMEOFFSET (7) CONSTRAINT [DF_PrimeSecure_Customers_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [NamePrefix]    NVARCHAR (10)      NULL,
    [NameSuffix]    NVARCHAR (10)      NULL,
    [FirstName]     NVARCHAR (50)      NOT NULL,
    [MiddleName]    NVARCHAR (50)      NULL,
    [LastName]      NVARCHAR (50)      NOT NULL,
    [Nationality]   NVARCHAR (50)      NULL,
    [CivilStatus]   VARCHAR (20)       NULL,
    [Gender]        VARCHAR (20)       NULL,
    [BirthDate]     SMALLDATETIME      NULL,
    [BirthCountry]  NVARCHAR (30)      NULL,
    [BirthRegion]   NVARCHAR (30)      NULL,
    [BirthCity]     NVARCHAR (30)      NULL,
    [EmailAddress]  VARCHAR (300)      NULL,
    [MobileNumber]  VARCHAR (20)       NULL,
    [HomeAddressId] UNIQUEIDENTIFIER   NULL,
    [WorkAddressId] UNIQUEIDENTIFIER   NULL,
    CONSTRAINT [PK_PrimeSecure_Customers] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE CLUSTERED INDEX [CI_PrimeSecure_Customers]
    ON [PrimeSecure].[Customers]([Id] ASC);

