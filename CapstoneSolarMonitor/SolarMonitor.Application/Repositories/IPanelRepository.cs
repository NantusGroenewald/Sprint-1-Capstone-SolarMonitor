using SolarMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarMonitor.Application.Repositories
{
    public interface IPanelRepository
    {
        Task<Panel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Panel>> GetPanelsAsync(CancellationToken cancellationToken);
        Task AddAsync (Panel panel, CancellationToken cancellationToken);
        Task UpdateAsync (Panel panel, CancellationToken cancellationToken);
    }
}
