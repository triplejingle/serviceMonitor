using System.Text.Json;
using Service.Monitor.Domain;

namespace Service.Monitor.Controllers.Requests;

public class AddEventRequest
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; } = String.Empty;
    public string Page { get; set; } = String.Empty;
    public string Action { get; set; } = String.Empty;
    public JsonDocument? Data { get; set; }

    public Event ToEvent()
    {
        return new Event
        {
            User = User,
            BusinessName = BusinessName,
            Page = Page,
            Action = Action,
            Data = Data,
            DateTime = DateTime.UtcNow
        };
    }
}