using AutoMapper;
using BookingWebsite.ActionFilters;
using Entities.Dto.City;
using Entities.Dto.Hotel;
using Entities.Models;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWebsite.Controllers;

[ApiController]
[Route("api/cities")]
public class CityController : BaseController
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public CityController(
        IRepositoryManager repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCitiesAsync([FromQuery] CityParameters cityParameters)
    {
        var cities = await _repository.City.GetAllCitiesAsync(cityParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(cities.MetaData));

        var citiesDto = _mapper.Map<IEnumerable<CityDto>>(cities);

        return Ok(citiesDto);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ServiceFilter(typeof(NotFoundFilterAttribute<City>))]
    public async Task<IActionResult> GetCityAsync(int id)
    {
        var city = await _repository.City.GetCityAsync(id, trackChanges: false);

        var cityDto = _mapper.Map<CityDto>(city);

        return Ok(cityDto);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCity([FromBody] CityForCreationDto city)
    {
        var cityEntity = _mapper.Map<City>(city);

        _repository.City.CreateCity(cityEntity);
        await _repository.SaveAsync();

        var cityToReturn = _mapper.Map<CityDto>(cityEntity);
        return Ok(cityToReturn);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(NotFoundFilterAttribute<City>))]
    public async Task<IActionResult> UpdateCity(int id, [FromBody] CityForUpdateDto city)
    {
        var cityEntity = await _repository.City.GetCityAsync(id, trackChanges: true);

        _mapper.Map(city, cityEntity);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<City>))]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await _repository.City.GetCityAsync(id, trackChanges: false);

        _repository.City.DeleteCity(city);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpGet("{id}/hotels")]
    [AllowAnonymous]
    [ServiceFilter(typeof(NotFoundFilterAttribute<City>))]
    public async Task<IActionResult> GetHotelsForCity(int id, [FromQuery] HotelParameters hotelParameters)
    {
        var city = await _repository.City.GetCityAsync(id, trackChanges: false);

        var hotels = await _repository.Hotel.GetHotelsByCityAsync(id, hotelParameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(hotels.MetaData));

        var hotelsDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);

        return Ok(hotelsDto);
    }
}
