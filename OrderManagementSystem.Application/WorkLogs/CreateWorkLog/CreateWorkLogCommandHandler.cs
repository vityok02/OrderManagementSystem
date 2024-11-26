﻿using Application.Abstract.Commands;
using Application.Orders.Commands;
using Domain;
using Domain.Abstract;
using Domain.WorkLogs;
using OrderManagementSystem.Models;

namespace Application.WorkLogs.CreateWorkLog;

internal class CreateWorkLogCommandHandler
    : ICommandHandler<CreateWorkLogCommand, WorkLog>
{
    private readonly IRepository<WorkLog> _workLogRepository;
    private readonly IRepository<WorkType> _workTypeRepository;

    public CreateWorkLogCommandHandler(
        IRepository<WorkLog> workLogRepository,
        IRepository<WorkType> workTypeRepository)
    {
        _workLogRepository = workLogRepository;
        _workTypeRepository = workTypeRepository;
    }

    public async Task<Result<WorkLog>> Handle(
        CreateWorkLogCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.WorkLogDto;

        var workType = await _workTypeRepository
            .GetAsync(dto.WorkTypeId, cancellationToken);

        if (workType is null)
        {
            return Result<WorkLog>
                .Failure(CreateWorkLogErrors.WorkTypeNotFound(dto.WorkTypeId));
        }

        var workLog = new WorkLog(
            new Customer(dto.CustomerName, dto.Contacts),
            workType,
            dto.Amount,
            dto.UnitPrice);

        await _workLogRepository.AddAsync(workLog, cancellationToken);

        return Result<WorkLog>.Success(workLog);
    }
}
