--Выбрать всю информацию обо всех кораблях.

select s.Name, s.ArrivalPort, s.DeparturePort, s.ArrivalTime, s.DepartureTime from [Ship] s

--Выбрать контактных лиц фирмы “IBM”.

select c.Name from [Contact] c, [Customer] s 
	where s.Name = 'IBM' and c.CustomerId = s.Id

--Выбрать номера контейнеров, у которых вес пустого контейнера меньше 300 и упорядочить их по этому весу.

select c.Id from [Container] c
	where c.Weight < 300
	order by c.Weight

--Выбрать номера групп для контейнеров с общим весом груза больше 100 и номера контейнеров, у которых  номер корабля,
--на котором они будут отправлены, содержит цифру 5. Список упорядочить по номеру группы.

select o.Number, c.ContainerId from [OrderedContainer] c, [Order] o -- Не совсем понял задание, но пусть будет так
	where c.Weight > 100 and o.Number = c.OrderId and charindex('5', c.ShipId) != 0
	order by o.Number

--Выдать время заказа перевозки груза, имена кораблей и дату их отправления, для которых время заказа с 10:10 до 20:00 1 июля 2005 года.

select distinct o.Data, s.Name, s.DepartureTime from [Order] o, [Ship] s, [OrderedContainer] c
	where o.Data between '2005/06/01 10:10' and '2005/06/01 20:00' and c.ShipId = s.Id and c.OrderId = o.Number

--Посчитать общий вес всех пустых контейнеров в первой группе контейнеров.

select sum(c.Weight) from [OrderedContainer] o, [Container] c
	where o.ContainerId = c.Id and o.OrderId = 1

--Получить список фирм, отсортированный по количеству контактных лиц. (Насколько я помню, так исправляли задачу)

select a.[Name] from [Contact] c, [Customer] a
	where c.CustomerId = a.Id
	group by a.Name
	order by count(a.Name)

--Выбрать постоянных клиентов корабля Титаник (не менее 2 заказов)

select c.[Name] from [Customer] c, [Ship] s, [Order] o, [OrderedContainer] a
	where s.Name = N'Титаник' and o.CustomerId = c.Id and a.ShipId = s.Id and a.OrderId = o.Number
	group by c.Name
	having count(c.[Name]) >= 2

--Удалить контакное лицо - Иванова.

delete from [Contact] where Contact.Name = N'Иванова'

--Удалить все заказы, связанные с Титаником. (В Ordered Container прописан ON DELETE CASCADE)

delete from [Order]
	where [Order].[Number] in
		(select a.Number from [OrderedContainer] o, [Ship] s, [Order] a
		where o.ShipId = s.Id and s.Name = N'Титаник' and [Order].Number = o.OrderId)

 --Заменить порт приписки кораблей Севастополь на Одессу

 update [Ship]
	set [Ship].[DeparturePort] = N'Одесса'
	where [Ship].[DeparturePort] = N'Севастополь'