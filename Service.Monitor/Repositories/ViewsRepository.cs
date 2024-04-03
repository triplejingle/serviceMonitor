using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Service.Monitor.Domain;
using Service.Monitor.Interfaces;

namespace Service.Monitor.Repositories;

public class ViewsRepository : IViewsRepository
{
    private const string Database = "monitor";
    private const string ActionsPerBusinessView = "ActionsPerBusiness";
    private readonly IMongoDatabase _database;

    public ViewsRepository()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION"));
        _database = client.GetDatabase(Database);
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
            .GetCollection<ActionsPerBusiness>(ActionsPerBusinessView)
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