using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using Service.Monitor.Domain;
using Service.Monitor.Repositories.Serializers;

namespace Service.Monitor.Repositories.DbModels;

public class EventDbModel
{
    [BsonIgnoreIfNull]
    public string User { get; set; } = String.Empty;
    [BsonIgnoreIfNull]
    public string BusinessName { get; set; }
    [BsonIgnoreIfNull]
    public string Page { get; set; }
    [BsonIgnoreIfNull]
    public string Action { get; set; }
    [BsonSerializer(typeof(JsonDocumentSerializer))]
    [BsonIgnoreIfNull]
    public JsonDocument? Data { get; set; }
    public DateTime DateTime { get; set; }
    
    public EventDbModel(Event @event)
    {
        User = @event.User;
        BusinessName = @event.BusinessName;
        Page = @event.Page;
        Action = @event.Action;
        Data = @event.Data;
        DateTime = @event.DateTime;
    }
}