namespace Service.Monitor.Domain;

public class ActionsPerBusiness
{
    public ActionsPerBusinessId _id { get; set; }
    public int count { get; set; }
}

public class ActionsPerBusinessId
{
    public string BusinessName { get; set; }
    public string Action { get; set; }
    public string Page { get; set; }
    public DateTime Date { get; set; }
}