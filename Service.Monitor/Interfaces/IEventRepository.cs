using Service.Monitor.Domain;

namespace Service.Monitor.Interfaces;

public interface IEventRepository
{
    Task<Event> Add(Event @event);
    Task<List<ActionsPerBusiness>> GetByMonitor(string businessName);
}