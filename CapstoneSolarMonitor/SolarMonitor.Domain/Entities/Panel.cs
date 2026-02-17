using SolarMonitor.Domain.Enums;
using SolarMonitor.Domain.ValueObjects;

namespace SolarMonitor.Domain.Entities;

public class Panel
{
    public Guid Id { get; private set; }

    public string Brand { get; private set; }
    public string Model { get; private set; }
    public PanelType Type { get; private set; }
    public DateTime InstallationDate { get; private set; }

    private readonly List<Reading> _readings = new();
    public IReadOnlyCollection<Reading> Readings => _readings.AsReadOnly();

    public Panel(string brand, string model, PanelType type)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be empty.");

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty.");

        Id = Guid.NewGuid();
        Brand = brand;
        Model = model;
        Type = type;
        InstallationDate = DateTime.UtcNow;
    }

    public void RecordReading(double watts, double voltage)
    {
        if (watts < 0)
            throw new ArgumentException("Solar panels cannot generate negative watts (unless they are broken black holes).");

        if (voltage < 0)
            throw new ArgumentException("Voltage cannot be negative.");

        var reading = new Reading(watts, voltage, DateTime.UtcNow);
        _readings.Add(reading);
    }
}