using MediatR;
using SolarMonitor.Application.DTOs;

namespace SolarMonitor.Application.Queries;

// We are asking MediatR to return a list of PanelResponse objects
public class GetAllPanelsQuery : IRequest<IEnumerable<PanelResponse>>
{
    // This class is empty because we don't need to pass any parameters to get ALL panels!
}