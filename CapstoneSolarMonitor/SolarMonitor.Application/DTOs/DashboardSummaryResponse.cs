namespace SolarMonitor.Application.DTOs
{
    public class DashboardSummaryResponse
    {
        public double TotalCurrentWatts { get; set; }
        public int ActivePanels { get; set; }
        public int TotalPanels { get; set; }
        public DateTime LastUpdated { get; set; }
        public string SystemStatus { get; set; } = string.Empty;
    }
}
