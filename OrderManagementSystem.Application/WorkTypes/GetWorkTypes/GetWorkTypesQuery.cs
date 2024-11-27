using Application.Abstract.Queries;

namespace Application.WorkTypes.GetWorkTypes;

public record GetWorkTypesQuery() : IQuery<WorkTypesResponse>;
