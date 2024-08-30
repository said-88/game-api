namespace RestApiCS.EndPoints;

using Microsoft.EntityFrameworkCore;
using RestApiCS.Data;
using RestApiCS.Dtos;
using RestApiCS.Entities;
using RestApiCS.Mapping;

public static class GamesEndpoints
{
    public static readonly List<GameDto> games =
    [
        new(1, "Super Mario Bros", "Multiplatform", 1985.14m, new DateOnly(1985, 9, 13)),
        new(2, "The Legend of Zelda", "Adventure", 1986.14m, new DateOnly(1986, 2, 21)),
        new(3, "Metroid", "Multiplatform", 1986.14m, new DateOnly(1986, 8, 6)),
        new(4, "Kid Icarus", "Multiplatform", 1986.14m, new DateOnly(1986, 12, 19)),
        new(5, "Spiderman", "Action", 1982.14m, new DateOnly(1982, 8, 6)),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        // change the parameter to GameStoreContext
        group.MapGet(
            "/",
            async (GameStoreContext dbContext) =>
                await dbContext.Games
                .Include(g => g.Genre)
                .Select(g => g.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group
            .MapGet(
                "/{id}",
                async (int id, GameStoreContext dbContext) =>
                {
                    Game? game = await dbContext.Games.FindAsync(id);

                    return game is null ? Results.NotFound() : Results.Json(game.ToDetailsDto());
                }
            )
            .WithName("GetGameById");

        group.MapPost(
            "/",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                // Extension method to convert CreateGameDto to Game entity
                Game game = newGame.ToEntity();

                await dbContext.Games.AddAsync(game);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    "GetGameById",
                    new { id = game.Id },
                    game.ToDetailsDto()
                );
            }
        );

        group.MapPut(
            "/{id}",
            async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();

                return Results.NoContent();
            }
        );

        return group;
    }
}
