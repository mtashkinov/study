insert into dbo.[Ship] ([Name], [DeparturePort], [DepartureTime], [ArrivalPort], [ArrivalTime]) values
(N'Вася', N'Одесса', N'2005/05/01 10:20', N'Одесса', N'2005/05/01 10:21'),
(N'Петя', N'Одесса', N'2005/05/01 10:21', N'Одесса', N'2005/05/01 10:22'),
(N'Иван', N'Одесса', N'2005/05/01 10:22', N'Одесса', N'2005/05/01 10:23'),
(N'Эдуардо', N'Севастополь', N'2005/05/01 10:23', N'Одесса', N'2005/05/01 10:24'),
(N'Титаник', N'Одесса', N'2005/05/01 10:24', N'Одесса', N'2005/05/01 10:25')

go

insert into dbo.[Customer] ([Name], [Adress], [Сharacteristic]) values
(N'IBM', N'Америка', N'Хорошие'),
(N'MIB', N'Америка', N'Очень хорошие'),
(N'Иванова Co', N'Россия', N'Плохие')

go

insert into dbo.[Contact] ([Name], [Adress], [Email], [Fax], [Phone], [CustomerId]) values
(N'Петя', N'Америка', N'petya@ibm.com', N'927467183', N'8293746173', 1),
(N'Вася', N'Америка', N'vasya@ibm.com', N'927467133', N'8293734173', 1),
(N'J', N'Америка', N'j@mib.com', N'00000000', N'1111111111', 2),
(N'Иванова', N'Россия', N'ivanova@ivanova.ru', N'92384832', N'23489873', 3)

go

insert into dbo.[Container] ([Weight], [Сapacity], [Type]) values
(100, 59, N'Обычный'),
(301, 65, N'Необычный'),
(101, 43, N'обычный')

go

insert into dbo.[Order] ([Name], [Cost], [Data], [Volume], [CustomerId], [Weight]) values
(N'Пришельцы', 10, N'2005/06/01 10:24', 50, 2, 25),
(N'Ещё пришельцы', 11, N'2005/06/01 10:25', 51, 2, 26)

go

insert into dbo.[OrderedContainer] ([Weight], [StampNumber], [OrderId], [ShipId], [ContainerId]) values
(101, 0, 1, 5, 1),
(102, 0, 1, 4, 3),
(302, 0, 2, 5, 2)

go