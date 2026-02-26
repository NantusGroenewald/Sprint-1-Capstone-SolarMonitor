using MediatR;
using SolarMonitor.Application.DTOs;
using SolarMonitor.Application.Repositories;

namespace SolarMonitor.Application.Queries;

public class GetPanelReadingsQueryHandler : IRequestHandler<GetPanelReadingsQuery, IEnumerable<ReadingResponse>>
{
    private readonly IPanelRepository _panelRepository;

    public GetPanelReadingsQueryHandler(IPanelRepository panelRepository)
    {
        _panelRepository = panelRepository;
    }

    public async Task<IEnumerable<ReadingResponse>> Handle(GetPanelReadingsQuery request, CancellationToken cancellationToken)
    {
        var panel = await _panelRepository.GetByIdAsync(request.PanelId, cancellationToken);

        if (panel == null)
        {
            return Enumerable.Empty<ReadingResponse>();
        }

        var response = panel.Readings
            .OrderByDescending(r => r.Timestamp)
            .Select(r => new ReadingResponse
            {
                Watts = r.Watts,
                Voltage = r.Voltage,
                Timestamp = r.Timestamp
            });

        return response;
    }
}