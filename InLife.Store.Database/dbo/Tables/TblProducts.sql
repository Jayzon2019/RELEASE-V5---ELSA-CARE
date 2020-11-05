CREATE TABLE [dbo].[TblProducts] (
    [Id]               INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]      DATETIMEOFFSET (7) CONSTRAINT [DF_TblProducts_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]        VARCHAR (36)       NULL,
    [UpdatedDate]      DATETIMEOFFSET (7) NULL,
    [UpdatedBy]        VARCHAR (36)       NULL,
    [ProductImg]       NVARCHAR (MAX)     CONSTRAINT [DF_TblProducts_ProductImg] DEFAULT ('') NOT NULL,
    [ProductName]      NVARCHAR (200)     CONSTRAINT [DF_TblProducts_ProductName] DEFAULT ('') NOT NULL,
    [ProductPrice]     NVARCHAR (100)     CONSTRAINT [DF_TblProducts_ProductPrice] DEFAULT ('') NOT NULL,
    [ProductCode]      NVARCHAR (100)     CONSTRAINT [DF_TblProducts_ProductCode] DEFAULT ('') NULL,
    [IsActive]         BIT                CONSTRAINT [DF_TblProducts_IsActive] DEFAULT ((0)) NOT NULL,
    [IsArchived]       BIT                CONSTRAINT [DF_TblProducts_IsArchived] DEFAULT ((0)) NOT NULL,
    [ShortDescription] NVARCHAR (1000)    NULL,
    [PriceWithOffer]   NVARCHAR (100)     NULL,
    [SortNum]          INT                NULL,
    CONSTRAINT [PK_TblProducts] PRIMARY KEY CLUSTERED ([Id] ASC)
);





