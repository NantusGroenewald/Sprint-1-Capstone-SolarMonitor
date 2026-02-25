using MediatR;
using SolarMonitor.Application.DTOs;
using SolarMonitor.Application.Repositories;

namespace SolarMonitor.Application.Queries;

public class GetAllPanelsQueryHandler : IRequestHandler<GetAllPanelsQuery, IEnumerable<PanelResponse>>
{
    private readonly IPanelRepository _panelRepository;

    public GetAllPanelsQueryHandler(IPanelRepository panelRepository)
    {
        _panelRepository = panelRepository;
    }

    public async Task<IEnumerable<PanelResponse>> Handle(GetAllPanelsQuery request, CancellationToken cancellationToken)
    {
        var panels = await _panelRepository.GetPanelsAsync(cancellationToken);

        var response = panels.Select(p => new PanelResponse
        {
            Id = p.Id,
            Brand = p.Brand,
            Model = p.Model,
            Type = p.Type.ToString()
        });

        return response;
    }
}