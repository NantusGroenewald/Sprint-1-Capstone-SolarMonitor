using MediatR;
using SolarMonitor.Application.DTOs;

namespace SolarMonitor.Application.Queries;

public class GetPanelReadingsQuery : IRequest<PagedResult<ReadingResponse>>
{
    public Guid PanelId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public GetPanelReadingsQuery(Guid panelId, int pageNumber = 1, int pageSize = 10)
    {
        PanelId = panelId;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}