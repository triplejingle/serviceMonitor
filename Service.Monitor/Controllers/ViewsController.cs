using Microsoft.AspNetCore.Mvc;
using Service.Monitor.Interfaces;
using Service.Monitor.Models;

namespace Service.Monitor.Controllers;

[ApiController]
[Route("/v1")]
[Produces("application/json")]
public class ViewsController : ControllerBase
{
    private readonly ILogger<MonitorController> _logger;
    private readonly IViewsRepository _viewsRepository;

    public ViewsController(IViewsRepository viewsRepository)
    {
        _viewsRepository = viewsRepository;
    }

    [HttpPost]
    [HttpOptions]
    [Route("views/actions")]
    [ProducesResponseType(typeof(GetGraphDataCollectionModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetActionsPerBusiness(
        [FromBody] GetActionPerBusinessModel getActionPerBusinessModel)
    {
        var actionPerBusiness = getActionPerBusinessModel.ToActionPerBusinessModel();
        var actionPerBusinessList = await _viewsRepository.GetActionsPerBusiness(actionPerBusiness,
            getActionPerBusinessModel.Start, getActionPerBusinessModel.End);

        var getActionPerBusinessCollectionModel = new GetGraphDataCollectionModel(actionPerBusinessList);
        GraphData graphData = new GraphData(actionPerBusinessList, getActionPerBusinessModel.Page,
            getActionPerBusinessModel.Action);
        getActionPerBusinessCollectionModel.Data.Add(graphData);

        return Ok(getActionPerBusinessCollectionModel);
    }
}