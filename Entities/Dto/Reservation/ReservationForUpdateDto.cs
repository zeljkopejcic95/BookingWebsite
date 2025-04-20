using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.Reservation;

public class ReservationForUpdateDto
{
    [Required(ErrorMessage = "Start date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateTime EndDate { get; set; }
}
