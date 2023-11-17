namespace Service.Monitor.Domain;

public class Event
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    public Dictionary<string, object>? Data { get; set; }
    public DateTime DateTime { get; set; }
}