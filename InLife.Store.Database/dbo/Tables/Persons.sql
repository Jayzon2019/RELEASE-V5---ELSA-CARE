CREATE TABLE [dbo].[Persons] (
    [ClusteringId]            BIGINT             IDENTITY (1, 1) NOT NULL,
    [Id]                      UNIQUEIDENTIFIER   NOT NULL,
    [DateCreated]             DATETIMEOFFSET (7) CONSTRAINT [DF_Persons_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [NamePrefix]              NVARCHAR (10)      NULL,
    [NameSuffix]              NVARCHAR (10)      NULL,
    [FirstName]               NVARCHAR (50)      NOT NULL,
    [MiddleName]              NVARCHAR (50)      NULL,
    [LastName]                NVARCHAR (50)      NOT NULL,
    [Nationality]             NVARCHAR (50)      NOT NULL,
    [CivilStatus]             VARCHAR (20)       NOT NULL,
    [Gender]                  VARCHAR (20)       NOT NULL,
    [BirthDate]               SMALLDATETIME      NULL,
    [BirthCountry]            NVARCHAR (30)      NULL,
    [BirthRegion]             NVARCHAR (30)      NULL,
    [BirthCity]               NVARCHAR (30)      NULL,
    [EmailAddress]            VARCHAR (320)      NULL,
    [MobileNumber]            VARCHAR (20)       NULL,
    [HomePhoneNumber]         VARCHAR (20)       NULL,
    [HomeAddress1]            NVARCHAR (100)     NULL,
    [HomeAddress2]            NVARCHAR (100)     NULL,
    [HomeAddress3]            NVARCHAR (100)     NULL,
    [HomeCity]                NVARCHAR (30)      NULL,
    [HomeRegion]              NVARCHAR (30)      NULL,
    [HomeZipCode]             NVARCHAR (10)      NULL,
    [HomeCountry]             NVARCHAR (30)      NULL,
    [WorkPhoneNumber]         VARCHAR (20)       NULL,
    [WorkAddress1]            NVARCHAR (100)     NULL,
    [WorkAddress2]            NVARCHAR (100)     NULL,
    [WorkAddress3]            NVARCHAR (100)     NULL,
    [WorkCity]                NVARCHAR (30)      NULL,
    [WorkRegion]              NVARCHAR (30)      NULL,
    [WorkZipCode]             NVARCHAR (10)      NULL,
    [WorkHomeCountry]         NVARCHAR (30)      NULL,
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
    CONSTRAINT [PK_Persons] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);




GO
CREATE CLUSTERED INDEX [CI_Persons]
    ON [dbo].[Persons]([ClusteringId] ASC);

