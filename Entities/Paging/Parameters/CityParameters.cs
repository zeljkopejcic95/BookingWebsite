namespace Entities.Paging.Parameters;

public class CityParameters : PaginationParameters
{
    public string? Name { get; set; }
    public string? Country { get; set; }
}