using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolarMonitor.Application.Queries;
using SolarMonitor.Api.Filters;

namespace SolarMonitor.Api.Controllers
{
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ISender _mediator;

        public DashboardController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSystemSummary(
            [FromHeader(Name = "X-API-KEY")] string apiKey,
            CancellationToken cancellationToken) 
        {
            var query = new GetDashboardSummaryQuery();
            var summary = await _mediator.Send(query, cancellationToken);

            return Ok(summary);
        }
    }
}
