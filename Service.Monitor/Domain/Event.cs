using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using Service.Monitor.Serializers;

namespace Service.Monitor.Domain;

public class Event
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    [BsonSerializer(typeof(JsonDocumentSerializer))]
    public JsonDocument? Data { get; set; }
    public DateTime DateTime { get; set; }
}