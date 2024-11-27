using Application.Abstract.Queries;
using Domain;

namespace Application.WorkTypes.GetWorkType;

public record GetWorkTypeQuery(int WorkTypeId) : IQuery<WorkTypeDto>;
