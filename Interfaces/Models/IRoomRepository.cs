using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;

namespace Interfaces;

public interface IRoomRepository
{
    Task<PagedList<Room>> GetAllRoomsAsync(RoomParameters roomParameters, bool trackChanges);
    Task<Room> GetRoomAsync(int roomId, bool trackChanges);
    void CreateRoom(Room room);
    void DeleteRoom(Room room);
    Task<PagedList<Room>> GetRoomsByHotelAsync(
        int hotelId,
        RoomParameters roomParameters,
        bool trackChanges);
}