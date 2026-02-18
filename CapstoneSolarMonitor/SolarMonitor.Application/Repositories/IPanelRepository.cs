using SolarMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarMonitor.Application.Repositories
{
    public interface IPanelRepository
    {
        Task<Panel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task SaveAsync(Panel panel, CancellationToken cancellationToken); 
    }
}
