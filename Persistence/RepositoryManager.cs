using Entities;
using Interfaces;
using Interfaces.Models;
using Persistence.ModelsRepository;

namespace Persistence;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    private IUserRepository _userRepository;
    private IRoomRepository _roomRepository;
    private IReservationRepository _reservationRepository;
    private IHotelRepository _hotelRepository;
    private ICityRepository _cityRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IUserRepository User => _userRepository ??= new UserRepository(_repositoryContext);
    public IRoomRepository Room => _roomRepository ??= new RoomRepository(_repositoryContext);
    public IReservationRepository Reservation => _reservationRepository ??= new ReservationRepository(_repositoryContext);
    public IHotelRepository Hotel => _hotelRepository ??= new HotelRepository(_repositoryContext);
    public ICityRepository City => _cityRepository ??= new CityRepository(_repositoryContext);

    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}
