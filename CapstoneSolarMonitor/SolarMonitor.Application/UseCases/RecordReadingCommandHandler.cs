using SolarMonitor.Application.Repositories;

namespace SolarMonitor.Application.UseCases;

public class RecordReadingCommandHandler
{
    private readonly IPanelRepository _repository;

    public RecordReadingCommandHandler(IPanelRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(RecordReadingCommand command, CancellationToken ct)
    {
        var panel = await _repository.GetByIdAsync(command.PanelId, ct);
        if (panel is null)
        {
            throw new Exception($"Panel with ID {command.PanelId} not found.");
        }

        panel.RecordReading(command.Watts, command.Voltage);
        await _repository.SaveAsync(panel, ct);

        return true;
    }
}