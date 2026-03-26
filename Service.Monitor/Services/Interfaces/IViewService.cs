using Service.Monitor.Controllers.Requests;
using Service.Monitor.Domain;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Services.Interfaces;

public interface IViewService
{
    Task<GetGraphDataCollectionResponse> GetActionsPerBusiness(GetActionPerBusinessRequest getActionPerBusinessRequest);
}