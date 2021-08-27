CREATE TABLE [PrimeCare].[Applications] (
    [Id]                      UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate]             DATETIMEOFFSET (7) CONSTRAINT [DF_InsuranceApplications_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CompletedDate]           DATETIMEOFFSET (7) NULL,
    [ReferenceId]             INT                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReferenceCode]           VARCHAR (20)       NULL,
    [PolicyNumber]            VARCHAR (50)       NULL,
    [Status]                  VARCHAR (20)       NULL,
    [OrderNumber]             VARCHAR (20)       NULL,
    [OrderItemNumber]         VARCHAR (20)       NULL,
    [OrderStatus]             VARCHAR (20)       NULL,
    [ProductName]             NVARCHAR (50)      NOT NULL,
    [ProductCode]             VARCHAR (50)       NOT NULL,
    [PlanCode]                VARCHAR (50)       NOT NULL,
    [PlanName]                VARCHAR (50)       NOT NULL,
    [PlanVariantCode]         VARCHAR (50)       NULL,
    [PaymentMode]             VARCHAR (20)       NULL,
    [PaymentFrequency]        VARCHAR (20)       NULL,
    [PlanFaceAmount]          DECIMAL (19, 4)    NOT NULL,
    [PlanPremium]             DECIMAL (19, 4)    NOT NULL,
    [AgentCode]               VARCHAR (50)       NULL,
    [AgentFirstName]          NVARCHAR (50)      NULL,
    [AgentLastName]           NVARCHAR (50)      NULL,
    [ReferralSource]          NVARCHAR (80)      NULL,
    [Health1]                 BIT                NULL,
    [Health2]                 BIT                NULL,
    [Health3]                 BIT                NULL,
    [Fatca1]                  BIT                NULL,
    [Fatca2]                  BIT                NULL,
    [Question1]               BIT                NULL,
    [Question2]               BIT                NULL,
    [PolicyDeliveryOption]    BIT                NULL,
    [CustomerId]              UNIQUEIDENTIFIER   NULL,
    [InsuredId]               UNIQUEIDENTIFIER   NULL,
    [InsuredRelationship]     VARCHAR (20)       NULL,
    [BeneficiaryId]           UNIQUEIDENTIFIER   NULL,
    [BeneficiaryRelationship] VARCHAR (20)       NULL,
    [BeneficiaryRight]        VARCHAR (20)       NULL,
    [BeneficiaryPriority]     VARCHAR (20)       NULL,
    [IsEligible]              BIT                NULL,
    [AffiliateCode]           NVARCHAR(50) NULL, 
    [AffiliateName]           NVARCHAR(50) NULL, 
    [AffiliateStatus]         NVARCHAR(50) NULL,
    [BranchCode]              VARCHAR (50)       NULL,
    [BranchName]              NVARCHAR (50)      NULL,
    CONSTRAINT [PK_PrimeCare_Applications] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);








GO
CREATE CLUSTERED INDEX [CI_PrimeCare_Applications]
    ON [PrimeCare].[Applications]([CreatedDate] ASC);

