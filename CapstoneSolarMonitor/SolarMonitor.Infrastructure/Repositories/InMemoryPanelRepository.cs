using System.Collections.Concurrent;
using SolarMonitor.Application.Repositories;
using SolarMonitor.Domain.Entities;
using SolarMonitor.Domain.Enums;

namespace SolarMonitor.Infrastructure.Repositories;

public class InMemoryPanelRepository : IPanelRepository
{
    private readonly ConcurrentDictionary<Guid, Panel> _db = new();

    public InMemoryPanelRepository()
    {
        var fixedId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var seedPanel = new Panel("Tesla", "Solar Roof", PanelType.Monocrystalline);

        typeof(Panel).GetProperty("Id")
                     .SetValue(seedPanel, fixedId);

        _db.TryAdd(seedPanel.Id, seedPanel);

        Console.WriteLine($"\n[DB SEED] Repository Initialized. Panel ID: {fixedId}\n");
    }

    public Task<Panel> GetByIdAsync(Guid id, CancellationToken ct)
    {
        _db.TryGetValue(id, out var panel);
        return Task.FromResult(panel);
    }

    public Task SaveAsync(Panel panel, CancellationToken ct)
    {
        _db[panel.Id] = panel;
        return Task.CompletedTask; 
    }
}