namespace EventPad.Api.Services.Tickets;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddTicketService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITicketService, TicketService>();
    }
}
