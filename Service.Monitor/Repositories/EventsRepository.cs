using MongoDB.Driver;
using Service.Monitor.Domain;
using Service.Monitor.Helper;
using Service.Monitor.Interfaces;

namespace Service.Monitor.Repositories;

public class EventsRepository : IEventRepository
{
    private readonly IMongoDatabase _database;

    public EventsRepository()
    {
        var client = new MongoClient(DatabaseProperties.ConnectionString);
        _database = client.GetDatabase(DatabaseProperties.Database);
    }

    public async Task<Event> Add(Event @event)
    {
        var collection = _database.GetCollection<Event>(DatabaseProperties.EventsTable);
        await collection.InsertOneAsync(@event);
        return @event;
    }
}