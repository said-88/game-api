using RestApiCS.Dtos;
using RestApiCS.Entities;

namespace RestApiCS.Mapping;
public static class GameMapping {
    public static Game ToEntity(this CreateGameDto newGame)    
    {
        return new Game()
        {
            Name = newGame.Name,
            GenreId = newGame.GenreId,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };
    }

    public static GameDto ToDto(this Game game)
    {
        return new (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDto ToDetailsDto(this Game game)
    {
        return new (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {   
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };	
    }
}