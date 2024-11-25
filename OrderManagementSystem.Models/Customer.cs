using Domain.Abstract;
using Domain.WorkLogs;
using System.Text.RegularExpressions;

namespace Domain;

public class Customer : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? PhoneNumber { get; private set; }
    public ICollection<WorkLog> Orders { get; private set; } = new List<WorkLog>();

    public Customer(string name, string? phoneNumber = null!)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}
