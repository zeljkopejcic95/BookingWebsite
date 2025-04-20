using AutoMapper;
using BookingWebsite.ActionFilters;
using BookingWebsite.Extensions;
using Entities.Dto.Reservation;
using Entities.Models;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWebsite.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationController : BaseController
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ReservationController(
        IRepositoryManager repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllReservationsAsync([FromQuery] ReservationParameters reservationParameters)
    {
        var reservations = await _repository.Reservation.GetAllReservationsAsync(reservationParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(reservations.MetaData));

        var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsDto);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Reservation>))]
    public async Task<IActionResult> GetReservationAsync(int id)
    {
        var reservation = await _repository.Reservation.GetReservationAsync(id, trackChanges: false);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        var reservationDto = _mapper.Map<ReservationDto>(reservation);

        return Ok(reservationDto);
    }

    [HttpPost]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationForCreationDto reservationDto)
    {
        var reservationEntity = _mapper.Map<Reservation>(reservationDto);

        _repository.Reservation.CreateReservation(reservationEntity);
        await _repository.SaveAsync();

        var createdReservation = _mapper.Map<ReservationDto>(reservationEntity);
        return Ok(createdReservation);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Reservation>))]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationForUpdateDto reservationDto)
    {
        var reservationEntity = await _repository.Reservation.GetReservationAsync(id, trackChanges: true);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        _mapper.Map(reservationDto, reservationEntity);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Reservation>))]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _repository.Reservation.GetReservationAsync(id, trackChanges: false);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        _repository.Reservation.DeleteReservation(reservation);
        await _repository.SaveAsync();

        return NoContent();
    }
}
