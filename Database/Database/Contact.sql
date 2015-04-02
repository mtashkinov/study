CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [Phone] NCHAR(20) NULL, 
    [Fax] NCHAR(20) NULL, 
    [Email] NCHAR(20) NOT NULL, 
    [Adress] NCHAR(50) NULL, 
    [CustomerId] INT NOT NULL, 
    CONSTRAINT [FK_Contact_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
