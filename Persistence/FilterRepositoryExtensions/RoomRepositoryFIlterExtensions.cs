using Entities.Models;
using Entities.Paging.Parameters;

namespace Persistence.FilterRepositoryExtensions;

public static class RoomRepositoryFIlterExtensions
{
    public static IQueryable<Room> FilterRooms(
        this IQueryable<Room> rooms,
        RoomParameters roomParameters)
    {
        if (roomParameters.Capacity.HasValue)
            rooms = rooms.Where(r => r.Capacity == roomParameters.Capacity.Value);

        if (roomParameters.MinPrice.HasValue)
            rooms = rooms.Where(r => r.PriceOneNight >= roomParameters.MinPrice.Value);

        if (roomParameters.MaxPrice.HasValue)
            rooms = rooms.Where(r => r.PriceOneNight <= roomParameters.MaxPrice.Value);

        if (roomParameters.HotelId.HasValue)
            rooms = rooms.Where(r => r.HotelId == roomParameters.HotelId.Value);

        return rooms;
    }
}
