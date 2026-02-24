using MediatR;
using SolarMonitor.Domain.Enums;

namespace SolarMonitor.Application.Commands;

public class RecordReadingCommand : IRequest<bool   >
{
    public Guid PanelId { get; set; }
    public double Watts { get; set; }
    public double Voltage { get; set; }
}