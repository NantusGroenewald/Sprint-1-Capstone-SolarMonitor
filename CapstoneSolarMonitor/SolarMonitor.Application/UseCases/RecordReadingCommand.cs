namespace SolarMonitor.Application.UseCases;

public record RecordReadingCommand(Guid PanelId, double Watts, double Voltage);