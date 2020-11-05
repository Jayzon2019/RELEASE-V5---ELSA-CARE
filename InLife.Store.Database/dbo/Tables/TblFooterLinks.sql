CREATE TABLE [dbo].[TblFooterLinks] (
    [Id]                INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]       DATETIMEOFFSET (7) CONSTRAINT [DF_TblFooterLinks_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]         VARCHAR (36)       NULL,
    [UpdatedDate]       DATETIMEOFFSET (7) NULL,
    [UpdatedBy]         VARCHAR (36)       NULL,
    [LogoUrl]           NVARCHAR (500)     NULL,
    [MainSiteUrl]       NVARCHAR (500)     NULL,
    [InsCommissionUrl]  NVARCHAR (500)     NULL,
    [CusCharterUrl]     NVARCHAR (500)     NULL,
    [TermsConditionUrl] NVARCHAR (500)     NULL,
    [PrivacyPolicyUrl]  NVARCHAR (500)     NULL,
    [ContactUsUrl]      NVARCHAR (500)     NULL,
    [FbUrl]             NVARCHAR (500)     NULL,
    [IsActive]          BIT                NULL,
    [IsArchived]        BIT                NULL,
    [TweeterUrl]        NVARCHAR (500)     NULL,
    [InstaUrl]          NVARCHAR (500)     NULL,
    [YouTubeUrl]        NVARCHAR (500)     NULL,
    [MainSiteTxt]       NVARCHAR (500)     NULL,
    [InsCommissionTxt]  NVARCHAR (500)     NULL,
    [CusCharterTxt]     NVARCHAR (500)     NULL,
    [TermsConditionTxt] NVARCHAR (500)     NULL,
    [PrivacyPolicyTxt]  NVARCHAR (500)     NULL,
    [ContactUsTxt]      NVARCHAR (500)     NULL,
    CONSTRAINT [PK_TblFooterLinks] PRIMARY KEY CLUSTERED ([Id] ASC)
);





