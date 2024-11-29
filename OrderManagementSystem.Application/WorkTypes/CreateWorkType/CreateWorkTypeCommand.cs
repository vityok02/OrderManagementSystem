using Application.Abstract.Commands;

namespace Application.WorkTypes.CreateWorkType;

public record CreateWorkTypeCommand(CreateWorkTypeDto WorkTypeDto) : ICommand<WorkTypeDto>;
