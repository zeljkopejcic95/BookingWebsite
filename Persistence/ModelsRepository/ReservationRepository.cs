using Entities;
using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.ModelsRepository;

public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
{
    public ReservationRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<Reservation>> GetAllReservationsAsync(
        ReservationParameters reservationParameters,
        bool trackChanges)
    {
        var reservations = FindAll(trackChanges)
            .OrderBy(r => r.StartDate);

        return await PagedList<Reservation>.ToPagedList(
            reservations,
            reservationParameters.PageNumber,
            reservationParameters.PageSize);
    }

    public async Task<Reservation> GetReservationAsync(int reservationId, bool trackChanges) =>
        await FindByCondition(r => r.Id == reservationId, trackChanges)
        .SingleOrDefaultAsync();

    public void CreateReservation(Reservation reservation) => Create(reservation);

    public void DeleteReservation(Reservation reservation) => Delete(reservation);

    public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId, bool trackChanges) =>
        await FindByCondition(r => r.UserId == userId, trackChanges)
        .ToListAsync();

    public async Task<PagedList<Reservation>> GetReservationsByRoomAsync(
        int roomId,
        ReservationParameters reservationParameters,
        bool trackChanges)
    {
        var reservations = FindByCondition(r => r.RoomId == roomId, trackChanges)
            .OrderBy(r => r.StartDate);

        return await PagedList<Reservation>.ToPagedList(
            reservations,
            reservationParameters.PageNumber,
            reservationParameters.PageSize);
    }
}
