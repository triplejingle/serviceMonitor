using Service.Monitor.Domain;
using Service.Monitor.Repositories.Interfaces;
using Service.Monitor.Services.Interfaces;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Services;
// code below is added to maintain architecture consistency. (remove comment when this code gets more complex)
public class EventService:IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<EventResponse> AddEvent(Event @event)
    {
        var @addedEvent = await _eventRepository.Add(@event);
        return new EventResponse(@addedEvent);
    }
}