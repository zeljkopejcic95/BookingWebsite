using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.Reservation;

public class ReservationForCreationDto
{
    [Required(ErrorMessage = "Start date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "User ID is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Room ID is required.")]
    public int RoomId { get; set; }
}
