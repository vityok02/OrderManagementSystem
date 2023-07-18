using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data;

public class Repository : IRepository
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext context) 
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Update(order);
        await SaveChangesAsync();
    }

    public async Task<Order> GetAsync(int id)
    {
        var orders = await _context.Orders.FindAsync(id);

        return orders!;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = await _context.Orders.ToArrayAsync();

        return orders;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
