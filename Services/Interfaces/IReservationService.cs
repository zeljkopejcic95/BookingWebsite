using Entities.Dto.Reservation;
using Entities.Paging;
using Entities.Paging.Parameters;

namespace Services.Interfaces;

public interface IReservationService
{
    Task<PagedList<ReservationDto>> GetAllReservationsAsync(ReservationParameters parameters);
    Task<ReservationDto> GetReservationByIdAsync(int id);
    Task<ReservationDto> CreateReservationAsync(ReservationForCreationDto dto);
    Task UpdateReservationAsync(int id, ReservationForUpdateDto dto);
    Task DeleteReservationAsync(int id);
}
