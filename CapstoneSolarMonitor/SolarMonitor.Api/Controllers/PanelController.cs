using Microsoft.AspNetCore.Mvc;
using SolarMonitor.Application.UseCases;

namespace SolarMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PanelsController : ControllerBase
{
    private readonly RecordReadingCommandHandler _handler;

    public PanelsController(RecordReadingCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("{id}/readings")]
    public async Task<IActionResult> RecordReading(Guid id, [FromBody] ReadingRequest request, CancellationToken ct)
    {
        var command = new RecordReadingCommand(id, request.Watts, request.Voltage);

        try
        {
            await _handler.HandleAsync(command, ct);
            return Ok(new { Message = "Reading recorded successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}

public record ReadingRequest(double Watts, double Voltage);