using Domain.Abstract;
using Domain.WorkLogs;

namespace Domain;

public class WorkType : BaseEntity
{
    public string Name { get; private set; } = default!;
    public ICollection<WorkLog>? Orders { get; private set; } = [];

    public WorkType()
    { }

    public WorkType(string name)
    {
        Name = name;
    }
}
