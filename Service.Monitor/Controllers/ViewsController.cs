using Microsoft.AspNetCore.Mvc;
using Service.Monitor.Controllers.Requests;
using Service.Monitor.Services.Interfaces;
using Service.Monitor.Services.Responses;

namespace Service.Monitor.Controllers;

[ApiController]
[Route("/v1")]
[Produces("application/json")]
public class ViewsController : ControllerBase
{
    private readonly ILogger<MonitorController> _logger;
    private readonly IViewService _viewsService;

    public ViewsController(IViewService viewsService)
    {
        _viewsService = viewsService;
    }

    [HttpPost]
    [HttpOptions]
    [Route("views/actions")]
    [ProducesResponseType(typeof(GetGraphDataCollectionResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetActionsPerBusiness(
        [FromBody] GetActionPerBusinessRequest getActionPerBusinessRequest)
    {
        _logger.LogInformation("Received request to get actions per business");
        var actionPerBusinessList = await _viewsService.GetActionsPerBusiness(getActionPerBusinessRequest);
        return Ok(actionPerBusinessList);
    }
}