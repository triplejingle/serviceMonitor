using Service.Monitor.Domain;

namespace Service.Monitor.Models;

public class AddEventModel
{
    public string User { get; set; } = string.Empty;
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    
    public IEventData? Data { get; set; }

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