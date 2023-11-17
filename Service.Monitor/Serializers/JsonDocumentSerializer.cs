using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Service.Monitor.Extensions;

namespace Service.Monitor.Serializers;

public class JsonDocumentSerializer : SerializerBase<JsonDocument>
{
    public override JsonDocument Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var myBsonDoc = BsonDocumentSerializer.Instance.Deserialize(context);
        return JsonDocument.Parse(myBsonDoc.ToString() ?? string.Empty);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, JsonDocument? value)
    {
        var myBsonDoc = BsonDocument.Parse(value?.ToJsonString());
        BsonDocumentSerializer.Instance.Serialize(context, myBsonDoc);
    }
}