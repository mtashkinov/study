drop table [dbo].[ActorFilm]
go

drop table [dbo].[Films]
go

drop table [dbo].[Directors]
go

drop table [dbo].[Actors]
go


CREATE TABLE [dbo].[Actors]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL
)

go

CREATE TABLE [dbo].[Directors]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL
)

go

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

go

CREATE TABLE [dbo].[ActorFilm]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [FilmId] INT NOT NULL, 
    [ActorId] INT NOT NULL,
	CONSTRAINT [FK_ActorFilm_Actors] FOREIGN KEY ([ActorId]) REFERENCES [Actors]([Id]),
	CONSTRAINT [FK_ActorFilm_Films] FOREIGN KEY ([FilmId]) REFERENCES [Films]([Id]) ON DELETE CASCADE
)

go


insert into [dbo].[Directors] ([Name]) values
(N'Братья Люмьер'),
(N'Фрэнк Дарабонт'),
(N'Роберт Земекис'),
(N'Стивен Спилберг'),
(N'Оливье Накаш, Эрик Толедано'),
(N'Кристофер Нолан'),
(N'Люк Бессон'),
(N'Дэвид Финчер'),
(N'Леонид Гайдай')
GO

insert into [dbo].[Actors] ([Name]) values
(N'Тим Роббинс'),
(N'Морган Фриман'),
(N'Боб Гантон'),
(N'Том Хэнкс'),
(N'Дэвид Морс'),
(N'Майкл Кларк Дункан'),
(N'Робин Райт'),
(N'Гэри Синиз'),
(N'Лиам Нисон'),
(N'Бен Кингсли'),
(N'Рэйф Файнс'),
(N'Франсуа Клюзе'),
(N'Омар Си'),
(N'Леонардо ДиКаприо'),
(N'Джозеф Гордон-Левитт'),
(N'Эллен Пейдж'),
(N'Жан Рено'),
(N'Гари Олдман'),
(N'Натали Портман'),
(N'Эдвард Нортон'),
(N'Брэд Питт'),
(N'Хелена Бонем Картер'),
(N'Александр Демьяненко'),
(N'Юрий Яковлев'),
(N'Леонид Куравлёв')
GO

insert into [dbo].[Films] ([Name], [Country], [Year], [DirectorId], [Image]) values
(N'Прибытие поезда на вокзал Ла-Сьота', N'Франция', '1896', 1, N'f89544'),
(N'Побег из Шоушенка', N'США', '1994', 2, N'f326'),
(N'Зеленая миля', N'США', '1999', 2, N'f435'),
(N'Форрест Гамп', N'США', '1994', 3, N'f448'),
(N'Список Шиндлера', N'США', '1993', 4, N'f329'),
(N'1+1', N'Франция', '2011', 5, N'f535341'),
(N'Начало', N'США, Великобритания', '2010', 6, N'f447301'),
(N'Леон', N'Франция', '1994', 7, N'389'),
(N'Бойцовский клуб', N'США, Германия', '1999', 8, N'f361'),
(N'Иван Васильевич меняет профессию', N'СССР', '1973', 9, N'f42664')
GO

insert into [dbo].[ActorFilm] ([ActorId], [FilmId]) values
(1, 2),
(2, 2),
(3, 2),
(4, 3),
(5, 3),
(6, 3),
(4, 4),
(7, 4),
(8, 4),
(9, 5),
(10, 5),
(11, 5),
(12, 6),
(13, 6),
(14, 7),
(15, 7),
(16, 7),
(17, 8),
(18, 8),
(19, 8),
(20, 9),
(21, 9),
(22, 9),
(23, 10),
(24, 10),
(25, 10)
GO