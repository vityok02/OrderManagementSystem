using OrderManagementSystem.Models;

namespace Domain;

public class Customer : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? PhoneNumber { get; set; } = null!;
}
