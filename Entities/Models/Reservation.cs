namespace Entities.Models;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice => CountTotalPrice();

    public int UserId { get; set; }
    public int RoomId { get; set; }

    public User User { get; set; }
    public Room Room { get; set; }

    private decimal CountTotalPrice()
    {
        int numberOfNights = (EndDate - StartDate).Days;
        return numberOfNights * Room.PriceOneNight;
    }
}
