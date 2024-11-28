using Domain;
using Domain.WorkLogs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<WorkLog> WorkLogs { get; set; } = null!;
    public DbSet<WorkType> WorkTypes { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkLog>().HasOne(o => o.WorkType)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.TypeId);
    }
}
