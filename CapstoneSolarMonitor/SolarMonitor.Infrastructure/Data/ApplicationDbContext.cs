using Microsoft.EntityFrameworkCore;
using SolarMonitor.Domain.Entities;
using SolarMonitor.Domain.Enums;

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

            entity.HasData(
                new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Brand = "SunPower", Model = "Maxeon 3", Type = PanelType.Monocrystalline, InstallationDate = new DateTime(2025, 1, 15) },
                new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Brand = "LG", Model = "NeON 2", Type = PanelType.Monocrystalline, InstallationDate = new DateTime(2025, 2, 20) },
                new { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Brand = "Canadian Solar", Model = "HiKu", Type = PanelType.Polycrystalline, InstallationDate = new DateTime(2025, 3, 10) }
            );
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.PanelId).IsRequired();
            entity.Property(r => r.Watts).IsRequired();
            entity.Property(r => r.Voltage).IsRequired();
            entity.Property(r => r.Timestamp).IsRequired();

            entity.HasData(
                new { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), PanelId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Watts = 350.5, Voltage = 48.2, Timestamp = new DateTime(2026, 3, 13, 9, 0, 0) },
                new { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), PanelId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Watts = 375.0, Voltage = 48.5, Timestamp = new DateTime(2026, 3, 13, 10, 0, 0) },
                new { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), PanelId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Watts = 320.0, Voltage = 47.8, Timestamp = new DateTime(2026, 3, 13, 9, 0, 0) }
            );
        });
    }
}