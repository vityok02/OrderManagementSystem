using Application.WorkTypes.CreateWorkType;

namespace Application.WorkTypes.GetWorkTypes;

public record WorkTypesResponse(IEnumerable<WorkTypeDto> WorkTypes);
