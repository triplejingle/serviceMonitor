using Service.Monitor.Domain;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Services.Interfaces;

public interface IViewService
{
     Task<GetGraphDataCollectionResponse> GetActionsPerBusiness(ActionsPerBusiness actionPerBusiness, DateTime start, DateTime end, string action, string page);
}