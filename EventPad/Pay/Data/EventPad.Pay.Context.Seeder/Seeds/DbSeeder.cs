namespace EventPad.Pay.Context.Seeder;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static PayDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<PayDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
        {
            await AddDemoData(serviceProvider);
            //await AddAdministrator(serviceProvider);
        })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        if (await context.UserAccounts.AnyAsync())
            return;

        await context.UserAccounts.AddRangeAsync(new DemoHelper().GetUserAccounts);

        await context.SaveChangesAsync();
    }

}
