CREATE TABLE [PrimeCare].[Persons] (
    [Id]                      UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate]             DATETIMEOFFSET (7) CONSTRAINT [DF_Persons_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [NamePrefix]              NVARCHAR (30)      NULL,
    [NameSuffix]              NVARCHAR (30)      NULL,
    [FirstName]               NVARCHAR (50)      NOT NULL,
    [MiddleName]              NVARCHAR (50)      NULL,
    [LastName]                NVARCHAR (50)      NOT NULL,
    [Nationality]             NVARCHAR (50)      NULL,
    [CivilStatus]             VARCHAR (20)       NULL,
    [Gender]                  VARCHAR (20)       NULL,
    [BirthDate]               SMALLDATETIME      NULL,
    [EmailAddress]            VARCHAR (320)      NULL,
    [MobileNumber]            VARCHAR (20)       NULL,
    [BirthAddressId]          UNIQUEIDENTIFIER   NULL,
    [HomeAddressId]           UNIQUEIDENTIFIER   NULL,
    [WorkAddressId]           UNIQUEIDENTIFIER   NULL,
    [PreferredMailingAddress] VARCHAR (10)       NULL,
    [CompanyName]             NVARCHAR (50)      NULL,
    [Occupation]              NVARCHAR (50)      NULL,
    [IncomeMonthlyAmount]     DECIMAL (19, 4)    NULL,
    [IncomeSource]            NVARCHAR (50)      NULL,
    [LegalIdType]             VARCHAR (30)       NULL,
    [LegalIdNumber]           VARCHAR (30)       NULL,
    [LegalIdImage]            VARCHAR (MAX)      NULL,
    [SecondaryLegalIdType]    VARCHAR (30)       NULL,
    [SecondaryLegalIdNumber]  VARCHAR (30)       NULL,
    [SecondaryLegalIdImage]   VARCHAR (MAX)      NULL,
    CONSTRAINT [PK_PrimeCare_Persons] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);






GO
CREATE CLUSTERED INDEX [CI_PrimeCare_Persons]
    ON [PrimeCare].[Persons]([CreatedDate] ASC);

