using Application;
using Application.Orders.Queries;
using Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/orders", async (ISender sender) =>
{
    var orders = await sender.Send(new GetOrdersQuery());

    return Results.Ok(orders.Value);
});

app.MapPost("/orders", async (ISender sender) =>
{
    var order = await sender.Send(new CreateOrderCommand());

    return Results.Created(order.Value);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
