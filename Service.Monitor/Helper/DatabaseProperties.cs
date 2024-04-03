namespace Service.Monitor.Helper;

public class DatabaseProperties
{
    public static readonly string Database = "monitor";
    public static readonly string? ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
    public static readonly string ViewsTable = "ActionsPerBusiness";
    public static readonly string EventsTable = "monitor";
}