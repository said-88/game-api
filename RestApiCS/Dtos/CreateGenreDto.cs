using System.ComponentModel.DataAnnotations;
namespace RestApiCS.Dtos;

public record class CreateGenreDto (
    [Required] string Name
);