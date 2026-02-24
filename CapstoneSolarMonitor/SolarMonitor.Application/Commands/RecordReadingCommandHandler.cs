using MediatR;
using SolarMonitor.Application.Repositories;

namespace SolarMonitor.Application.Commands;

public class RecordReadingCommandHandler : IRequestHandler<RecordReadingCommand, bool>
{
    private readonly IPanelRepository _repository;

    public RecordReadingCommandHandler(IPanelRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RecordReadingCommand command, CancellationToken ct)
    {
        var panel = await _repository.GetByIdAsync(command.PanelId, ct);
        if (panel is null)
        {
            throw new Exception($"Panel with ID {command.PanelId} not found.");
        }

        panel.RecordReading(command.Watts, command.Voltage);
        await _repository.UpdateAsync(panel, ct);

        return true;
    }
}