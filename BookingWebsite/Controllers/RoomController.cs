using AutoMapper;
using BookingWebsite.ActionFilters;
using BookingWebsite.Extensions;
using Entities.Dto.Reservation;
using Entities.Dto.Room;
using Entities.Models;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWebsite.Controllers;

[Route("api/rooms")]
[ApiController]
public class RoomController : BaseController
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public RoomController(
        IRepositoryManager repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllRoomsAsync([FromQuery] RoomParameters roomParameters)
    {
        var rooms = await _repository.Room.GetAllRoomsAsync(roomParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(rooms.MetaData));

        var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

        return Ok(roomsDto);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Room>))]
    public async Task<IActionResult> GetRoomAsync(int id)
    {
        var room = await _repository.Room.GetRoomAsync(id, trackChanges: false);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        var roomDto = _mapper.Map<RoomDto>(room);

        return Ok(roomDto);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateRoom([FromBody] RoomForCreationDto room)
    {
        var roomEntity = _mapper.Map<Room>(room);

        _repository.Room.CreateRoom(roomEntity);
        await _repository.SaveAsync();

        var createdRoom = _mapper.Map<RoomDto>(roomEntity);
        return Ok(createdRoom);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Room>))]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomForUpdateDto roomDto)
    {
        var roomEntity = await _repository.Room.GetRoomAsync(id, trackChanges: true);

        _mapper.Map(roomDto, roomEntity);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Room>))]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _repository.Room.GetRoomAsync(id, trackChanges: false);

        _repository.Room.DeleteRoom(room);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpGet("{roomId}/reservations")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Room>))]
    public async Task<IActionResult> GetReservationsForRoom(
        int roomId,
        [FromQuery] ReservationParameters reservationParameters)
    {
        var room = await _repository.Room.GetRoomAsync(roomId, trackChanges: false);

        var reservations = await _repository.Reservation.GetReservationsByRoomAsync(
            roomId,
            reservationParameters,
            trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(reservations.MetaData));

        var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsDto);
    }
}