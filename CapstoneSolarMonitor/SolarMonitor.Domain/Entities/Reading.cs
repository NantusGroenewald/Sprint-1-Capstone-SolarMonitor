namespace SolarMonitor.Domain.Entities;

public class Reading
{
    public Guid Id { get; private set; }
    public Guid PanelId { get; private set; } // The foreign key linking back to the Panel
    public double Watts { get; private set; }
    public double Voltage { get; private set; }
    public DateTime Timestamp { get; private set; }

    // Required by EF Core
    private Reading() { }

    public Reading(Guid panelId, double watts, double voltage)
    {
        Id = Guid.NewGuid();
        PanelId = panelId;
        Watts = watts;
        Voltage = voltage;
        Timestamp = DateTime.UtcNow;
    }
}