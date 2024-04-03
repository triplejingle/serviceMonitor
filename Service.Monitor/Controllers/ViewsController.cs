using Microsoft.AspNetCore.Mvc;
using Service.Monitor.Domain;
using Service.Monitor.Interfaces;

namespace Service.Monitor.Controllers;

[ApiController]
[Route("/v1")]
[Produces("application/json")]
public class ViewsController : ControllerBase
{
    private readonly ILogger<MonitorController> _logger;
    private readonly IViewsRepository _viewsRepository;

    public ViewsController(IViewsRepository viewsRepository)
    {
        _viewsRepository = viewsRepository;
    }

    [HttpGet]
    [HttpOptions]
    [Route("views/actions/{businessName}")]
    [ProducesResponseType(typeof(Event), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetActionsPerBusiness(
        [FromRoute] string businessName,
        [FromQuery] string? page,
        [FromQuery] string? action,
        [FromQuery] DateTime? start,
        [FromQuery] DateTime? end
    )
    {
        var actionsPerBusinesses = await _viewsRepository.GetActionsPerBusiness(businessName, page, action, start, end);
        return Ok(actionsPerBusinesses);
    }
}