using RestApiCS.Dtos;
using RestApiCS.Entities;

namespace RestApiCS.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }

    public static Genre ToEntity(this CreateGenreDto newGenre)
    {
        return new Genre { Name = newGenre.Name };
    }
}
