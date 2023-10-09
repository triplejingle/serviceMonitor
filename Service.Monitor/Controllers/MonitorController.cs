using Microsoft.AspNetCore.Mvc;
using service_monitor.Interfaces;
using service_monitor.Models;

namespace service_monitor.Controllers;

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

    [HttpPost("events")]
    public async Task<IActionResult> Post([FromBody] AddEventModel addEventModel)
    {
        try
        {
            var @event = await _eventRepository.Add(addEventModel.ToEvent());
            return Ok(@event);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}