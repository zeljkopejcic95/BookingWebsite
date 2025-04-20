using Entities;
using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.FilterRepositoryExtensions;

namespace Persistence.ModelsRepository;

public class RoomRepository : RepositoryBase<Room>, IRoomRepository
{
    public RoomRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<Room>> GetAllRoomsAsync(
        RoomParameters roomParameters,
        bool trackChanges)
    {
        var rooms = FindAll(trackChanges)
            .FilterRooms(roomParameters)
            .OrderBy(r => r.Number);

        return await PagedList<Room>.ToPagedList(
            rooms,
            roomParameters.PageNumber,
            roomParameters.PageSize);
    }

    public async Task<Room> GetRoomAsync(int roomId, bool trackChanges) =>
        await FindByCondition(r => r.Id == roomId, trackChanges)
        .SingleOrDefaultAsync();

    public void CreateRoom(Room room) => Create(room);

    public void DeleteRoom(Room room) => Delete(room);

    public async Task<PagedList<Room>> GetRoomsByHotelAsync(
        int hotelId,
        RoomParameters roomParameters,
        bool trackChanges)
    {
        var rooms = FindByCondition(r => r.HotelId == hotelId, trackChanges)
            .OrderBy(r => r.Number);

        return await PagedList<Room>.ToPagedList(
            rooms,
            roomParameters.PageNumber,
            roomParameters.PageSize);
    }
}
