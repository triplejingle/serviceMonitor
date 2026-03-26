using Microsoft.VisualBasic;
using Service.Monitor.Controllers.Requests;
using Service.Monitor.Domain;
using Service.Monitor.Repositories.Interfaces;
using Service.Monitor.Services.Interfaces;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Services;

public class ViewService:IViewService
{
    private readonly IViewsRepository _viewsRepository;

    public ViewService(IViewsRepository viewsRepository)
    {
        _viewsRepository = viewsRepository;
    }
    public async Task<GetGraphDataCollectionResponse> GetActionsPerBusiness(GetActionPerBusinessRequest getActionPerBusinessRequest)
    {
        var actionPerBusiness = getActionPerBusinessRequest.ToActionPerBusinessModel();
        var startDate = getActionPerBusinessRequest.Start;
        var endDate = getActionPerBusinessRequest.End;
        var action = getActionPerBusinessRequest.Action;
        var page = getActionPerBusinessRequest.Page;
        
        List<ActionsPerBusiness> initialList = await _viewsRepository.GetActionsPerBusiness(actionPerBusiness, startDate, endDate);
        List<ActionsPerBusiness> filledInList =  FillInMissingDays(initialList, actionPerBusiness,startDate, endDate);

        return GetActionPerBusinessCollectionModel(filledInList, action, page);
    }
    
    private List<ActionsPerBusiness> FillInMissingDays( List<ActionsPerBusiness> initialList, ActionsPerBusiness actionPerBusiness,DateTime startDate,
        DateTime endDate)
    {
        long nrOfDays = DateAndTime.DateDiff(DateInterval.Day, startDate, endDate);
        var actionsPerBusinesses = new List<ActionsPerBusiness>();
        for (int i = 0; i < nrOfDays; i++)
        {
            var actionsPerBusiness = initialList.FirstOrDefault(a => a.id.Date.Date == startDate.Date);
            if (actionsPerBusiness == null)
            {
                var id = actionPerBusiness.id.ShallowCopy();
                actionsPerBusiness = new ActionsPerBusiness(id);
                actionsPerBusiness.id.Date = startDate;
            }
            
            actionsPerBusinesses.Add(actionsPerBusiness);
            startDate = startDate.AddDays(1);
        }
       return actionsPerBusinesses;
    }
    
    private GetGraphDataCollectionResponse GetActionPerBusinessCollectionModel(List<ActionsPerBusiness> filledInList, string Action, string Page)
    {
        var getActionPerBusinessCollectionModel = new GetGraphDataCollectionResponse(filledInList);
        getActionPerBusinessCollectionModel.AddGraphDataData(filledInList,Action, Page);
        return getActionPerBusinessCollectionModel;
    }

}