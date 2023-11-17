using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using Service.Monitor.Serializers;

namespace Service.Monitor.Domain;

public class Event
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
}