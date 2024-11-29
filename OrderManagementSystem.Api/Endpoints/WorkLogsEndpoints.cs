using Api.Endpoints.Filters;
using Application.WorkLogs;
using Application.WorkLogs.CreateWorkLog;
using Application.WorkLogs.GetWorkLog;
using Application.WorkLogs.GetWorkLogs;
using Application.WorkLogs.UpdateStatus;
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
            .WithName("GetWorkLog")
            .AddEndpointFilter<EnsureWorkLogExistFilter>()
            .Produces<WorkLogDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", CreateWorkLog)
            .WithName("CreateWorkLog")
            .AddEndpointFilter<EnsureWorkTypesExistFilter>();

        group.MapPut("{id:int}/status", UpdateStatus)
            .WithName("UpdateStatus")
            .AddEndpointFilter<EnsureWorkLogExistFilter>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    // TOTO: WARNING: Use FromBody instead of FromQuery
    private static async Task<IResult> UpdateStatus(
        ISender sender,
        [FromRoute] int id,
        [FromQuery] int status)
    {
        var result = await sender
            .Send(new UpdateStatusCommand(id, status));

        return result.IsSuccess
            ? Results.Ok()
            : Results.BadRequest();
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
        [FromRoute] int id)
    {
        var result = await sender.Send(new GetWorkLogQuery(id));

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound();
    }

    private static async Task<IResult> CreateWorkLog(
        HttpContext context,
        LinkGenerator linkGenerator,
        ISender sender,
        [FromBody] CreateWorkLogDto dto)
    {
        var result = await sender
            .Send(new CreateWorkLogCommand(dto));

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        var link = linkGenerator
            .GetPathByName(context, "GetWorkLog", new { id = result.Value.Id });

        return Results
            .Created(link, result.Value);
    }
}
