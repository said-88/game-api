namespace RestApiCS.EndPoints;

using Microsoft.EntityFrameworkCore;
using RestApiCS.Data;
using RestApiCS.Dtos;
using RestApiCS.Mapping;

public static class GenresEndPoints
{
    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("genres").WithParameterValidation();

        group.MapGet("/", async (GameStoreContext dbContext) =>
                await dbContext.Genres
                .Select(g => g.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );
        
        group.MapPost("/", async (CreateGenreDto newGenre, GameStoreContext dbContext) =>
        {
            var genre = newGenre.ToEntity();
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
            return genre.ToDto();
        });

        return group;
    }
}