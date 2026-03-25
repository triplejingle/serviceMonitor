using Service.Monitor.Domain;

namespace Service.Monitor.Repositories.Interfaces;

public interface IEventRepository
{
    Task<Event> Add(Event @event);
}