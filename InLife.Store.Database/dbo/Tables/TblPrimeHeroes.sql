CREATE TABLE [dbo].[TblPrimeHeroes] (
    [Id]              INT                IDENTITY (1, 1) NOT NULL,
    [PrimeHeroBg]     NVARCHAR (MAX)     CONSTRAINT [DF_TblPrimeHero_PrimeHeroBg] DEFAULT ('') NOT NULL,
    [PrimeHeroTitle]  NVARCHAR (120)     CONSTRAINT [DF_TblPrimeHero_PrimeHeroTitle] DEFAULT ('') NOT NULL,
    [PrimeHeroBtnTxt] NVARCHAR (60)      CONSTRAINT [DF_TblPrimeHero_PrimeHeroBtnTxt] DEFAULT ('') NOT NULL,
    [BtnTxtLink]      NVARCHAR (300)     NOT NULL,
    [CreatedDate]     DATETIMEOFFSET (7) CONSTRAINT [DF_TblPrimeHero_CreatedDate] DEFAULT ('') NOT NULL,
    [CreatedBy]       VARCHAR (36)       NULL,
    [UpdatedDate]     DATETIMEOFFSET (7) NULL,
    [UpdatedBy]       VARCHAR (36)       NULL,
    [IsActive]        BIT                CONSTRAINT [DF_TblPrimeHero_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]      BIT                CONSTRAINT [DF_TblPrimeHero_IsArchive] DEFAULT ((0)) NOT NULL,
    [Heading]         NVARCHAR (500)     NULL,
    [SubHeading]      NVARCHAR (500)     NULL,
    [PrimeHeroMobBg]  NVARCHAR (MAX)     NULL,
    [HeadingColor]    NVARCHAR (100)     NULL,
    [SubHeadingColor] NVARCHAR (100)     NULL,
    [ContentPostion]  NVARCHAR (50)      NULL,
    CONSTRAINT [PK_TblPrimeHero] PRIMARY KEY CLUSTERED ([Id] ASC)
);

