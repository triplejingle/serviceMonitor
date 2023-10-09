using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using service_monitor.Domain;
using service_monitor.Interfaces;
using service_monitor.Models;

namespace service_monitor.Controllers;

[ApiController]
public class MonitorController : ControllerBase
{
    private readonly ILogger<MonitorController> _logger;
    private readonly IEventRepository _eventRepository;

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