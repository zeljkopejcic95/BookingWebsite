using AutoMapper;
using BookingWebsite.ActionFilters;
using Entities.Dto.Hotel;
using Entities.Dto.Room;
using Entities.Models;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWebsite.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : BaseController
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public HotelController(
        IRepositoryManager repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllHotelsAsync([FromQuery] HotelParameters hotelParameters)
    {
        var hotels = await _repository.Hotel.GetAllHotelsAsync(hotelParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(hotels.MetaData));

        var hotelsDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);

        return Ok(hotelsDto);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Hotel>))]
    public async Task<IActionResult> GetHotelAsync(int id)
    {
        var hotel = await _repository.Hotel.GetHotelAsync(id, trackChanges: false);

        var hotelDto = _mapper.Map<HotelDto>(hotel);

        return Ok(hotelDto);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateHotel([FromBody] HotelForCreationDto hotel)
    {
        var hotelEntity = _mapper.Map<Hotel>(hotel);

        _repository.Hotel.CreateHotel(hotelEntity);
        await _repository.SaveAsync();

        var hotelToReturn = _mapper.Map<HotelDto>(hotelEntity);
        return Ok(hotelToReturn);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Hotel>))]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelForUpdateDto hotel)
    {
        var hotelEntity = await _repository.Hotel.GetHotelAsync(id, trackChanges: true);

        _mapper.Map(hotel, hotelEntity);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Hotel>))]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _repository.Hotel.GetHotelAsync(id, trackChanges: false);

        _repository.Hotel.DeleteHotel(hotel);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpGet("{hotelId}/rooms")]
    [AllowAnonymous]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Hotel>))]
    public async Task<IActionResult> GetRoomsForHotel(int hotelId, [FromQuery] RoomParameters roomParameters)
    {
        var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);

        var rooms = await _repository.Room.GetRoomsByHotelAsync(hotelId, roomParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(rooms.MetaData));

        var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

        return Ok(roomsDto);
    }
}
