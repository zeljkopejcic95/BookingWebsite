using AutoMapper;
using Entities.Dto.Reservation;
using Entities.Models;
using Entities.Paging;
using Entities.Paging.Parameters;
using Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class ReservationService : IReservationService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ReservationService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedList<ReservationDto>> GetAllReservationsAsync(ReservationParameters reservationParameters)
    {
        var reservations = await _repository.Reservation.GetAllReservationsAsync(reservationParameters, trackChanges: false);
        var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return new PagedList<ReservationDto>(reservationsDto.ToList(), reservations.MetaData.TotalCount, reservationParameters.PageNumber, reservationParameters.PageSize);
    }

    public async Task<ReservationDto> GetReservationByIdAsync(int id)
    {
        var reservation = await _repository.Reservation.GetReservationAsync(id, trackChanges: false);
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> CreateReservationAsync(ReservationForCreationDto dto)
    {
        var reservationEntity = _mapper.Map<Reservation>(dto);
        _repository.Reservation.CreateReservation(reservationEntity);
        await _repository.SaveAsync();
        return _mapper.Map<ReservationDto>(reservationEntity);
    }

    public async Task UpdateReservationAsync(int id, ReservationForUpdateDto dto)
    {
        var reservation = await _repository.Reservation.GetReservationAsync(id, trackChanges: true);
        _mapper.Map(dto, reservation);
        await _repository.SaveAsync();
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _repository.Reservation.GetReservationAsync(id, trackChanges: false);
        _repository.Reservation.DeleteReservation(reservation);
        await _repository.SaveAsync();
    }
}
