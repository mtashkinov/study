CREATE TABLE [dbo].[OrderedContainer]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [OrderId] INT NOT NULL, 
    [ShipId] INT NOT NULL, 
    [StampNumber] INT NOT NULL, 
    [Weight] INT NOT NULL, 
    [ContainerId] INT NOT NULL, 
    CONSTRAINT [FK_OrderedContainer_Order] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Number]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrderedContainer_Ship] FOREIGN KEY ([ShipId]) REFERENCES [Ship]([Id]), 
    CONSTRAINT [FK_OrderedContainer_Container] FOREIGN KEY ([ContainerId]) REFERENCES [Container]([Id])
)
