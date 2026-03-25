using Service.Monitor.Domain;

namespace Service.Monitor.Controllers.Requests;

public class GetActionPerBusinessRequest
{
    public string BusinessName { get; set; } = String.Empty;
    public string Page { get; set; } = String.Empty;
    public string Action { get; set; } = String.Empty;
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime End { get; set; } = DateTime.Now;

    public ActionsPerBusiness ToActionPerBusinessModel()
    {
        return new ActionsPerBusiness
        (
           new ActionsPerBusinessId
            (
                BusinessName = BusinessName,
                Action = Action,
                Page = Page
            )
        );
    }
}