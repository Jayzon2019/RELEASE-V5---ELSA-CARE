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
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT IF EXISTS [DF_Persons_DateCreated]
GO
/****** Object:  Index [PK_UserTokens]    Script Date: 5 Oct 2020 8:24:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTokens]') AND type in (N'U'))
ALTER TABLE [dbo].[UserTokens] DROP CONSTRAINT IF EXISTS [PK_UserTokens]
GO
/****** Object:  Index [PK_Users_UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_UserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[Users_UserRoles] DROP CONSTRAINT IF EXISTS [PK_Users_UserRoles]
GO
/****** Object:  Index [PK_Users]    Script Date: 5 Oct 2020 8:24:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT IF EXISTS [PK_Users]
GO
/****** Object:  Index [PK_UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT IF EXISTS [PK_UserRoles]
GO
/****** Object:  Index [PK_UserLogins]    Script Date: 5 Oct 2020 8:24:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT IF EXISTS [PK_UserLogins]
GO
/****** Object:  Index [PK_Persons]    Script Date: 5 Oct 2020 8:24:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT IF EXISTS [PK_Persons]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[UserTokens]
GO
/****** Object:  Table [dbo].[Users_UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[Users_UserRoles]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[UserRoleClaims]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[UserRoleClaims]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[UserLogins]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[UserClaims]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[Persons]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 5 Oct 2020 8:24:57 AM ******/
DROP TABLE IF EXISTS [dbo].[DeviceCodes]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 5 Oct 2020 8:24:57 AM ******/
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
/****** Object:  Table [dbo].[Persons]    Script Date: 5 Oct 2020 8:24:57 AM ******/
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
	[SecondaryLegalIdImage] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 5 Oct 2020 8:24:57 AM ******/
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
/****** Object:  Table [dbo].[UserLogins]    Script Date: 5 Oct 2020 8:24:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[LoginProvider] [varchar](128) NOT NULL,
	[ProviderKey] [varchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [varchar](36) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleClaims]    Script Date: 5 Oct 2020 8:24:57 AM ******/
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5 Oct 2020 8:24:57 AM ******/
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
	[DateActivated] [datetimeoffset](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_UserRoles](
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[RoleId] [varchar](36) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 5 Oct 2020 8:24:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[LoginProvider] [varchar](128) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[UserRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Admin', N'Administrator', N'ADMINISTRATOR', NULL)
GO
INSERT [dbo].[UserRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ContentManager', N'Content Manager', N'CONTENT MANAGER', NULL)
GO
INSERT [dbo].[UserRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Agent', N'Agent', N'AGENT', NULL)
GO
/****** Object:  Index [PK_Persons]    Script Date: 5 Oct 2020 8:24:57 AM ******/
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [PK_Persons] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_UserLogins]    Script Date: 5 Oct 2020 8:24:57 AM ******/
ALTER TABLE [dbo].[UserLogins] ADD  CONSTRAINT [PK_UserLogins] PRIMARY KEY NONCLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [PK_UserRoles] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_Users]    Script Date: 5 Oct 2020 8:24:57 AM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_Users_UserRoles]    Script Date: 5 Oct 2020 8:24:57 AM ******/
ALTER TABLE [dbo].[Users_UserRoles] ADD  CONSTRAINT [PK_Users_UserRoles] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_UserTokens]    Script Date: 5 Oct 2020 8:24:57 AM ******/
ALTER TABLE [dbo].[UserTokens] ADD  CONSTRAINT [PK_UserTokens] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_DateCreated]  DEFAULT (sysdatetimeoffset()) FOR [DateCreated]
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
