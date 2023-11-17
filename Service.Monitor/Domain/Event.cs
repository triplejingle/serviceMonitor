namespace Service.Monitor.Domain;

public class Event
{
    public string User { get; set; } = String.Empty;
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    public IEventData? Data { get; set; }
    public DateTime DateTime { get; set; }
}


public interface IEventData
{
    
}

public class EventUtmData : IEventData
{
    public string? UtmId { get; set; }
    public string UtmSource { get; set; } = string.Empty;
    public string UtmMedium { get; set; } = string.Empty;
    public string? UtmCampaign { get; set; }
    public string? UtmTerm { get; set; }
    public string? UtmContent { get; set; }
}