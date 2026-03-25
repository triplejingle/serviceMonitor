namespace Service.Monitor.Domain;

public class ActionsPerBusiness
{
    public ActionsPerBusiness(ActionsPerBusinessId id)
    {
        this.id = id;
    }

    public ActionsPerBusinessId id { get; set; }
    public int Count { get; set; } = 0;
}

public class ActionsPerBusinessId
{
    public ActionsPerBusinessId(string businessName, string action, string page)
    {
        BusinessName = businessName;
        Action = action;
        Page = page;
    }

    public string BusinessName { get; set; }
    public string Action { get; set; }
    public string Page { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    public ActionsPerBusinessId ShallowCopy()
    {
        return (ActionsPerBusinessId)this.MemberwiseClone();
    }
}