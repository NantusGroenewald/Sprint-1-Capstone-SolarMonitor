using MediatR;
using SolarMonitor.Domain.Enums;

namespace SolarMonitor.Application.Commands;

public class CreatePanelCommand : IRequest<Guid>
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public PanelType Type { get; set; }
}