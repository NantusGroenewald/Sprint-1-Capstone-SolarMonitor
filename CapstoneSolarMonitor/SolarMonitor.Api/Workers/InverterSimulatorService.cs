using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SolarMonitor.Application.Repositories;
using Microsoft.AspNetCore.SignalR;
using SolarMonitor.Api.Hubs;

namespace SolarMonitor.Api.Services;

public class InverterSimulatorService : BackgroundService
{
    private readonly ILogger<InverterSimulatorService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<SolarHub> _hubContext;

    public InverterSimulatorService(ILogger<InverterSimulatorService> logger, IServiceProvider serviceProvider, IHubContext<SolarHub> hubContext)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Deye Inverter Simulator started.");

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var panelRepository = scope.ServiceProvider.GetRequiredService<IPanelRepository>();
                var panels = await panelRepository.GetPanelsAsync(stoppingToken);
                var panelList = panels.ToList();

                if (!panelList.Any())
                {
                    _logger.LogWarning("Simulator is running, but no panels are registered in the database!");
                    continue;
                }

                var random = new Random();

                foreach (var panel in panelList)
                {
                    double simulatedWatts = Math.Round(random.NextDouble() * 545, 2);
                    double simulatedVoltage = Math.Round(41.0 + (random.NextDouble() * 2), 2);

                    panel.RecordReading(simulatedWatts, simulatedVoltage);
                    await panelRepository.UpdateAsync(panel, stoppingToken);

                    var liveData = new
                    {
                        PanelId = panel.Id,
                        Watts = simulatedWatts,
                        Voltage = simulatedVoltage,
                        Timestamp = DateTime.UtcNow
                    };

                    await _hubContext.Clients.All.SendAsync("ReceiveLiveReading", liveData, stoppingToken);
                }

                _logger.LogInformation("Simulated telemetry recorded and broadcasted for {Count} panels at {Time}", panelList.Count, DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error simulating inverter data.");
            }
        }
    }
}