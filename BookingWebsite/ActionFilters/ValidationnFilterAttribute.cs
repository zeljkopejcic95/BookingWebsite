using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingWebsite.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{
    private readonly ILoggerManager _logger;

    public ValidationFilterAttribute(ILoggerManager logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value?.GetType().Name.EndsWith("Dto") == true).Value;

        if (param == null)
        {
            _logger.LogError("DTO object sent from client is null.");
            context.Result = new BadRequestObjectResult("DTO object is null.");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the DTO object.");
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    { }
}
