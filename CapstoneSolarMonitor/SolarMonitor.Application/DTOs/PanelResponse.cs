namespace SolarMonitor.Application.DTOs;

public class PanelResponse
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Returning as a string is easier for UI dashboards to read
}