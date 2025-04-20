namespace Entities.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }

    public List<Hotel> Hotels { get; set; } = new();
}
