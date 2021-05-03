CREATE TABLE [Group].[Applications] (
    [Id]                                 UNIQUEIDENTIFIER   NOT NULL,
    [CreatedDate]                        DATETIMEOFFSET (7) CONSTRAINT [DF_Group_Applications_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CompletedDate]                      DATETIMEOFFSET (7) NULL,
    [ExportedDate]                       DATETIMEOFFSET (7) NULL,
    [ReferenceCode]                      VARCHAR (36)       NULL,
    [PolicyNumber]                       VARCHAR (50)       NULL,
    [Status]                             VARCHAR (30)       NOT NULL,
    [ProductCode]                        VARCHAR (50)       NOT NULL,
    [ProductName]                        VARCHAR (50)       NOT NULL,
    [PlanCode]                           VARCHAR (50)       NOT NULL,
    [PlanVariantCode]                    VARCHAR (50)       NOT NULL,
    [PlanFaceAmount]                     DECIMAL (19, 4)    NOT NULL,
    [PlanPremium]                        DECIMAL (19, 4)    NOT NULL,
    [PaymentMode]                        VARCHAR (20)       NULL,
    [PaymentFrequency]                   VARCHAR (20)       NULL,
    [TotalMembers]                       INT                NULL,
    [TotalTeachers]                      INT                NULL,
    [TotalStudents]                      INT                NULL,
    [BusinessStructure]                  VARCHAR (30)       NULL,
    [CompanyName]                        NVARCHAR (300)     NULL,
    [CompanyPhoneNumber]                 VARCHAR (20)       NULL,
    [CompanyMobileNumber]                VARCHAR (20)       NULL,
    [CompanyEmailAddress]                VARCHAR (300)      NULL,
    [CompanyAddress1]                    NVARCHAR (300)     NULL,
    [CompanyAddress2]                    NVARCHAR (300)     NULL,
    [CompanyTown]                        NVARCHAR (50)      NULL,
    [CompanyCity]                        NVARCHAR (50)      NULL,
    [CompanyRegion]                      NVARCHAR (50)      NULL,
    [CompanyZipCode]                     VARCHAR (5)        NULL,
    [CompanyCountry]                     NVARCHAR (50)      NULL,
    [RepresentativeNamePrefix]           NVARCHAR (20)      NULL,
    [RepresentativeNameSuffix]           NVARCHAR (20)      NULL,
    [RepresentativeFirstName]            NVARCHAR (50)      NULL,
    [RepresentativeMiddleName]           NVARCHAR (50)      NULL,
    [RepresentativeLastName]             NVARCHAR (50)      NULL,
    [RepresentativePhoneNumber]          VARCHAR (20)       NULL,
    [RepresentativeMobileNumber]         VARCHAR (20)       NULL,
    [RepresentativeEmailAddress]         VARCHAR (300)      NULL,
    [EmployeeCensusFileId]               UNIQUEIDENTIFIER   NULL,
    [AdminFormFileId]                    UNIQUEIDENTIFIER   NULL,
    [RepresentativeFileId]               UNIQUEIDENTIFIER   NULL,
    [BirDocumentFileId]                  UNIQUEIDENTIFIER   NULL,
    [BusinessRegistrationDocumentFileId] UNIQUEIDENTIFIER   NULL,
    [IncorporationDocumentFileId]        UNIQUEIDENTIFIER   NULL,
    [AuthorizationDocumentFileId]        UNIQUEIDENTIFIER   NULL,
    [IndividualApplicationsFileId]       UNIQUEIDENTIFIER   NULL,
    [PaymentProofFileId]                 UNIQUEIDENTIFIER   NULL,
    [EmployeeCensusFile]                 VARCHAR (50)       NULL,
    [AdminFormFile]                      VARCHAR (50)       NULL,
    [RepresentativeFile]                 VARCHAR (50)       NULL,
    [BirDocumentFile]                    VARCHAR (50)       NULL,
    [BusinessRegistrationDocumentFile]   VARCHAR (50)       NULL,
    [IncorporationDocumentFile]          VARCHAR (50)       NULL,
    [AuthorizationDocumentFile]          VARCHAR (50)       NULL,
    [IndividualApplicationsFile]         VARCHAR (50)       NULL,
    [PaymentProofFile]                   VARCHAR (50)       NULL,
    [FeedbackRating]                     INT                NULL,
    [FeedbackMessage]                    NVARCHAR (2000)    NULL,
    [CancellationReason]                 NVARCHAR (2000)    NULL,
    [CancellationComments]               NVARCHAR (2000)    NULL,
    [Otp]                                VARCHAR (10)       NULL,
    [OtpExpiration]                      DATETIMEOFFSET (7) NULL,
    [Session]                            VARCHAR (300)      NULL,
    [SessionExpiration]                  DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_Group_Applications] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Group_Applications] UNIQUE NONCLUSTERED ([ReferenceCode] ASC)
);










GO
CREATE CLUSTERED INDEX [CI_Group_Applications]
    ON [Group].[Applications]([CreatedDate] ASC);

