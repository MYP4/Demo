namespace EventPad.Pay.Services.Transactions;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddTransactionService(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITransactionService, TransactionService>();
    }
}
