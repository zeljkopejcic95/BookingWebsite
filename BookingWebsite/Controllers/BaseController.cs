using Microsoft.AspNetCore.Mvc;

namespace BookingWebsite.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleNotFound(string resourceName, int id)
    {
        return NotFound($"{resourceName} with id: {id} doesn't exist in the database.");
    }

    protected IActionResult HandleInvalidModelState(string resourceName)
    {
        return UnprocessableEntity($"Invalid model state for the {resourceName} object");
    }
}