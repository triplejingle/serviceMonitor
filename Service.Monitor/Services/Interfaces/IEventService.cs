using Service.Monitor.Domain;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Services.Interfaces;

public interface IEventService
{
     Task<EventResponse> AddEvent(Event @event);
}