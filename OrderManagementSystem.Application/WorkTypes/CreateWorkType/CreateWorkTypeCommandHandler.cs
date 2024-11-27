using Application.Abstract.Commands;
using Domain;
using Domain.Abstract;
using OrderManagementSystem.Models;

namespace Application.WorkTypes.CreateWorkType;

internal class CreateWorkTypeCommandHandler
    : ICommandHandler<CreateWorkTypeCommand, WorkType>
{
    private readonly IRepository<WorkType> _workTypeRepository;

    public CreateWorkTypeCommandHandler(IRepository<WorkType> workTypeRepository)
    {
        _workTypeRepository = workTypeRepository;
    }

    public async Task<Result<WorkType>> Handle(CreateWorkTypeCommand request, CancellationToken cancellationToken)
    {
        var workType = new WorkType(request.WorkTypeDto.Name);

        await _workTypeRepository.CreateAsync(workType, cancellationToken);
        await _workTypeRepository.SaveChangesAsync(cancellationToken);

        return Result<WorkType>.Success(workType);
    }
}
