using Application.Abstract.Queries;
using Application.WorkTypes.CreateWorkType;
using Domain;

namespace Application.WorkTypes.GetWorkType;

public record GetWorkTypeQuery(int WorkTypeId) : IQuery<WorkTypeDto>;
