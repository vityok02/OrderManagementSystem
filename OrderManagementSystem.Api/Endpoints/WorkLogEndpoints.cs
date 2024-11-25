using Application.Orders.Commands;
using Application.WorkLogs.CreateWorkLog;
using Application.WorkLogs.GetWorkLog;
using Application.WorkLogs.GetWorkLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class WorkLogEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/worklogs");

        group.MapGet("/worklogs", async (ISender sender) =>
        {
            var orders = await sender.Send(new GetOrdersQuery());

            return Results.Ok(orders.Value);
        });

        group.MapGet("/worklogs/{id:int}", async (
            ISender sender,
            int id) =>
        {
            var order = await sender.Send(new GetWorkLogQuery(id));

            return order.IsSuccess
                ? Results.Ok(order.Value)
                : Results.NotFound();
        })
            .WithName("GetWorkLog");

        group.MapPost("/worklogs", async (
            HttpContext context,
            LinkGenerator linkGenerator,
            ISender sender,
            ILogger<Program> logger,
            [FromBody] CreateWorkLogDto dto) =>
        {
            var workLog = await sender
                .Send(new CreateWorkLogCommand(dto));

            if (workLog.IsFailure)
            {
                logger
                    .LogError("Failed to create work log: {Error}", workLog.Error);
            }

            var link = linkGenerator
                .GetPathByName(context, "GetWorkLog", new { id = workLog.Value.Id });

            return Results
                .Created(link, workLog);
        })
            .WithName("CreateWorkLog");
    }
}
