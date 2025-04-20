using AutoMapper;
using BookingWebsite.ActionFilters;
using BookingWebsite.Extensions;
using Entities.Dto.Reservation;
using Entities.Dto.User;
using Entities.Models;
using Entities.Paging.Parameters;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWebsite.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : BaseController
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public UserController(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper,
        IJwtService jwtService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserParameters userParameters)
    {
        var users = await _repository.User.GetAllUsersAsync(
            userParameters,
            trackChanges: false);

        Response.Headers.Add
            ("X-Pagination", JsonConvert.SerializeObject(users.MetaData));

        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        return Ok(usersDto);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ServiceFilter(typeof(NotFoundFilterAttribute<User>))]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        var user = await _repository.User.GetUserAsync(id, trackChanges: false);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [HttpPost]
    [AllowAnonymous]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto user)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var userEntity = _mapper.Map<User>(user);
        userEntity.PasswordHash = passwordHash;

        _repository.User.CreateUser(userEntity);
        await _repository.SaveAsync();

        var createdUser = _mapper.Map<UserDto>(userEntity);
        var token = _jwtService.GenerateToken(createdUser);

        return Ok(new
        {
            user = createdUser,
            token
        });

    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
    {
        var user = await _repository.User.GetUserByEmailAsync(userDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
        {
            _logger.LogError("Invalid email or password.");
            return Unauthorized("Invalid email or password.");
        }

        var userDtoResponse = _mapper.Map<UserDto>(user);
        var token = _jwtService.GenerateToken(userDtoResponse);

        return Ok(new { user = userDtoResponse, token });
    }

    [HttpPut("{id}")]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(NotFoundFilterAttribute<User>))]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto user)
    {
        var userEntity = await _repository.User.GetUserAsync(id, trackChanges: true);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        _mapper.Map(user, userEntity);
        await _repository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ServiceFilter(typeof(NotFoundFilterAttribute<User>))]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var reservations = await _repository.Reservation.GetReservationsByUserAsync(id, trackChanges: false);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return Ok(reservationsDto);
    }

    [HttpGet("{id}/reservations")]
    [Authorize]
    [ServiceFilter(typeof(NotFoundFilterAttribute<User>))]
    public async Task<IActionResult> GetReservationsForUser(int id)
    {
        var user = await _repository.User.GetUserAsync(id, trackChanges: false);
        if (!User.IsUserSelfOrAdmin(id))
        {
            return Forbid("You are not allowed to access this resource.");
        }

        var reservations = await _repository.Reservation.GetReservationsByUserAsync(id, trackChanges: false);
        var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsDto);
    }
}