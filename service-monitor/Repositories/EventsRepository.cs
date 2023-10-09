using MongoDB.Driver;
using service_monitor.Domain;
using service_monitor.Interfaces;

namespace service_monitor.repository;

public class EventsRepository: IEventRepository
{
    private readonly MongoClient MongoClient;
    private readonly IMongoDatabase database;
    private const string _database = "monitor";
    private const string _collection = "monitor";
    
    public EventsRepository()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION")); 
        database = client.GetDatabase(_database);
    }
    
    public async Task<Event> Add(Event @event)
    {
        var collection = database.GetCollection<Event>(_collection);
        await collection.InsertOneAsync(@event);
        return @event;
    }
}