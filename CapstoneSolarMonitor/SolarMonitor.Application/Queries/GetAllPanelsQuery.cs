using MediatR;
using SolarMonitor.Application.DTOs;

namespace SolarMonitor.Application.Queries;

public class GetAllPanelsQuery : IRequest<IEnumerable<PanelResponse>>
{

}