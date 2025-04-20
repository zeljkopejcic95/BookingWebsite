namespace Entities.Paging.Parameters;

public class RoomParameters : PaginationParameters
{
    public int? Capacity { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? HotelId { get; set; }
}