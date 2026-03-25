using MongoDB.Driver;
using Service.Monitor.Domain;
using Service.Monitor.Repositories.DbModels;
using Service.Monitor.Repositories.Helper;
using Service.Monitor.Repositories.Interfaces;

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
        var eventDbModel = new EventDbModel(@event);
        var collection = _database.GetCollection<EventDbModel>(DatabaseProperties.EventsTable);
         await collection.InsertOneAsync(@eventDbModel);
        return @event;
    }
}