using Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingWebsite.ActionFilters;

public class NotFoundFilterAttribute<TEntity> : IAsyncActionFilter where TEntity : class
{
    private readonly ILoggerManager _logger;
    private readonly IRepositoryManager _repository;

    public NotFoundFilterAttribute(ILoggerManager logger, IRepositoryManager repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("id", out var idObj) || idObj is not int id)
        {
            await next();
            return;
        }

        object? entity = typeof(TEntity).Name switch
        {
            nameof(City) =>
                await _repository.City.GetCityAsync(id, trackChanges: false),

            nameof(Hotel) =>
                await _repository.Hotel.GetHotelAsync(id, trackChanges: false),

            nameof(Reservation) =>
                await _repository.Reservation.GetReservationAsync(id, trackChanges: false),

            nameof(User) =>
                await _repository.User.GetUserAsync(id, trackChanges: false),

            nameof(Room) =>
                await _repository.Room.GetRoomAsync(id, trackChanges: false),

            _ => null
        };

        if (entity == null)
        {
            _logger.LogInfo($"{typeof(TEntity).Name} with id: {id} doesn't exist in the database.");
            context.Result = new NotFoundResult();
            return;
        }

        context.HttpContext.Items["entity"] = entity;
        await next();
    }
}