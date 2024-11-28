using Application.WorkLogs.CreateWorkLog;
using Application.WorkLogs.GetWorkLog;
using Application.WorkLogs.GetWorkLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class WorkLogsEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/worklogs")
            .WithTags("WorkLogs Group")
            .WithOpenApi();

        group.MapGet("", GetWorkLogs)
            .WithName("GetWorkLogs");

        group.MapGet("{id:int}", GetWorkLog)
            .WithName("GetWorkLog");

        group.MapPost("", CreateWorkLog)
            .WithName("CreateWorkLog")
            .AddEndpointFilter<EnsureWorkTypesExistFilter>();
    }

    private static async Task<IResult> GetWorkLogs(
        ISender sender)
    {
        var result = await sender.Send(new GetOrdersQuery());
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound();
    }

    private static async Task<IResult> GetWorkLog(
        ISender sender,
        int id)
    {
        var order = await sender.Send(new GetWorkLogQuery(id));

        return order.IsSuccess
            ? Results.Ok(order.Value)
            : Results.NotFound();
    }

    private static async Task<IResult> CreateWorkLog(
        HttpContext context,
        LinkGenerator linkGenerator,
        ISender sender,
        [FromBody] CreateWorkLogDto dto)
    {
        var workLog = await sender
            .Send(new CreateWorkLogCommand(dto));

        if (workLog.IsFailure)
        {
            return Results.BadRequest(workLog.Error);
        }

        var link = linkGenerator
            .GetPathByName(context, "GetWorkLog", new { id = workLog.Value.Id });

        return Results
            .Created(link, workLog);
    }
}
