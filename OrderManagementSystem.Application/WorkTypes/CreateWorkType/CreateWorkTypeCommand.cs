using Application.Abstract.Commands;
using Domain;

namespace Application.WorkTypes.CreateWorkType;

public record CreateWorkTypeCommand(WorkTypeDto WorkTypeDto) : ICommand<WorkTypeDto>;
