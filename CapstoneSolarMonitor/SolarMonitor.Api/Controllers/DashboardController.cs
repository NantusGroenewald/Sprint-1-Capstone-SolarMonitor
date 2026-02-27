using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolarMonitor.Application.Queries;

namespace SolarMonitor.Api.Controllers
{
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
        public async Task<IActionResult> GetSystemSummary(CancellationToken cancellationToken) 
        {
            var query = new GetDashboardSummaryQuery();
            var summary = await _mediator.Send(query, cancellationToken);

            return Ok(summary);
        }
    }
}
