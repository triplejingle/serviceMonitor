using Service.Monitor.Controllers.Requests;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Services.Interfaces;

public interface IEventService
{
     Task<EventResponse> AddEvent(AddEventRequest @event);
}