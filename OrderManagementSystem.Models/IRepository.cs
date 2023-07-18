namespace OrderManagementSystem.Models;

public interface IRepository
{
    Task<Order> GetAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task SaveChangesAsync();
}
