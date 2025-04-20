using Interfaces.Models;

namespace Interfaces;

public interface IRepositoryManager
{
    ICityRepository City { get; }
    IHotelRepository Hotel { get; }
    IReservationRepository Reservation { get; }
    IRoomRepository Room { get; }
    IUserRepository User { get; }

    Task SaveAsync();
}
