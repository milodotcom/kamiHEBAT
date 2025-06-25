CREATE TABLE [dbo].[Customers] (
    [CustomerID]   INT            IDENTITY (1, 1) NOT NULL,
    [CustomerName] NVARCHAR (100) NOT NULL,
    [PhoneNumber]  VARCHAR (20)   NULL,
    [Address]      NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);