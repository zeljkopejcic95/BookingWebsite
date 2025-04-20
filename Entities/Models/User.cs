using Entities.Enums;

namespace Entities.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; } = UserRole.Guest;

    public List<Reservation> Reservations { get; set; } = new();
}
