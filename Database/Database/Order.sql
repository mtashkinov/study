CREATE TABLE [dbo].[Order]
(
    [Number] INT IDENTITY (1,1) NOT NULL, 
    [Data] DATETIME NOT NULL, 
    [Name] NCHAR(50) NOT NULL, 
    [Weight] INT NOT NULL, 
    [Volume] INT NOT NULL, 
    [Cost] INT NOT NULL, 
    [CustomerId] INT NOT NULL, 
    PRIMARY KEY ([Number]), 
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
