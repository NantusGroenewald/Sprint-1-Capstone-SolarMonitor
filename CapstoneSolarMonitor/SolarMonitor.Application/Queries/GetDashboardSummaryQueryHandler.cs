using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SolarMonitor.Application.DTOs;
using SolarMonitor.Application.Repositories;
using System.Text.Json;

namespace SolarMonitor.Application.Queries
{
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryResponse>
    {
        private readonly IPanelRepository _repository;
        private readonly IDistributedCache _cache;

        public GetDashboardSummaryQueryHandler(IPanelRepository repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<DashboardSummaryResponse> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "SystemDashboardSummary";
            
            var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                return JsonSerializer.Deserialize<DashboardSummaryResponse>(cachedResponse);
            }

            var panels = await _repository.GetPanelsAsync(cancellationToken);

            double totalWatts = 0;
            int activePanelCount = 0;
            var activeThreshold = DateTime.UtcNow.AddMinutes(-15);

            foreach (var panel in panels)
            {
                var latestReading = panel.Readings.OrderByDescending(r => r.Timestamp).FirstOrDefault(); 

                if(latestReading != null && latestReading.Timestamp >= activeThreshold)
                {
                    totalWatts += latestReading.Watts;
                    activePanelCount++;
                }
            }

            var summary = new DashboardSummaryResponse
            {
                TotalCurrentWatts = totalWatts,
                ActivePanels = activePanelCount,
                TotalPanels = panels.Count(),
                LastUpdated = DateTime.UtcNow,
                SystemStatus = activePanelCount == panels.Count() ? "All Systems Operational" : $"{activePanelCount} of {panels.Count()} Panels Active"
            };

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(summary), cacheOptions, cancellationToken);

            return summary; 
        }

    }
}
