CREATE TABLE [PrimeSecure].[Applications] (
    [Id]                      UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate]             DATETIMEOFFSET (7) CONSTRAINT [DF_PrimeSecure_Applications_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CompletedDate]           DATETIMEOFFSET (7) NULL,
    [ReferenceId]             INT                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReferenceCode]           VARCHAR (36)       NULL,
    [PolicyNumber]            VARCHAR (50)       NULL,
    [Status]                  VARCHAR (30)       NOT NULL,
    [ProductCode]             VARCHAR (50)       NOT NULL,
    [ProductName]             VARCHAR (50)       NOT NULL,
    [PlanCode]                VARCHAR (50)       NOT NULL,
    [PlanVariantCode]         VARCHAR (50)       NOT NULL,
    [PlanFaceAmount]          DECIMAL (19, 4)    NOT NULL,
    [PlanPremium]             DECIMAL (19, 4)    NOT NULL,
    [PaymentMode]             VARCHAR (20)       NULL,
    [PaymentFrequency]        VARCHAR (20)       NULL,
    [AgentCode]               VARCHAR (50)       NULL,
    [AgentFirstName]          NVARCHAR (50)      NULL,
    [AgentLastName]           NVARCHAR (50)      NULL,
    [ReferralSource]          NVARCHAR (50)      NULL,
    [CustomerId]              UNIQUEIDENTIFIER   NULL,
    [InsuredId]               UNIQUEIDENTIFIER   NULL,
    [InsuredRelationhhip]     VARCHAR (50)       NULL,
    [BeneficiaryId]           UNIQUEIDENTIFIER   NULL,
    [BeneficiaryRelationhhip] VARCHAR (50)       NULL,
    [Company]                 VARCHAR (50)       NULL,
    [Occupation]              VARCHAR (50)       NULL,
    [IncomeSource]            VARCHAR (50)       NULL,
    [IncomeAmount]            DECIMAL (19, 4)    NULL,
    [Height]                  DECIMAL (19, 4)    NULL,
    [Weight]                  DECIMAL (19, 4)    NULL,
    [DeclarationQuestion1]    BIT                NULL,
    [DeclarationQuestion2]    BIT                NULL,
    [DeclarationQuestion3]    BIT                NULL,
    [DeclarationQuestion4]    BIT                NULL,
    [CovidQuestion1]          BIT                NULL,
    [CovidQuestion2]          BIT                NULL,
    [CovidQuestion3]          BIT                NULL,
    [CovidQuestion4]          BIT                NULL,
    [CovidQuestion5]          BIT                NULL,
    [CovidQuestion6]          BIT                NULL,
    [CovidQuestion7]          BIT                NULL,
    [CovidQuestion8]          BIT                NULL,
    [Otp]                     VARCHAR (10)       NULL,
    [OtpExpiration]           DATETIMEOFFSET (7) NULL,
    [Session]                 VARCHAR (300)      NULL,
    [SessionExpiration]       DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_PrimeSecure_Applications] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [UK_PrimeSecure_Applications] UNIQUE NONCLUSTERED ([ReferenceCode] ASC)
);






GO
CREATE CLUSTERED INDEX [CI_PrimeSecure_Applications]
    ON [PrimeSecure].[Applications]([CreatedDate] ASC);

