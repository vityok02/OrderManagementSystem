using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public string ConnectionString { get; set; } = @"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;"; // @"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;"

    public AppDbContext()
    { }

    //public AppDbContext(string connectionString)
    //{
    //    ConnectionString = connectionString;
    //    Database.EnsureCreated();
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}
