using Service.Monitor.Domain;

namespace Service.Monitor.Interfaces;

public interface IViewsRepository
{
    Task<List<ActionsPerBusiness>> GetActionsPerBusiness(
        string businessName,
        string? page,
        string? action,
        DateTime? start,
        DateTime? end
    );
}