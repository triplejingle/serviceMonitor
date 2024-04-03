using Service.Monitor.Domain;

namespace Service.Monitor.Interfaces;

public interface IViewsRepository
{
    Task<List<ActionsPerBusiness>> GetActionsPerBusiness(ActionsPerBusiness actionPerBusiness, DateTime start,
        DateTime end);
}