using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.City;

public class CityForCreationDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    [MaxLength(50, ErrorMessage = "Country can't be longer than 50 characters.")]
    public string Country { get; set; }
}
