using Microsoft.AspNetCore.Mvc;
using Service.Monitor.Domain;
using Service.Monitor.Interfaces;
using Service.Monitor.Models;

namespace Service.Monitor.Controllers;

[ApiController]
[Route("/v1")]
public class MonitorController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    private readonly ILogger<MonitorController> _logger;

    public MonitorController(ILogger<MonitorController> logger, IEventRepository eventRepository)
    {
        _logger = logger;
        _eventRepository = eventRepository;
    }

    [HttpPost]
    [HttpOptions]
    [Route("events")]
    [ProducesResponseType(typeof(Event), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] AddEventModel addEventModel)
    {
        var @event = await _eventRepository.Add(addEventModel.ToEvent());
        return Ok(@event);
    }
}