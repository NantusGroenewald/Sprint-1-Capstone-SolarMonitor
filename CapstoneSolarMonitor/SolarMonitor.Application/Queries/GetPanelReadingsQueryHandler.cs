using MediatR;
using SolarMonitor.Application.DTOs;
using SolarMonitor.Application.Repositories;

namespace SolarMonitor.Application.Queries;

public class GetPanelReadingsQueryHandler : IRequestHandler<GetPanelReadingsQuery, PagedResult<ReadingResponse>>
{
    private readonly IPanelRepository _panelRepository;

    public GetPanelReadingsQueryHandler(IPanelRepository panelRepository)
    {
        _panelRepository = panelRepository;
    }

    public async Task<PagedResult<ReadingResponse>> Handle(GetPanelReadingsQuery request, CancellationToken cancellationToken)
    {

        var (readings, totalCount) = await _panelRepository.GetPagedReadingsAsync(request.PanelId, request.PageNumber, request.PageSize, cancellationToken);

        var items = readings.Select(r => new ReadingResponse
        {
            Watts = r.Watts,
            Voltage = r.Voltage,
            Timestamp = r.Timestamp
        }).ToList();

        return new PagedResult<ReadingResponse>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}