using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Domain.Abstract;
using Domain.WorkLogs;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("docker")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IWorkLogRepository, WorkLogRepository>();

        return services;
    }
}
