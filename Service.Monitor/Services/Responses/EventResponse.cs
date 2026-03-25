using System.Text.Json;
using Service.Monitor.Domain;

namespace Service.Monitor.Services.Responses;

public class EventResponse
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    public JsonDocument? Data { get; set; }
    public DateTime DateTime { get; set; }
    public EventResponse(Event @event)
    {
        User = @event.User;
        BusinessName = @event.BusinessName;
        Page = @event.Page;
        Action = @event.Action;
        Data = @event.Data;
        DateTime = @event.DateTime;
    }
}