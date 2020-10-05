CREATE TABLE [dbo].[Customers] (
    [Id]            INT                IDENTITY (1, 1) NOT NULL,
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
    [HomeAddressId] INT                NULL,
    [WorkAddressId] INT                NULL,
    [DateCreated]   DATETIMEOFFSET (7) CONSTRAINT [DF_Customers_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

