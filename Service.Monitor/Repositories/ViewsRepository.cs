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

    public async Task<List<ActionsPerBusiness>> GetActionsPerBusiness(
        string businessName,
        string? page,
        string? action,
        DateTime? start = null,
        DateTime? end = null
    )
    {
        var collection = _database
            .GetCollection<ActionsPerBusiness>(DatabaseProperties.ViewsTable)
            .AsQueryable()
            .Where(a => a._id.BusinessName == businessName);

        if (page != null)
        {
            collection = collection.Where(a => a._id.Page == page);
        }

        if (action != null)
        {
            collection = collection.Where(a => a._id.Action == action);
        }

        if (start != null)
        {
            collection = collection.Where(a => a._id.Date >= start);
        }

        if (end != null)
        {
            collection = collection.Where(a => a._id.Date <= end);
        }

        return await collection.ToListAsync();
    }
}