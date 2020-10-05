IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_ActivationDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_ActivationCode]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_RestDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_RestCode]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_IsArchive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_CreatedDate_1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_Password]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_Email]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_UserRoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_FirstName]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUsers] DROP CONSTRAINT IF EXISTS [DF_TblUsers_GenderId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblUserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[TblUserRoles] DROP CONSTRAINT IF EXISTS [DF_TblUserRoles_UserRole]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_IsArchived]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_CreatedDate_1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_ProductCode]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_ProductPrice]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_ProductName]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProducts]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProducts] DROP CONSTRAINT IF EXISTS [DF_TblProducts_ProductImg]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProductDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProductDetails] DROP CONSTRAINT IF EXISTS [DF_TblProductDetails_IsArchived]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProductDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProductDetails] DROP CONSTRAINT IF EXISTS [DF_TblProductDetails_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblProductDetails]') AND type in (N'U'))
ALTER TABLE [dbo].[TblProductDetails] DROP CONSTRAINT IF EXISTS [DF_TblProductDetails_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeHeroes] DROP CONSTRAINT IF EXISTS [DF_TblPrimeHero_IsArchive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeHeroes] DROP CONSTRAINT IF EXISTS [DF_TblPrimeHero_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeHeroes] DROP CONSTRAINT IF EXISTS [DF_TblPrimeHero_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeHeroes] DROP CONSTRAINT IF EXISTS [DF_TblPrimeHero_PrimeHeroBtnTxt]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeHeroes] DROP CONSTRAINT IF EXISTS [DF_TblPrimeHero_PrimeHeroTitle]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeHeroes] DROP CONSTRAINT IF EXISTS [DF_TblPrimeHero_PrimeHeroBg]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeCare]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeCare] DROP CONSTRAINT IF EXISTS [DF_TblPrimeCare_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeCare]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeCare] DROP CONSTRAINT IF EXISTS [DF_TblPrimeCare_CreatedBy]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeCare]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeCare] DROP CONSTRAINT IF EXISTS [DF_TblPrimeCare_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblPrimeCare]') AND type in (N'U'))
ALTER TABLE [dbo].[TblPrimeCare] DROP CONSTRAINT IF EXISTS [DF_TblPrimeCare_PrimeCareFile]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_IsArchived]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_BtnTxtLink]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_HeroBtnTxt]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_HeroTitle]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblHeroes]') AND type in (N'U'))
ALTER TABLE [dbo].[TblHeroes] DROP CONSTRAINT IF EXISTS [DF_TblHero_HeroBg]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFooterLinks]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFooterLinks] DROP CONSTRAINT IF EXISTS [DF_TblFooterLinks_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqs] DROP CONSTRAINT IF EXISTS [DF_TblFaq_IsArchived]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqs] DROP CONSTRAINT IF EXISTS [DF_TblFaq_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqs] DROP CONSTRAINT IF EXISTS [DF_TblFaq_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqs] DROP CONSTRAINT IF EXISTS [DF_TblFaq_FaqAnswer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqs] DROP CONSTRAINT IF EXISTS [DF_TblFaq_FaqQuestion]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqs] DROP CONSTRAINT IF EXISTS [DF_TblFaq_FaqCatId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_IsArchived]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_FaqCategory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblExceptionLogs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblExceptionLogs] DROP CONSTRAINT IF EXISTS [DF_TblExceptionLogs_ExInner]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblExceptionLogs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblExceptionLogs] DROP CONSTRAINT IF EXISTS [DF_TblExceptionLogs_ExDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblExceptionLogs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblExceptionLogs] DROP CONSTRAINT IF EXISTS [DF_TblExceptionLogs_ExUrl]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblExceptionLogs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblExceptionLogs] DROP CONSTRAINT IF EXISTS [DF_TblExceptionLogs_ExSource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblExceptionLogs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblExceptionLogs] DROP CONSTRAINT IF EXISTS [DF_TblExceptionLogs_ExMsg]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblActivityLogs]') AND type in (N'U'))
ALTER TABLE [dbo].[TblActivityLogs] DROP CONSTRAINT IF EXISTS [DF_TblActivityLogs_ActivityDate]
GO
/****** Object:  Table [dbo].[TblUsers]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblUsers]
GO
/****** Object:  Table [dbo].[TblUserRoles]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblUserRoles]
GO
/****** Object:  Table [dbo].[TblProducts]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblProducts]
GO
/****** Object:  Table [dbo].[TblProductDetails]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblProductDetails]
GO
/****** Object:  Table [dbo].[TblPrimeHeroes]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblPrimeHeroes]
GO
/****** Object:  Table [dbo].[TblPrimeCare]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblPrimeCare]
GO
/****** Object:  Table [dbo].[TblKeyMetrics]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblKeyMetrics]
GO
/****** Object:  Table [dbo].[TblHeroes]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblHeroes]
GO
/****** Object:  Table [dbo].[TblFooterLinks]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFooterLinks]
GO
/****** Object:  Table [dbo].[TblFaqs]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFaqs]
GO
/****** Object:  Table [dbo].[TblFaqCategories]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFaqCategories]
GO
/****** Object:  Table [dbo].[TblExceptionLogs]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblExceptionLogs]
GO
/****** Object:  Table [dbo].[TblEmailCredentials]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblEmailCredentials]
GO
/****** Object:  Table [dbo].[TblActivityLogs]    Script Date: 5 Oct 2020 8:26:39 AM ******/
DROP TABLE IF EXISTS [dbo].[TblActivityLogs]
GO
/****** Object:  Table [dbo].[TblActivityLogs]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblActivityLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityBy] [nvarchar](300) NULL,
	[ActionPerfomed] [nvarchar](200) NULL,
	[ActivityDescription] [nvarchar](500) NULL,
	[IpAddress] [nvarchar](100) NULL,
	[ActivityDate] [datetimeoffset](7) NULL,
	[ActivityById] [varchar](36) NULL,
 CONSTRAINT [PK_TblActivityLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblEmailCredentials]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblEmailCredentials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](300) NULL,
	[Password] [nvarchar](300) NULL,
	[Smtp] [nvarchar](200) NULL,
	[port] [int] NULL,
	[IsBodyHtml] [bit] NULL,
	[EnableSSL] [bit] NULL,
 CONSTRAINT [PK_TblEmailCredentials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblExceptionLogs]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblExceptionLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExMsg] [nvarchar](500) NOT NULL,
	[ExSource] [nvarchar](max) NOT NULL,
	[ExUrl] [nvarchar](200) NOT NULL,
	[ExDate] [datetimeoffset](7) NOT NULL,
	[ExInner] [nvarchar](max) NULL,
 CONSTRAINT [PK_TblExceptionLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFaqCategories]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFaqCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK_TblFaqCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFaqs]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFaqs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Question] [nvarchar](300) NOT NULL,
	[Answer] [nvarchar](800) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[SortNum] [int] NULL,
 CONSTRAINT [PK_TblFaq] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFooterLinks]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFooterLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogoUrl] [nvarchar](500) NULL,
	[MainSiteUrl] [nvarchar](500) NULL,
	[InsCommissionUrl] [nvarchar](500) NULL,
	[CusCharterUrl] [nvarchar](500) NULL,
	[TermsConditionUrl] [nvarchar](500) NULL,
	[PrivacyPolicyUrl] [nvarchar](500) NULL,
	[ContactUsUrl] [nvarchar](500) NULL,
	[FbUrl] [nvarchar](500) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NULL,
	[IsArchived] [bit] NULL,
	[TweeterUrl] [nvarchar](500) NULL,
	[InstaUrl] [nvarchar](500) NULL,
	[YouTubeUrl] [nvarchar](500) NULL,
	[MainSiteTxt] [nvarchar](500) NULL,
	[InsCommissionTxt] [nvarchar](500) NULL,
	[CusCharterTxt] [nvarchar](500) NULL,
	[TermsConditionTxt] [nvarchar](500) NULL,
	[PrivacyPolicyTxt] [nvarchar](500) NULL,
	[ContactUsTxt] [nvarchar](500) NULL,
 CONSTRAINT [PK_TblFooterLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblHeroes]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblHeroes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HeroBg] [nvarchar](max) NOT NULL,
	[HeroTitle] [nvarchar](120) NOT NULL,
	[HeroBtnTxt] [nvarchar](30) NOT NULL,
	[BtnTxtLink] [nvarchar](300) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[Heading] [nvarchar](500) NULL,
	[SubHeading] [nvarchar](500) NULL,
	[HeroMobBg] [nvarchar](max) NULL,
	[HeadingColor] [nvarchar](100) NULL,
	[SubHeadingColor] [nvarchar](100) NULL,
	[ContentPostion] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblHero] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblKeyMetrics]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblKeyMetrics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](300) NULL,
	[PageViews] [int] NULL,
	[Sessions] [nvarchar](300) NULL,
	[PageViewedAt] [datetimeoffset](7) NULL,
	[PageLeftAt] [datetimeoffset](7) NULL,
	[MachineName] [nvarchar](300) NULL,
	[Ip] [nvarchar](300) NULL,
 CONSTRAINT [PK_TblKeyMetrics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblPrimeCare]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPrimeCare](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PrimeCareFile] [nvarchar](max) NOT NULL,
	[PrimeCareFileDescription] [nvarchar](500) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[PrimeCareFileName] [nvarchar](200) NULL,
 CONSTRAINT [PK_TblPrimeCare] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblPrimeHeroes]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPrimeHeroes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PrimeHeroBg] [nvarchar](max) NOT NULL,
	[PrimeHeroTitle] [nvarchar](120) NOT NULL,
	[PrimeHeroBtnTxt] [nvarchar](60) NOT NULL,
	[BtnTxtLink] [nvarchar](300) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[Heading] [nvarchar](500) NULL,
	[SubHeading] [nvarchar](500) NULL,
	[PrimeHeroMobBg] [nvarchar](max) NULL,
	[HeadingColor] [nvarchar](100) NULL,
	[SubHeadingColor] [nvarchar](100) NULL,
	[ContentPostion] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblPrimeHero] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblProductDetails]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblProductDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CasesCovered] [nvarchar](500) NULL,
	[BenefitType] [nvarchar](500) NULL,
	[AgeEligibility] [nvarchar](500) NULL,
	[NumberOfAvailments] [nvarchar](500) NULL,
	[BenefitLimit] [nvarchar](500) NULL,
	[DocProFee] [nvarchar](500) NULL,
	[RoomAccommodation] [nvarchar](500) NULL,
	[LaboratoryDiagnosticPro] [nvarchar](500) NULL,
	[MedicinesAsMedicallyNeeded] [nvarchar](500) NULL,
	[UseOfOperationRoom] [nvarchar](500) NULL,
	[SurgerySurgonFees] [nvarchar](500) NULL,
	[Laparoscopic] [nvarchar](500) NULL,
	[MRA] [nvarchar](500) NULL,
	[MRI] [nvarchar](500) NULL,
	[CT] [nvarchar](500) NULL,
	[Therapetic] [nvarchar](500) NULL,
	[PainManagement] [nvarchar](500) NULL,
	[Arthoscopic] [nvarchar](500) NULL,
	[OtherMedical] [nvarchar](500) NULL,
	[OneTime] [nvarchar](500) NULL,
	[Usage] [nvarchar](500) NULL,
	[AccreditedHospitals] [nvarchar](500) NULL,
	[MER] [nvarchar](500) NULL,
	[AFR] [nvarchar](500) NULL,
	[ARP] [nvarchar](500) NULL,
	[Validity] [nvarchar](500) NULL,
	[Waiting] [nvarchar](500) NULL,
	[NumberOfRegistrations] [nvarchar](500) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[UnlimitedTeleMed] [nvarchar](500) NULL,
	[PreExistingConCover] [nvarchar](500) NULL,
	[NonAccreditedHos] [nvarchar](500) NULL,
	[ReimbursementNonAccreditedHos] [nvarchar](500) NULL,
	[TopSixHospitalAccess] [nvarchar](500) NULL,
	[RegistrationOfSucceedingVouchers] [nvarchar](500) NULL,
	[Combinability] [nvarchar](500) NULL,
	[IndividualOrGroup] [nvarchar](500) NULL,
	[PrepaidPlan] [nvarchar](500) NULL,
	[Consultation] [nvarchar](500) NULL,
	[Inclusions] [nvarchar](max) NULL,
	[SpecialModalities] [nvarchar](500) NULL,
	[Exclusions] [nvarchar](max) NULL,
	[FTFConsultation] [nvarchar](500) NULL,
	[Telemedicine] [nvarchar](500) NULL,
	[DentalConsultation] [nvarchar](500) NULL,
	[DentalServicesBenefit] [nvarchar](500) NULL,
	[HospitalNetwork] [nvarchar](500) NULL,
	[RegistrationRules] [nvarchar](500) NULL,
	[MedicalCoverage] [nvarchar](500) NULL,
	[LearnMoreBtnLink] [nvarchar](500) NULL,
	[BuyNowBtnLink] [nvarchar](500) NULL,
	[Coverage] [nvarchar](500) NULL,
	[VoucherUsed] [nvarchar](500) NULL,
	[VoucherUnused] [nvarchar](500) NULL,
	[ConsultationCards] [nvarchar](500) NULL,
	[InPatient] [nvarchar](500) NULL,
	[OutPatient] [nvarchar](500) NULL,
 CONSTRAINT [PK_TblProductDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblProducts]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductImg] [nvarchar](max) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[ProductPrice] [nvarchar](100) NOT NULL,
	[ProductCode] [nvarchar](100) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[ShortDescription] [nvarchar](1000) NULL,
	[PriceWithOffer] [nvarchar](100) NULL,
	[SortNum] [int] NULL,
 CONSTRAINT [PK_TblProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUserRoles]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUserRoles](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserRole] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TblUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUsers]    Script Date: 5 Oct 2020 8:26:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DOB] [datetime] NULL,
	[GenderId] [int] NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NULL,
	[UserRoleId] [int] NOT NULL,
	[Email] [nvarchar](300) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[UserImg] [nvarchar](500) NULL,
	[Phone] [nvarchar](100) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[RestCode] [nvarchar](50) NOT NULL,
	[RestDate] [datetime] NOT NULL,
	[ActivationCode] [nvarchar](50) NOT NULL,
	[ActivationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TblUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TblActivityLogs] ADD  CONSTRAINT [DF_TblActivityLogs_ActivityDate]  DEFAULT (sysdatetimeoffset()) FOR [ActivityDate]
GO
ALTER TABLE [dbo].[TblExceptionLogs] ADD  CONSTRAINT [DF_TblExceptionLogs_ExMsg]  DEFAULT ('') FOR [ExMsg]
GO
ALTER TABLE [dbo].[TblExceptionLogs] ADD  CONSTRAINT [DF_TblExceptionLogs_ExSource]  DEFAULT ('') FOR [ExSource]
GO
ALTER TABLE [dbo].[TblExceptionLogs] ADD  CONSTRAINT [DF_TblExceptionLogs_ExUrl]  DEFAULT ('') FOR [ExUrl]
GO
ALTER TABLE [dbo].[TblExceptionLogs] ADD  CONSTRAINT [DF_TblExceptionLogs_ExDate]  DEFAULT (sysdatetimeoffset()) FOR [ExDate]
GO
ALTER TABLE [dbo].[TblExceptionLogs] ADD  CONSTRAINT [DF_TblExceptionLogs_ExInner]  DEFAULT ('') FOR [ExInner]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_FaqCategory]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_CreatedDate]  DEFAULT ('') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblFaqs] ADD  CONSTRAINT [DF_TblFaq_FaqCatId]  DEFAULT ((0)) FOR [CategoryId]
GO
ALTER TABLE [dbo].[TblFaqs] ADD  CONSTRAINT [DF_TblFaq_FaqQuestion]  DEFAULT ('') FOR [Question]
GO
ALTER TABLE [dbo].[TblFaqs] ADD  CONSTRAINT [DF_TblFaq_FaqAnswer]  DEFAULT ('') FOR [Answer]
GO
ALTER TABLE [dbo].[TblFaqs] ADD  CONSTRAINT [DF_TblFaq_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblFaqs] ADD  CONSTRAINT [DF_TblFaq_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblFaqs] ADD  CONSTRAINT [DF_TblFaq_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblFooterLinks] ADD  CONSTRAINT [DF_TblFooterLinks_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_HeroBg]  DEFAULT ('') FOR [HeroBg]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_HeroTitle]  DEFAULT ('') FOR [HeroTitle]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_HeroBtnTxt]  DEFAULT ('') FOR [HeroBtnTxt]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_BtnTxtLink]  DEFAULT ('') FOR [BtnTxtLink]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblHeroes] ADD  CONSTRAINT [DF_TblHero_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblPrimeCare] ADD  CONSTRAINT [DF_TblPrimeCare_PrimeCareFile]  DEFAULT ('') FOR [PrimeCareFile]
GO
ALTER TABLE [dbo].[TblPrimeCare] ADD  CONSTRAINT [DF_TblPrimeCare_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblPrimeCare] ADD  CONSTRAINT [DF_TblPrimeCare_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[TblPrimeCare] ADD  CONSTRAINT [DF_TblPrimeCare_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblPrimeHeroes] ADD  CONSTRAINT [DF_TblPrimeHero_PrimeHeroBg]  DEFAULT ('') FOR [PrimeHeroBg]
GO
ALTER TABLE [dbo].[TblPrimeHeroes] ADD  CONSTRAINT [DF_TblPrimeHero_PrimeHeroTitle]  DEFAULT ('') FOR [PrimeHeroTitle]
GO
ALTER TABLE [dbo].[TblPrimeHeroes] ADD  CONSTRAINT [DF_TblPrimeHero_PrimeHeroBtnTxt]  DEFAULT ('') FOR [PrimeHeroBtnTxt]
GO
ALTER TABLE [dbo].[TblPrimeHeroes] ADD  CONSTRAINT [DF_TblPrimeHero_CreatedDate]  DEFAULT ('') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblPrimeHeroes] ADD  CONSTRAINT [DF_TblPrimeHero_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblPrimeHeroes] ADD  CONSTRAINT [DF_TblPrimeHero_IsArchive]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblProductDetails] ADD  CONSTRAINT [DF_TblProductDetails_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblProductDetails] ADD  CONSTRAINT [DF_TblProductDetails_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblProductDetails] ADD  CONSTRAINT [DF_TblProductDetails_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_ProductImg]  DEFAULT ('') FOR [ProductImg]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_ProductName]  DEFAULT ('') FOR [ProductName]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_ProductPrice]  DEFAULT ('') FOR [ProductPrice]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_ProductCode]  DEFAULT ('') FOR [ProductCode]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_CreatedDate_1]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblProducts] ADD  CONSTRAINT [DF_TblProducts_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblUserRoles] ADD  CONSTRAINT [DF_TblUserRoles_UserRole]  DEFAULT ('') FOR [UserRole]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_GenderId]  DEFAULT ((0)) FOR [GenderId]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_FirstName]  DEFAULT ('') FOR [FirstName]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_UserRoleId]  DEFAULT ((0)) FOR [UserRoleId]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_Email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_Password]  DEFAULT ('') FOR [Password]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_CreatedDate_1]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_IsArchive]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_RestCode]  DEFAULT ('') FOR [RestCode]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_RestDate]  DEFAULT ('') FOR [RestDate]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_ActivationCode]  DEFAULT ('') FOR [ActivationCode]
GO
ALTER TABLE [dbo].[TblUsers] ADD  CONSTRAINT [DF_TblUsers_ActivationDate]  DEFAULT ('') FOR [ActivationDate]
GO
