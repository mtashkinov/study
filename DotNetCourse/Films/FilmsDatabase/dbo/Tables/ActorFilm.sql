CREATE TABLE [dbo].[ActorFilm]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [FilmId] INT NOT NULL, 
    [ActorId] INT NOT NULL,
	CONSTRAINT [FK_ActorFilm_Actors] FOREIGN KEY ([ActorId]) REFERENCES [Actors]([Id]),
	CONSTRAINT [FK_ActorFilm_Films] FOREIGN KEY ([FilmId]) REFERENCES [Films]([Id])
)