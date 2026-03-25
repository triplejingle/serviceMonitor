using Microsoft.VisualBasic;
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
    
    public async Task<GetGraphDataCollectionResponse> GetActionsPerBusiness(ActionsPerBusiness actionPerBusiness, DateTime start, DateTime end, string action, string page)
    {
        List<ActionsPerBusiness> initialList = await _viewsRepository.GetActionsPerBusiness(actionPerBusiness, actionPerBusiness.id.Date, actionPerBusiness.id.Date);
        List<ActionsPerBusiness> filledInList =  FillInMissingDays(initialList, actionPerBusiness, start, end);
        
        return GetActionPerBusinessCollectionModel(action, page, filledInList);;
    }
    
    private List<ActionsPerBusiness> FillInMissingDays( List<ActionsPerBusiness> initialList, ActionsPerBusiness actionPerBusiness, DateTime dateTime, DateTime end)
    {
        DateTime currentDate = dateTime;
        long nrOfDays = DateAndTime.DateDiff(DateInterval.Day, dateTime, end);
        var actionsPerBusinesses = new List<ActionsPerBusiness>();
        for (int i = 0; i < nrOfDays; i++)
        {
            var actionsPerBusiness = initialList.FirstOrDefault(a => a.id.Date.Date == currentDate.Date);
            if (actionsPerBusiness == null)
            {
                var id = actionPerBusiness.id.ShallowCopy();
                actionsPerBusiness = new ActionsPerBusiness(id);
            }

            actionsPerBusiness.id.Date = currentDate;
            actionsPerBusinesses.Add(actionsPerBusiness);
            currentDate = currentDate.AddDays(1);
        }
       return actionsPerBusinesses;
    }
    
    private static GetGraphDataCollectionResponse GetActionPerBusinessCollectionModel(string action, string page, List<ActionsPerBusiness> filledInList)
    {
        var getActionPerBusinessCollectionModel = new GetGraphDataCollectionResponse(filledInList);
        getActionPerBusinessCollectionModel.AddGraphDataData(filledInList, action, page);
        return getActionPerBusinessCollectionModel;
    }
}