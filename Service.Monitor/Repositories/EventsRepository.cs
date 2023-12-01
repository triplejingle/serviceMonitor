using MongoDB.Driver;
using Service.Monitor.Domain;
using Service.Monitor.Interfaces;

namespace Service.Monitor.Repositories;

public class EventsRepository : IEventRepository
{
    private const string Database = "monitor";
    private const string Collection = "monitor";
    private const string ActionsPerBusinessView = "ActionsPerBusiness";
    private readonly IMongoDatabase _database;

    public EventsRepository()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION"));
        _database = client.GetDatabase(Database);
    }

    public async Task<Event> Add(Event @event)
    {
        var collection = _database.GetCollection<Event>(Collection);
        await collection.InsertOneAsync(@event);
        return @event;
    }

    public async Task<List<ActionsPerBusiness>> GetByMonitor(string businessName)
    {
        var collection = _database.GetCollection<ActionsPerBusiness>(ActionsPerBusinessView);
        var cursor = await collection.FindAsync(g => g._id.BusinessName == businessName);
        var result = await cursor.ToListAsync();
        return result;
    }
}