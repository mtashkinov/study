CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL, 
    [Adress] NCHAR(50) NULL, 
    [Сharacteristic] NCHAR(100) NULL
)
