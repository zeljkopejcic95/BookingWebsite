using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;

namespace Interfaces;

public interface IHotelRepository
{
    Task<PagedList<Hotel>> GetAllHotelsAsync(HotelParameters hotelParameters, bool trackChanges);
    Task<Hotel> GetHotelAsync(int hotelId, bool trackChanges);
    void CreateHotel(Hotel hotel);
    void DeleteHotel(Hotel hotel);
    Task<PagedList<Hotel>> GetHotelsByCityAsync(int cityId, HotelParameters hotelParameters, bool trackChanges);
}
