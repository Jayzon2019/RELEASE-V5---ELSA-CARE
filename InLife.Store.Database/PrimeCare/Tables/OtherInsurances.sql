CREATE TABLE [PrimeCare].[OtherInsurances] (
    [ClusteringId]           BIGINT             IDENTITY (1, 1) NOT NULL,
    [Id]                     UNIQUEIDENTIFIER   NOT NULL,
    [DateCreated]            DATETIMEOFFSET (7) CONSTRAINT [DF_OtherInsurances_DateCreated] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [InsuranceApplicationId] UNIQUEIDENTIFIER   NOT NULL,
    [CompanyName]            NVARCHAR (50)      NOT NULL,
    [BasicFaceAmount]        DECIMAL (19, 4)    NULL,
    [DreadDiseaseFaceAmount] DECIMAL (19, 4)    NULL,
    [AccidentalFaceAmount]   DECIMAL (19, 4)    NULL,
    [IssueYear]              INT                NULL,
    CONSTRAINT [PK_OtherInsurances] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);





