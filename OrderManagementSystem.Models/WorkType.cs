using Domain.Abstract;
using Domain.WorkLogs;

namespace Domain;

public class WorkType : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<WorkLog>? Orders { get; set; } = new HashSet<WorkLog>();
}
