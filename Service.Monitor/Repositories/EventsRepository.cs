using MongoDB.Driver;
using Service.Monitor.Domain;
using Service.Monitor.Interfaces;

namespace Service.Monitor.Repositories;

public class EventsRepository : IEventRepository
{
    private const string Database = "monitor";
    private const string Collection = "monitor";
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
}