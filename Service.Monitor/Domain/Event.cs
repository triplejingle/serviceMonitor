using System.Text.Json;

namespace Service.Monitor.Domain;

public class Event
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; } = String.Empty;
    public string Page { get; set; } = String.Empty;
    public string Action { get; set; } = String.Empty;
    public JsonDocument? Data { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}