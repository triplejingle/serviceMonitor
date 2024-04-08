using Service.Monitor.Domain;

namespace Service.Monitor.Models;

public class GetGraphDataCollectionModel
{
    public GetGraphDataCollectionModel(List<ActionsPerBusiness> actionPerBusinessList)
    {
        foreach (var actionsPerBusiness in actionPerBusinessList)
        {
            Labels.Add(actionsPerBusiness._id.Date.ToString());
        }
    }

    public List<string> Labels { get; set; } = new();
    public List<GraphData> Data { get; set; } = new();
}

public class GraphData
{
    public GraphData(List<ActionsPerBusiness> actionPerBusinessIdList, string page, string action)
    {
        Total = new List<int>();
        Label = page + " " + action;
        foreach (var actionsPerBusinessId in actionPerBusinessIdList)
        {
            Total.Add(actionsPerBusinessId.Count);
        }
    }

    public string Label { get; set; }
    public List<int> Total { get; set; }
}