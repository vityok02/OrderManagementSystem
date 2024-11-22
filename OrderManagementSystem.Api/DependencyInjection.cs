using System.Reflection;
using System.Reflection.Metadata;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        return services;
    }
}
