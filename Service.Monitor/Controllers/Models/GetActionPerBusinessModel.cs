using Service.Monitor.Domain;

namespace Service.Monitor.Models;

public class GetActionPerBusinessModel
{
    public string BusinessName { get; set; }
    public string Page { get; set; }
    public string Action { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public ActionsPerBusiness ToActionPerBusinessModel()
    {
        return new ActionsPerBusiness
        {
            _id = new ActionsPerBusinessId
            {
                BusinessName = BusinessName,
                Action = Action,
                Page = Page,
                Date = default
            },
            Count = 0
        };
    }
}