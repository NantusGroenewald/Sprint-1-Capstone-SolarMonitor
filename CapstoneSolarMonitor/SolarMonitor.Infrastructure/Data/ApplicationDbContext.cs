using Microsoft.EntityFrameworkCore;
using SolarMonitor.Domain.Entities;

namespace SolarMonitor.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Panel> Panels { get; set; }
    public DbSet<Reading> Readings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Panel>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Brand).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Model).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Type).HasConversion<string>();

            entity.Navigation(p => p.Readings)
                  .HasField("_readings")
                  .UsePropertyAccessMode(PropertyAccessMode.Field);

            entity.HasMany(p => p.Readings)
                  .WithOne()
                  .HasForeignKey(r => r.PanelId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.PanelId).IsRequired();
            entity.Property(r => r.Watts).IsRequired();
            entity.Property(r => r.Voltage).IsRequired();
            entity.Property(r => r.Timestamp).IsRequired();
        });
    }
}