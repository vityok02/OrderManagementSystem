using Application.WorkTypes;
using Application.WorkTypes.CreateWorkType;
using Application.WorkTypes.GetWorkType;
using Application.WorkTypes.GetWorkTypes;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class WorkTypesEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/work-types")
            .WithDisplayName("WorkTypes Group")
            .WithOpenApi();

        group.MapGet("", GetWorkTypes)
            .WithName("GetWorkTypes");

        group.MapGet("{id:int}", GetWorkType)
            .WithName("GetWorkType");

        group.MapPost("", CreateWorkType)
            .WithName("CreateWorkType");
    }

    private static async Task<IResult> GetWorkTypes(
        ISender sender)
    {
        var result = await sender.Send(new GetWorkTypesQuery());
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound();
    }

    private static async Task<IResult> GetWorkType(
        ISender sender,
        int id)
    {
        var workType = await sender.Send(new GetWorkTypeQuery(id));
        return workType.IsSuccess
            ? Results.Ok(workType.Value)
            : Results.NotFound();
    }

    private static async Task<IResult> CreateWorkType(
        HttpContext context,
        LinkGenerator linkGenerator,
        ISender sender,
        [FromBody] CreateWorkTypeDto workTypeDto)
    {
        var workType = await sender.Send(new CreateWorkTypeCommand(workTypeDto));
        return workType.IsSuccess
            ? Results.Created(linkGenerator.GetPathByName("GetWorkType", new { id = workType.Value.Id }), workType.Value)
            : Results.BadRequest(workType.Error);
    }
}
