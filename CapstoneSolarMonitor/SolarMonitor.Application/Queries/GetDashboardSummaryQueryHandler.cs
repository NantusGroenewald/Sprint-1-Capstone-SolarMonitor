using MediatR;
using SolarMonitor.Application.DTOs;
using SolarMonitor.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarMonitor.Application.Queries
{
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryResponse>
    {
        private readonly IPanelRepository _repository;

        public GetDashboardSummaryQueryHandler(IPanelRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardSummaryResponse> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
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

            return new DashboardSummaryResponse
            {
                TotalCurrentWatts = totalWatts,
                ActivePanels = activePanelCount,
                TotalPanels = panels.Count(),
                LastUpdated = DateTime.UtcNow,
                SystemStatus = activePanelCount == panels.Count() ? "All Systems Operational" : $"{activePanelCount} of {panels.Count()} Panels Active"
            };
        }

    }
}
