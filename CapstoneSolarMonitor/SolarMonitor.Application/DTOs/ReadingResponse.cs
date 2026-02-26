namespace SolarMonitor.Application.DTOs;

public class ReadingResponse
{
    public double Watts { get; set; }
    public double Voltage { get; set; }
    public DateTime Timestamp { get; set; }
}