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
/****** Object:  Index [PK_UserTokens]    Script Date: 12 Oct 2020 12:55:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTokens]') AND type in (N'U'))
ALTER TABLE [dbo].[UserTokens] DROP CONSTRAINT IF EXISTS [PK_UserTokens]
GO
/****** Object:  Index [PK_Users_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_UserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[Users_UserRoles] DROP CONSTRAINT IF EXISTS [PK_Users_UserRoles]
GO
/****** Object:  Index [PK_Users]    Script Date: 12 Oct 2020 12:55:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT IF EXISTS [PK_Users]
GO
/****** Object:  Index [PK_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT IF EXISTS [PK_UserRoles]
GO
/****** Object:  Index [PK_UserLogins]    Script Date: 12 Oct 2020 12:55:59 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT IF EXISTS [PK_UserLogins]
GO
/****** Object:  Index [IX_PersistedGrants_SubjectId_SessionId_Type]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [IX_PersistedGrants_SubjectId_SessionId_Type] ON [dbo].[PersistedGrants]
GO
/****** Object:  Index [IX_PersistedGrants_SubjectId_ClientId_Type]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [IX_PersistedGrants_SubjectId_ClientId_Type] ON [dbo].[PersistedGrants]
GO
/****** Object:  Index [IX_PersistedGrants_Expiration]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [IX_PersistedGrants_Expiration] ON [dbo].[PersistedGrants]
GO
/****** Object:  Index [IX_DeviceCodes_Expiration]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [IX_DeviceCodes_Expiration] ON [dbo].[DeviceCodes]
GO
/****** Object:  Index [IX_DeviceCodes_DeviceCode]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [IX_DeviceCodes_DeviceCode] ON [dbo].[DeviceCodes]
GO
/****** Object:  Index [CI_UserTokens]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [CI_UserTokens] ON [dbo].[UserTokens] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[UserTokens]
GO
/****** Object:  Index [CI_Users_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [CI_Users_UserRoles] ON [dbo].[Users_UserRoles] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Users_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[Users_UserRoles]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[UserRoleClaims]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[UserRoleClaims]
GO
/****** Object:  Index [CI_UserLogins]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP INDEX IF EXISTS [CI_UserLogins] ON [dbo].[UserLogins] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[UserLogins]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[UserClaims]
GO
/****** Object:  Table [dbo].[PersistedGrants]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[PersistedGrants]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 12 Oct 2020 12:55:59 PM ******/
DROP TABLE IF EXISTS [dbo].[DeviceCodes]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Table [dbo].[PersistedGrants]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Table [dbo].[UserClaims]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Table [dbo].[UserLogins]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Index [CI_UserLogins]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE CLUSTERED INDEX [CI_UserLogins] ON [dbo].[UserLogins]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleClaims]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Table [dbo].[Users_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Index [CI_Users_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE CLUSTERED INDEX [CI_Users_UserRoles] ON [dbo].[Users_UserRoles]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 12 Oct 2020 12:55:59 PM ******/
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
/****** Object:  Index [CI_UserTokens]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE CLUSTERED INDEX [CI_UserTokens] ON [dbo].[UserTokens]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
INSERT [dbo].[UserRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Admin', N'Administrator', N'ADMINISTRATOR', NULL)
GO
INSERT [dbo].[UserRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ContentManager', N'Content Manager', N'CONTENT MANAGER', NULL)
GO
INSERT [dbo].[UserRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Agent', N'Agent', N'AGENT', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DeviceCodes_DeviceCode]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_DeviceCodes_DeviceCode] ON [dbo].[DeviceCodes]
(
	[DeviceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeviceCodes_Expiration]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_DeviceCodes_Expiration] ON [dbo].[DeviceCodes]
(
	[Expiration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersistedGrants_Expiration]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_PersistedGrants_Expiration] ON [dbo].[PersistedGrants]
(
	[Expiration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PersistedGrants_SubjectId_ClientId_Type]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_PersistedGrants_SubjectId_ClientId_Type] ON [dbo].[PersistedGrants]
(
	[SubjectId] ASC,
	[ClientId] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PersistedGrants_SubjectId_SessionId_Type]    Script Date: 12 Oct 2020 12:55:59 PM ******/
CREATE NONCLUSTERED INDEX [IX_PersistedGrants_SubjectId_SessionId_Type] ON [dbo].[PersistedGrants]
(
	[SubjectId] ASC,
	[SessionId] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_UserLogins]    Script Date: 12 Oct 2020 12:55:59 PM ******/
ALTER TABLE [dbo].[UserLogins] ADD  CONSTRAINT [PK_UserLogins] PRIMARY KEY NONCLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [PK_UserRoles] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_Users]    Script Date: 12 Oct 2020 12:55:59 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_Users_UserRoles]    Script Date: 12 Oct 2020 12:55:59 PM ******/
ALTER TABLE [dbo].[Users_UserRoles] ADD  CONSTRAINT [PK_Users_UserRoles] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_UserTokens]    Script Date: 12 Oct 2020 12:55:59 PM ******/
ALTER TABLE [dbo].[UserTokens] ADD  CONSTRAINT [PK_UserTokens] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
