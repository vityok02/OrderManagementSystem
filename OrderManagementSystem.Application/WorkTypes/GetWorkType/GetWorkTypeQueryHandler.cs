using Application.Abstract.Queries;
using Application.WorkTypes.CreateWorkType;
using Domain;
using Domain.Abstract;

namespace Application.WorkTypes.GetWorkType;

internal class GetWorkTypeQueryHandler : IQueryHandler<GetWorkTypeQuery, WorkTypeDto>
{
    private readonly IRepository<WorkType> _workTypeRepository;

    public GetWorkTypeQueryHandler(IRepository<WorkType> workTypeRepository)
    {
        _workTypeRepository = workTypeRepository;
    }

    public async Task<Result<WorkTypeDto>> Handle(GetWorkTypeQuery query, CancellationToken cancellationToken)
    {
        var workType = await _workTypeRepository.GetAsync(query.WorkTypeId, cancellationToken);

        if (workType is null)
        {
            return Result<WorkTypeDto>.Failure(new Error("WorkType not found"));
        }

        return Result<WorkTypeDto>.Success(workType.ToDto());
    }
}