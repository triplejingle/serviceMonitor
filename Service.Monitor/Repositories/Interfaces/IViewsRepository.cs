using Service.Monitor.Domain;

namespace Service.Monitor.Repositories.Interfaces;

public interface IViewsRepository
{
    Task<List<ActionsPerBusiness>> GetActionsPerBusiness(ActionsPerBusiness actionPerBusiness, DateTime start,
        DateTime end);
}