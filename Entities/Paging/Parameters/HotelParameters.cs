namespace Entities.Paging.Parameters;

public class HotelParameters : PaginationParameters
{
    public string? SearchTerm { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
}
