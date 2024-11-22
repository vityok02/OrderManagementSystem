using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<WorkType> OrderTypes { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasOne(o => o.Type)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.TypeId);
    }
}
