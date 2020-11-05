CREATE TABLE [dbo].[TblHeroes] (
    [Id]              INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]     DATETIMEOFFSET (7) CONSTRAINT [DF_TblHero_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]       VARCHAR (36)       NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       VARCHAR (36)       NULL,
    [HeroBg]          NVARCHAR (MAX)     CONSTRAINT [DF_TblHero_HeroBg] DEFAULT ('') NOT NULL,
    [HeroTitle]       NVARCHAR (120)     CONSTRAINT [DF_TblHero_HeroTitle] DEFAULT ('') NOT NULL,
    [HeroBtnTxt]      NVARCHAR (30)      CONSTRAINT [DF_TblHero_HeroBtnTxt] DEFAULT ('') NOT NULL,
    [BtnTxtLink]      NVARCHAR (300)     CONSTRAINT [DF_TblHero_BtnTxtLink] DEFAULT ('') NOT NULL,
    [IsActive]        BIT                CONSTRAINT [DF_TblHero_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]      BIT                CONSTRAINT [DF_TblHero_IsArchived] DEFAULT ((0)) NOT NULL,
    [Heading]         NVARCHAR (500)     NULL,
    [SubHeading]      NVARCHAR (500)     NULL,
    [HeroMobBg]       NVARCHAR (MAX)     NULL,
    [HeadingColor]    NVARCHAR (100)     NULL,
    [SubHeadingColor] NVARCHAR (100)     NULL,
    [ContentPostion]  NVARCHAR (50)      NULL,
    CONSTRAINT [PK_TblHero] PRIMARY KEY CLUSTERED ([Id] ASC)
);

