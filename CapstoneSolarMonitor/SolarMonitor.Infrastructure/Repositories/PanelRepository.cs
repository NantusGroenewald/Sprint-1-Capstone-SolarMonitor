using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using SolarMonitor.Application.Repositories;
using SolarMonitor.Domain.Entities;
using SolarMonitor.Domain.Enums;
using SolarMonitor.Infrastructure.Data;

namespace SolarMonitor.Infrastructure.Repositories
{
    public class PanelRepository : IPanelRepository
    {
        private readonly ApplicationDbContext _context;

        public PanelRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Panel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Panels
                .Include(p => p.Readings)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Panel>> GetPanelsAsync(CancellationToken cancellationToken)
        {
            return await _context.Panels
                .Include(p => p.Readings)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Panel panel, CancellationToken cancellationToken)
        {
            await _context.Panels
               .AddAsync(panel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Panel panel, CancellationToken cancellationToken)
        {
            var readingEntries = _context.ChangeTracker.Entries<Reading>().ToList();
            foreach (var entry in readingEntries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.State = EntityState.Added;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
