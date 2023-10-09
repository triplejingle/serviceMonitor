using service_monitor.Domain;

namespace service_monitor.Interfaces;

public interface IEventRepository
{
    Task<Event> Add(Event @event);
}