namespace Service.Monitor.Domain;

public class ActionsPerBusiness
{
    //_id attribute is because of the view to change this the pipeline of the view has to be updated.
    public ActionsPerBusinessId _id { get; set; }
    public int Count { get; set; }
}

public class ActionsPerBusinessId
{
    public string BusinessName { get; set; }
    public string Action { get; set; }
    public string Page { get; set; }
    public DateTime Date { get; set; }

    public ActionsPerBusinessId ShallowCopy()
    {
        return (ActionsPerBusinessId)this.MemberwiseClone();
    }
}