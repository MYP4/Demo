namespace EventPad.Api.Services.Specific;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddSpecificEventService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ISpecificEventService, SpecificEventService>();
    }
}
