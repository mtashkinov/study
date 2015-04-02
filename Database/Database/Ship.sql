CREATE TABLE [dbo].[Ship]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [DeparturePort] NCHAR(20) NOT NULL, 
    [ArrivalPort] NCHAR(20) NULL, 
    [DepartureTime] DATETIME NULL, 
    [ArrivalTime] DATETIME NULL
)
