using MongoDB.Bson.Serialization.Attributes;
using Service.Monitor.Domain;

namespace Service.Monitor.Repositories.DbModels;

public class ActionsPerBusinessDbModel
{
    //_id attribute is because of the view to change this the pipeline of the view has to be updated.
    [BsonId]
    public ActionsPerBusinessIdDbModel _id { get; set; }  
    
    [BsonElement("count")]
    public int Count { get; set; }

    public ActionsPerBusiness ToActionPerBusinessDbModel()
    {
        var id = new ActionsPerBusinessId(_id.BusinessName,_id.Action,_id.Page);
        id.Date = _id.Date;
        
        var actionsPerBusiness = new ActionsPerBusiness(id);
        actionsPerBusiness.Count = Count;
        
        return actionsPerBusiness;
    }
}

public class ActionsPerBusinessIdDbModel
{
    public ActionsPerBusinessIdDbModel(DateTime date)
    {
        Date = date;
    }

    [BsonElement("BusinessName")] public string BusinessName { get; set; } = "";
    
    [BsonElement("Action")]
    public string Action { get; set; } = "";
    
    [BsonElement("Page")]
    public string Page { get; set; } = "";
    
    [BsonElement("Date")]
    public DateTime Date { get; set; }
}