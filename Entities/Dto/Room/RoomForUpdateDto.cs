using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.Room;

public class RoomForUpdateDto
{
    [Required(ErrorMessage = "Room number is required.")]
    [Range(1, 999, ErrorMessage = "Room number must be a positive integer and below 999.")]
    public int Number { get; set; }

    [Required(ErrorMessage = "Room capacity is required.")]
    [Range(1, 50, ErrorMessage = "Capacity must be a positive integer and below 50.")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Price per night is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price per night must be greater than 0.")]
    public decimal PriceOneNight { get; set; }
}
