using Domain;

namespace Application.WorkTypes.CreateWorkType;

public static class WorkTypeExtensions
{
    public static WorkTypeDto ToDto(this WorkType workType)
    {
        return new WorkTypeDto(workType.Id, workType.Name);
    }
}