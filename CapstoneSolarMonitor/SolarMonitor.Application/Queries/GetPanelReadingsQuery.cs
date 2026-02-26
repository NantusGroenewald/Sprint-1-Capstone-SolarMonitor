using MediatR;
using SolarMonitor.Application.DTOs;

namespace SolarMonitor.Application.Queries;

public class GetPanelReadingsQuery : IRequest<IEnumerable<ReadingResponse>>
{
    public Guid PanelId { get; set; }

    public GetPanelReadingsQuery(Guid panelId)
    {
        PanelId = panelId;
    }
}