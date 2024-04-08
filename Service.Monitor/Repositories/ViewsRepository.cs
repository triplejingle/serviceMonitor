using Microsoft.VisualBasic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Service.Monitor.Domain;
using Service.Monitor.Helper;
using Service.Monitor.Interfaces;

namespace Service.Monitor.Repositories;

public class ViewsRepository : IViewsRepository
{
    private readonly IMongoDatabase _database;

    public ViewsRepository()
    {
        var client = new MongoClient(DatabaseProperties.ConnectionString);
        _database = client.GetDatabase(DatabaseProperties.Database);
    }

    public async Task<List<ActionsPerBusiness>> GetActionsPerBusiness(ActionsPerBusiness actionPerBusiness,
        DateTime start, DateTime end)
    {
        var defaultData = actionPerBusiness._id;
        var collection = _database
            .GetCollection<ActionsPerBusiness>(DatabaseProperties.ViewsTable)
            .AsQueryable()
            .Where(a =>
                a._id.BusinessName == defaultData.BusinessName
            );

        collection = collection.Where(a => a._id.Date >= start);
        collection = collection.Where(a => a._id.Date <= end);
        List<ActionsPerBusiness> actionsPerBusinessesCollection = await collection.ToListAsync();

        return AddDefaultDataToBusinessList(actionsPerBusinessesCollection, defaultData, start, end);
    }

    private List<ActionsPerBusiness> AddDefaultDataToBusinessList(List<ActionsPerBusiness> initialList,
        ActionsPerBusinessId defaultData, DateTime start,
        DateTime end)
    {
        DateTime currentDate = start;
        long nrOfDays = DateAndTime.DateDiff(DateInterval.Day, start, end);
        var actionsPerBusinessList = new List<ActionsPerBusiness>();
        for (int i = 0; i < nrOfDays; i++)
        {
            var actionsPerBusiness = initialList.FirstOrDefault(a => a._id.Date.Date == currentDate.Date);
            if (actionsPerBusiness == null)
            {
                actionsPerBusiness = new ActionsPerBusiness();
                actionsPerBusiness._id = defaultData.ShallowCopy();
            }

            actionsPerBusiness._id.Date = currentDate;
            actionsPerBusinessList.Add(actionsPerBusiness);
            currentDate = currentDate.AddDays(1);
        }

        return actionsPerBusinessList;
    }
}