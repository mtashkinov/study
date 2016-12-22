CREATE TABLE [dbo].[Films]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Country] NVARCHAR(50) NULL, 
    [Year] NVARCHAR(50) NOT NULL, 
    [DirectorId] INT NOT NULL, 
    [Image] NVARCHAR(50) NULL,
	CONSTRAINT [FK_Films_Directors] FOREIGN KEY ([DirectorId]) REFERENCES [Directors]([Id])
)