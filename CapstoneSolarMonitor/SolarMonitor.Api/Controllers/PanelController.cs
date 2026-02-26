using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolarMonitor.Application.Commands;
using SolarMonitor.Application.Queries;
namespace SolarMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PanelsController : ControllerBase
{
    private readonly ISender _mediator;

    public PanelsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePanel([FromBody] CreatePanelCommand command)
    {
        var panelId = await _mediator.Send(command);

        return Ok(new { Id = panelId, Message = "Panel successfully registered via MediatR!" });
    }

    [HttpPost("{id}/readings")]
    public async Task<IActionResult> RecordReading(Guid id, [FromBody] RecordReadingCommand command, CancellationToken cancellationToken)
    {
        command.PanelId = id; 
        try
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new {Message = "Reading successfully recorded via MediatR!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPanels(CancellationToken cancellationToken)
    {
        var query = new GetAllPanelsQuery();
        var panels = await _mediator.Send(query, cancellationToken);
        return Ok(panels);
    }


    [HttpGet("{id}/readings")]
    public async Task<IActionResult> GetPanelReadings(Guid id)
    {
        var query = new GetPanelReadingsQuery(id);

        var readings = await _mediator.Send(query);

        return Ok(readings);
    }
}