using Application.Abstract.Commands;
using Domain;
using Domain.Abstract;

namespace Application.WorkTypes.CreateWorkType;

internal class CreateWorkTypeCommandHandler
    : ICommandHandler<CreateWorkTypeCommand, WorkTypeDto>
{
    private readonly IRepository<WorkType> _workTypeRepository;

    public CreateWorkTypeCommandHandler(IRepository<WorkType> workTypeRepository)
    {
        _workTypeRepository = workTypeRepository;
    }

    public async Task<Result<WorkTypeDto>> Handle(
        CreateWorkTypeCommand request,
        CancellationToken cancellationToken)
    {
        var workType = new WorkType(request.WorkTypeDto.Name);

        await _workTypeRepository.CreateAsync(workType, cancellationToken);
        await _workTypeRepository.SaveChangesAsync(cancellationToken);

        return Result<WorkTypeDto>.Success(workType.ToDto());
    }
}
