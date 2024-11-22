using Domain.Abstract;
using Domain.WorkLogs;

namespace OrderManagementSystem.Models;

public class WorkType : BaseEntity
{
    public string? Name { get; set; }
    public ICollection<WorkLog>? Orders { get; set; } = new HashSet<WorkLog>();
}
