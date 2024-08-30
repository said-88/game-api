using System.ComponentModel.DataAnnotations;

namespace RestApiCS.Dtos; 

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name,
    int GenreId,
    [Range(1,3000)] decimal Price,
    DateOnly ReleaseDate
);