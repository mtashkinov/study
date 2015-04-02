CREATE TABLE [dbo].[Container]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Type] NCHAR(10) NOT NULL, 
    [Weight] INT NOT NULL, 
    [Сapacity] INT NOT NULL
)
