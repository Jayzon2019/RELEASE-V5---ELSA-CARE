CREATE TABLE [PrimeCare].[Applications] (
    [Id]                      UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate]             DATETIMEOFFSET (7) CONSTRAINT [DF_InsuranceApplications_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CompletedDate]           DATETIMEOFFSET (7) NULL,
    [ReferenceCode]           VARCHAR (36)       NULL,
    [PolicyNumber]            VARCHAR (50)       NULL,
    [Status]                  VARCHAR (20)       NULL,
    [ProductName]             NVARCHAR (50)      NOT NULL,
    [ProductCode]             VARCHAR (20)       NOT NULL,
    [PlanCode]                VARCHAR (20)       NOT NULL,
    [PlanName]                NVARCHAR (30)      NOT NULL,
    [PlanPaymentMode]         VARCHAR (20)       NOT NULL,
    [PlanFaceAmount]          DECIMAL (19, 4)    NOT NULL,
    [PlanPremium]             DECIMAL (19, 4)    NOT NULL,
    [AgentCode]               VARCHAR (50)       NULL,
    [AgentFirstName]          NVARCHAR (50)      NULL,
    [AgentLastName]           NVARCHAR (50)      NULL,
    [ReferralSource]          NVARCHAR (50)      NULL,
    [Heallth1]                VARCHAR (10)       NULL,
    [Heallth2]                VARCHAR (10)       NULL,
    [Heallth3]                VARCHAR (10)       NULL,
    [Fatca1]                  VARCHAR (10)       NULL,
    [Fatca2]                  VARCHAR (10)       NULL,
    [Question1]               VARCHAR (10)       NULL,
    [Question2]               VARCHAR (10)       NULL,
    [PolicyDeliveryOption]    VARCHAR (20)       NULL,
    [CustomerId]              UNIQUEIDENTIFIER   NULL,
    [InsuredId]               UNIQUEIDENTIFIER   NOT NULL,
    [InsuredRelationship]     VARCHAR (20)       NULL,
    [BeneficiaryId]           UNIQUEIDENTIFIER   NOT NULL,
    [BeneficiaryRelationship] VARCHAR (20)       NULL,
    [BeneficiaryRight]        VARCHAR (20)       NULL,
    [BeneficiaryPriority]     VARCHAR (20)       NULL,
    CONSTRAINT [PK_PrimeCare_Applications] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);






GO
CREATE CLUSTERED INDEX [CI_PrimeCare_Applications]
    ON [PrimeCare].[Applications]([CreatedDate] ASC);

