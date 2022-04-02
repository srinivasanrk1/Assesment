SET ANSI_NULLS ON
GO
USE InventoryDB

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category] (
    [Id]      UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    [CategoryName] VARCHAR (100)    NOT NULL
);

 


 SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product] (
    [Id]           UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
    [Name]        VARCHAR (MAX)    NOT NULL,
    [BarCode]     NVARCHAR (100)   NOT NULL,
    [Description] VARCHAR (MAX)    NOT NULL,
    [Weight]      DECIMAL (18, 2)  NOT NULL,
    [Status]      NVARCHAR (50)    NOT NULL,
    [CategoryId]  UNIQUEIDENTIFIER NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Product_CategoryId]
    ON [dbo].[Product]([CategoryId] ASC);



GO
ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [FK_Category_Id] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]);