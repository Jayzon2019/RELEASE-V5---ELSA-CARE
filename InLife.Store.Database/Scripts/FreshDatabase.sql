IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTokens]') AND type in (N'U'))
ALTER TABLE [dbo].[UserTokens] DROP CONSTRAINT IF EXISTS [FK_UserTokens_Users_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_UserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[Users_UserRoles] DROP CONSTRAINT IF EXISTS [FK_Users_UserRoles_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_UserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[Users_UserRoles] DROP CONSTRAINT IF EXISTS [FK_Users_UserRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoleClaims]') AND type in (N'U'))
ALTER TABLE [dbo].[UserRoleClaims] DROP CONSTRAINT IF EXISTS [FK_UserRoleClaims_UserRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT IF EXISTS [FK_UserLogins_Users_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserClaims]') AND type in (N'U'))
ALTER TABLE [dbo].[UserClaims] DROP CONSTRAINT IF EXISTS [FK_UserClaims_Users_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OtherInsurances]') AND type in (N'U'))
ALTER TABLE [dbo].[OtherInsurances] DROP CONSTRAINT IF EXISTS [FK_OtherInsurances_InsuranceApplications]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsuranceApplications]') AND type in (N'U'))
ALTER TABLE [dbo].[InsuranceApplications] DROP CONSTRAINT IF EXISTS [FK_InsuranceApplications_Persons_Owner]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsuranceApplications]') AND type in (N'U'))
ALTER TABLE [dbo].[InsuranceApplications] DROP CONSTRAINT IF EXISTS [FK_InsuranceApplications_Persons_Insured]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsuranceApplications]') AND type in (N'U'))
ALTER TABLE [dbo].[InsuranceApplications] DROP CONSTRAINT IF EXISTS [FK_InsuranceApplications_Persons_Beneficiary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsuranceApplications]') AND type in (N'U'))
ALTER TABLE [dbo].[InsuranceApplications] DROP CONSTRAINT IF EXISTS [FK_InsuranceApplications_InsuranceApplications]
GO
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
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Quotes]') AND type in (N'U'))
ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT IF EXISTS [DF_Quotes_DateCreated]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT IF EXISTS [DF_Persons_DateCreated]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OtherInsurances]') AND type in (N'U'))
ALTER TABLE [dbo].[OtherInsurances] DROP CONSTRAINT IF EXISTS [DF_OtherInsurances_DateCreated]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsuranceApplications]') AND type in (N'U'))
ALTER TABLE [dbo].[InsuranceApplications] DROP CONSTRAINT IF EXISTS [DF_InsuranceApplications_DateCreated]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
ALTER TABLE [dbo].[Customers] DROP CONSTRAINT IF EXISTS [DF_Customers_DateCreated]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Addresses]') AND type in (N'U'))
ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT IF EXISTS [DF_Addresses_DateCreated]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[UserTokens]
GO
/****** Object:  Table [dbo].[Users_UserRoles]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[Users_UserRoles]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[UserRoleClaims]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[UserRoleClaims]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[UserLogins]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[UserClaims]
GO
/****** Object:  Table [dbo].[TblUsers]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblUsers]
GO
/****** Object:  Table [dbo].[TblUserRoles]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblUserRoles]
GO
/****** Object:  Table [dbo].[TblProducts]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblProducts]
GO
/****** Object:  Table [dbo].[TblProductDetails]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblProductDetails]
GO
/****** Object:  Table [dbo].[TblPrimeHeroes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblPrimeHeroes]
GO
/****** Object:  Table [dbo].[TblPrimeCare]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblPrimeCare]
GO
/****** Object:  Table [dbo].[TblKeyMetrics]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblKeyMetrics]
GO
/****** Object:  Table [dbo].[TblHeroes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblHeroes]
GO
/****** Object:  Table [dbo].[TblFooterLinks]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFooterLinks]
GO
/****** Object:  Table [dbo].[TblFaqs]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFaqs]
GO
/****** Object:  Table [dbo].[TblFaqCategories]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFaqCategories]
GO
/****** Object:  Table [dbo].[TblExceptionLogs]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblExceptionLogs]
GO
/****** Object:  Table [dbo].[TblEmailCredentials]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblEmailCredentials]
GO
/****** Object:  Table [dbo].[TblActivityLogs]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[TblActivityLogs]
GO
/****** Object:  Table [dbo].[Quotes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[Quotes]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[Persons]
GO
/****** Object:  Table [dbo].[PersistedGrants]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[PersistedGrants]
GO
/****** Object:  Table [dbo].[OtherInsurances]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[OtherInsurances]
GO
/****** Object:  Table [dbo].[InsuranceApplications]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[InsuranceApplications]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[DeviceCodes]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[Customers]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 5 Oct 2020 8:20:07 AM ******/
DROP TABLE IF EXISTS [dbo].[Addresses]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](30) NULL,
	[Region] [nvarchar](30) NULL,
	[Country] [nvarchar](30) NULL,
	[ZipCode] [varchar](5) NULL,
	[DateCreated] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamePrefix] [nvarchar](10) NULL,
	[NameSuffix] [nvarchar](10) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Nationality] [nvarchar](50) NULL,
	[CivilStatus] [varchar](20) NULL,
	[Gender] [varchar](20) NULL,
	[BirthDate] [smalldatetime] NULL,
	[BirthCountry] [nvarchar](30) NULL,
	[BirthRegion] [nvarchar](30) NULL,
	[BirthCity] [nvarchar](30) NULL,
	[EmailAddress] [varchar](300) NULL,
	[MobileNumber] [varchar](20) NULL,
	[HomeAddressId] [int] NULL,
	[WorkAddressId] [int] NULL,
	[DateCreated] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceCodes](
	[UserCode] [nvarchar](200) NOT NULL,
	[DeviceCode] [nvarchar](200) NOT NULL,
	[SubjectId] [nvarchar](200) NULL,
	[SessionId] [nvarchar](100) NULL,
	[ClientId] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreationTime] [datetime2](7) NOT NULL,
	[Expiration] [datetime2](7) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DeviceCodes] PRIMARY KEY CLUSTERED 
(
	[UserCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InsuranceApplications]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsuranceApplications](
	[ClusteringId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetimeoffset](7) NOT NULL,
	[PlanCode] [varchar](20) NOT NULL,
	[PlanName] [nvarchar](30) NOT NULL,
	[PlanPaymentMode] [varchar](20) NOT NULL,
	[PlanFaceAmount] [decimal](19, 4) NOT NULL,
	[PlanPremium] [decimal](19, 4) NOT NULL,
	[AgentFirstName] [nvarchar](50) NULL,
	[AgentLastName] [nvarchar](50) NULL,
	[ReferralSource] [nvarchar](50) NULL,
	[Heallth1] [varchar](10) NULL,
	[Heallth2] [varchar](10) NULL,
	[Heallth3] [varchar](10) NULL,
	[Fatca1] [varchar](10) NULL,
	[Fatca2] [varchar](10) NULL,
	[Question1] [varchar](10) NULL,
	[Question2] [varchar](10) NULL,
	[PolicyDeliveryOption] [varchar](20) NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[InsuredId] [uniqueidentifier] NOT NULL,
	[InsuredRelationship] [varchar](20) NULL,
	[BeneficiaryId] [uniqueidentifier] NOT NULL,
	[BeneficiaryRelationship] [varchar](20) NULL,
	[BeneficiaryRight] [varchar](20) NULL,
	[BeneficiaryPriority] [varchar](20) NULL,
 CONSTRAINT [PK_InsuranceApplications] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OtherInsurances]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherInsurances](
	[ClusteringId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetimeoffset](7) NOT NULL,
	[InsuranceApplicationId] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[BasicFaceAmount] [decimal](19, 4) NULL,
	[DreadDiseaseFaceAmount] [decimal](19, 4) NULL,
	[AccidentalFaceAmount] [decimal](19, 4) NULL,
	[IssueYear] [int] NULL,
 CONSTRAINT [PK_OtherInsurances] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersistedGrants]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersistedGrants](
	[Key] [nvarchar](200) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[SubjectId] [nvarchar](200) NULL,
	[SessionId] [nvarchar](100) NULL,
	[ClientId] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreationTime] [datetime2](7) NOT NULL,
	[Expiration] [datetime2](7) NULL,
	[ConsumedTime] [datetime2](7) NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PersistedGrants] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[ClusteringId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetimeoffset](7) NOT NULL,
	[NamePrefix] [nvarchar](10) NULL,
	[NameSuffix] [nvarchar](10) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Nationality] [nvarchar](50) NOT NULL,
	[CivilStatus] [varchar](20) NOT NULL,
	[Gender] [varchar](20) NOT NULL,
	[BirthDate] [smalldatetime] NULL,
	[BirthCountry] [nvarchar](30) NULL,
	[BirthRegion] [nvarchar](30) NULL,
	[BirthCity] [nvarchar](30) NULL,
	[EmailAddress] [varchar](320) NULL,
	[MobileNumber] [varchar](20) NULL,
	[HomePhoneNumber] [varchar](20) NULL,
	[HomeAddress1] [nvarchar](100) NULL,
	[HomeAddress2] [nvarchar](100) NULL,
	[HomeAddress3] [nvarchar](100) NULL,
	[HomeCity] [nvarchar](30) NULL,
	[HomeRegion] [nvarchar](30) NULL,
	[HomeZipCode] [nvarchar](10) NULL,
	[HomeCountry] [nvarchar](30) NULL,
	[WorkPhoneNumber] [varchar](20) NULL,
	[WorkAddress1] [nvarchar](100) NULL,
	[WorkAddress2] [nvarchar](100) NULL,
	[WorkAddress3] [nvarchar](100) NULL,
	[WorkCity] [nvarchar](30) NULL,
	[WorkRegion] [nvarchar](30) NULL,
	[WorkZipCode] [nvarchar](10) NULL,
	[WorkHomeCountry] [nvarchar](30) NULL,
	[PreferredMailingAddress] [varchar](10) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[Occupation] [nvarchar](50) NULL,
	[IncomeMonthlyAmount] [decimal](19, 4) NULL,
	[IncomeSource] [nvarchar](50) NULL,
	[LegalIdType] [varchar](30) NULL,
	[LegalIdNumber] [varchar](30) NULL,
	[LegalIdImage] [varchar](max) NULL,
	[SecondaryLegalIdType] [varchar](30) NULL,
	[SecondaryLegalIdNumber] [varchar](30) NULL,
	[SecondaryLegalIdImage] [varchar](max) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ProductCode] [varchar](10) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductFaceAmount] [decimal](19, 4) NULL,
	[PaymentMode] [int] NULL,
	[AgentFirstName] [nvarchar](50) NULL,
	[AgentLastName] [nvarchar](50) NULL,
	[ReferralSource] [nvarchar](80) NULL,
	[Health1] [bit] NULL,
	[Health2] [bit] NULL,
	[Health3] [bit] NULL,
	[IsEligible] [bit] NULL,
	[DateCreated] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Quotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblActivityLogs]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblEmailCredentials]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblExceptionLogs]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblFaqCategories]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblFaqs]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblFooterLinks]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblHeroes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblKeyMetrics]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblPrimeCare]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblPrimeHeroes]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblProductDetails]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblProducts]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblUserRoles]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[TblUsers]    Script Date: 5 Oct 2020 8:20:07 AM ******/
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
/****** Object:  Table [dbo].[UserClaims]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[LoginProvider] [varchar](128) NOT NULL,
	[ProviderKey] [varchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [varchar](36) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY NONCLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleClaims]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [varchar](36) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [varchar](36) NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[DateActivated] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_UserRoles]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_UserRoles](
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[RoleId] [varchar](36) NOT NULL,
 CONSTRAINT [PK_Users_UserRoles] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 5 Oct 2020 8:20:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[LoginProvider] [varchar](128) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [DF_Addresses_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[InsuranceApplications] ADD  CONSTRAINT [DF_InsuranceApplications_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[OtherInsurances] ADD  CONSTRAINT [DF_OtherInsurances_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Quotes] ADD  CONSTRAINT [DF_Quotes_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
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
ALTER TABLE [dbo].[InsuranceApplications]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceApplications_InsuranceApplications] FOREIGN KEY([Id])
REFERENCES [dbo].[InsuranceApplications] ([Id])
GO
ALTER TABLE [dbo].[InsuranceApplications] CHECK CONSTRAINT [FK_InsuranceApplications_InsuranceApplications]
GO
ALTER TABLE [dbo].[InsuranceApplications]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceApplications_Persons_Beneficiary] FOREIGN KEY([BeneficiaryId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[InsuranceApplications] CHECK CONSTRAINT [FK_InsuranceApplications_Persons_Beneficiary]
GO
ALTER TABLE [dbo].[InsuranceApplications]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceApplications_Persons_Insured] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[InsuranceApplications] CHECK CONSTRAINT [FK_InsuranceApplications_Persons_Insured]
GO
ALTER TABLE [dbo].[InsuranceApplications]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceApplications_Persons_Owner] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[InsuranceApplications] CHECK CONSTRAINT [FK_InsuranceApplications_Persons_Owner]
GO
ALTER TABLE [dbo].[OtherInsurances]  WITH CHECK ADD  CONSTRAINT [FK_OtherInsurances_InsuranceApplications] FOREIGN KEY([InsuranceApplicationId])
REFERENCES [dbo].[InsuranceApplications] ([Id])
GO
ALTER TABLE [dbo].[OtherInsurances] CHECK CONSTRAINT [FK_OtherInsurances_InsuranceApplications]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleClaims_UserRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleClaims] CHECK CONSTRAINT [FK_UserRoleClaims_UserRoles_RoleId]
GO
ALTER TABLE [dbo].[Users_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users_UserRoles] CHECK CONSTRAINT [FK_Users_UserRoles_RoleId]
GO
ALTER TABLE [dbo].[Users_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoles_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users_UserRoles] CHECK CONSTRAINT [FK_Users_UserRoles_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
