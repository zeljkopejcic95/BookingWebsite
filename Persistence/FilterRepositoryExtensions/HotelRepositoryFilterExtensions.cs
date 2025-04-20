using Entities.Models;
using Entities.Paging.Parameters;

namespace Persistence.FilterRepositoryExtensions;

public static class HotelRepositoryFilterExtensions
{
    public static IQueryable<Hotel> FilterHotels(
        this IQueryable<Hotel> hotels,
        HotelParameters hotelParameters)
    {
        if (!string.IsNullOrWhiteSpace(hotelParameters.SearchTerm))
        {
            var lowerTerm = hotelParameters.SearchTerm.ToLower();
            hotels = hotels.Where(h =>
                h.Name.ToLower().Contains(lowerTerm) ||
                h.Description.ToLower().Contains(lowerTerm));
        }

        if (!string.IsNullOrWhiteSpace(hotelParameters.Address))
            hotels = hotels.Where(h => h.Address.ToLower()
            .Contains(hotelParameters.Address.ToLower()));

        if (hotelParameters.CityId.HasValue)
            hotels = hotels.Where(h => h.CityId == hotelParameters.CityId.Value);

        return hotels;
    }

    public static IQueryable<Hotel> SearchByName(this IQueryable<Hotel> hotels, string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return hotels;

        var lowerName = name.ToLower();
        return hotels.Where(h => h.Name.ToLower().Contains(lowerName));
    }
}
