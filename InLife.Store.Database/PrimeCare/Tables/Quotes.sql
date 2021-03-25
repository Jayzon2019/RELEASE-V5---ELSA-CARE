CREATE TABLE [PrimeCare].[Quotes] (
    [Id]                INT                IDENTITY (1, 1) NOT NULL,
    [CustomerId]        INT                NOT NULL,
    [ProductCode]       VARCHAR (10)       NOT NULL,
    [ProductName]       NVARCHAR (50)      NOT NULL,
    [ProductFaceAmount] DECIMAL (19, 4)    NULL,
    [PaymentMode]       INT                NULL,
    [AgentCode]         VARCHAR (50)       NULL,
    [AgentFirstName]    NVARCHAR (50)      NULL,
    [AgentLastName]     NVARCHAR (50)      NULL,
    [ReferralSource]    NVARCHAR (80)      NULL,
    [Health1]           BIT                NULL,
    [Health2]           BIT                NULL,
    [Health3]           BIT                NULL,
    [IsEligible]        BIT                NULL,
    [DateCreated]       DATETIMEOFFSET (7) CONSTRAINT [DF_Quotes_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    CONSTRAINT [PK_Quotes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

