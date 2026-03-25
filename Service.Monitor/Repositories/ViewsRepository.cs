using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Service.Monitor.Domain;
using Service.Monitor.Repositories.DbModels;
using Service.Monitor.Repositories.Helper;
using Service.Monitor.Repositories.Interfaces;

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
        var defaultData = actionPerBusiness.id;
        var collection = _database
            .GetCollection<ActionsPerBusinessDbModel>(DatabaseProperties.ViewsTable)
            .AsQueryable()
            .Where(a =>
                a._id.BusinessName == defaultData.BusinessName
            );

        collection = collection.Where(a => a._id.Date >= start);
        collection = collection.Where(a => a._id.Date <= end);
        return await collection.Select(a => a.ToActionPerBusinessDbModel()).ToListAsync();
    }
}