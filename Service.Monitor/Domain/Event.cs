namespace service_monitor.Domain;

public class Event
{
    public string User { get; set; }
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    public DateTime DateTime { get; set; }
}