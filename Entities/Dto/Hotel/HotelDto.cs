using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.Hotel;

public class HotelDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Hotel name is required.")]
    [MaxLength(100, ErrorMessage = "Hotel name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Hotel description is required.")]
    [MaxLength(200, ErrorMessage = "Hotel description cannot be longer than 200 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Hotel address is required.")]
    [MaxLength(150, ErrorMessage = "Hotel address cannot be longer than 150 characters.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "City ID is required.")]
    public int CityId { get; set; }
}
