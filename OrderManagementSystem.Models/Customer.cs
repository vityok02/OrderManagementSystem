using Domain.Abstract;
using Domain.WorkLogs;

namespace Domain;

public class Customer : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? PhoneNumber { get; private set; } = null!;
    public ICollection<WorkLog> Orders { get; private set; } = new List<WorkLog>();

    public Customer(string name, string? phoneNumber = null!)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}
