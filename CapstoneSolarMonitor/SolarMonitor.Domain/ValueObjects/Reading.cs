namespace SolarMonitor.Domain.ValueObjects
{
    public record Reading(double Watts, double Voltage, DateTime TimeStamp);
}
