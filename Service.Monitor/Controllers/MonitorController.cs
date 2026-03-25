using Microsoft.AspNetCore.Mvc;
using Service.Monitor.Controllers.Requests;
using Service.Monitor.Services.Interfaces;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Controllers;

[ApiController]
[Route("/v1")]
[Produces("application/json")]
public class MonitorController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<MonitorController> _logger;

    public MonitorController(ILogger<MonitorController> logger, IEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }

    [HttpPost]
    [HttpOptions]
    [Route("events")]
    [ProducesResponseType(typeof(EventResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] AddEventRequest addEventRequest)
    {
        _logger.LogInformation("Received request to add event");
        return Ok(_eventService.AddEvent(addEventRequest.ToEvent()));
    }
}