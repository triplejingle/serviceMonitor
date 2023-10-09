using service_monitor.Domain;

namespace service_monitor.Models;

public class AddEventModel
{
    public string User { get; set; }
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    public DateTime DateTime { get; set; }

    public Event ToEvent()
    {
        return new Event
        {
            User = User,
            BusinessName = BusinessName,
            Page = Page,
            Action = Action,
            DateTime = DateTime
        };
    }
}