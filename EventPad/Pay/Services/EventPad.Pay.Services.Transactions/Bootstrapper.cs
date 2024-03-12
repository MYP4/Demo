namespace EventPad.Pay.Services.Transactions;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddEventAccountService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITransactionService, TransactionService>();
    }
}
