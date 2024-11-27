using Application.Abstract.Queries;
using Domain;
using Domain.Abstract;
using OrderManagementSystem.Models;

namespace Application.WorkTypes.GetWorkTypes;

internal class GetWorkTypesQueryHandler : IQueryHandler<GetWorkTypesQuery, WorkTypesResponse>
{
    private readonly IRepository<WorkType> _workTypeRepository;

    public GetWorkTypesQueryHandler(IRepository<WorkType> workTypeRepository)
    {
        _workTypeRepository = workTypeRepository;
    }

    public async Task<Result<WorkTypesResponse>> Handle(GetWorkTypesQuery request, CancellationToken cancellationToken)
    {
        var workTypes = await _workTypeRepository.GetAllAsync(cancellationToken);

        return Result<WorkTypesResponse>.Success(new WorkTypesResponse(workTypes.Select(wt => wt.ToDto())));
    }
}
