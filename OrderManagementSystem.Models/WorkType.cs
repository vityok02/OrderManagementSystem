namespace OrderManagementSystem.Models;

public class WorkType : BaseEntity
{
    public string? Name { get; set; }
    public ICollection<Order>? Orders { get; set; } = new HashSet<Order>();
}
