using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;

namespace Interfaces;

public interface IReservationRepository
{
    Task<PagedList<Reservation>> GetAllReservationsAsync(
        ReservationParameters reservationParameters,
        bool trackChanges);
    Task<Reservation> GetReservationAsync(int reservationId, bool trackChanges);
    void CreateReservation(Reservation reservation);
    void DeleteReservation(Reservation reservation);
    Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId, bool trackChanges);
    Task<PagedList<Reservation>> GetReservationsByRoomAsync(
        int roomId,
        ReservationParameters reservationParameters,
        bool trackChanges);
}
