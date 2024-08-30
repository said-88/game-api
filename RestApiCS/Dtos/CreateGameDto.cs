using System.ComponentModel.DataAnnotations;
namespace RestApiCS.Dtos;

public record class CreateGameDto (
    [Required][StringLength(50)] string Name,
    int GenreId,
    [Range(1,3000)] decimal Price,
    DateOnly ReleaseDate
);
