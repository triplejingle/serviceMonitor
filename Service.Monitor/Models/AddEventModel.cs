using Service.Monitor.Domain;

namespace Service.Monitor.Models;

public class AddEventModel
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    
    public Dictionary<string, object>? Data { get; set; }

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