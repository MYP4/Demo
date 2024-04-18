namespace EventPad.Pay.Context.Seeder;

using EventPad.Pay.Context.Entities;
using Microsoft.AspNetCore.Identity;
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
            await AddAdministrator(serviceProvider);
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
        await context.EventAccounts.AddRangeAsync(new DemoHelper().GetEventAccounts);

        await context.SaveChangesAsync();
    }

    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddAdmin ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        var adminId = Guid.Parse("5152f065-e458-4869-b4d0-4c11f72deeca");

        if (await context.UserAccounts.AnyAsync(x => x.Uid == adminId))
            return;

        var adminAccount = new UserAccount()
        {
            Id = 1,
            Uid = adminId,
            AccountNumber = "1234569871012146",
            Balance = 1000
        };

        await context.UserAccounts.AddAsync(adminAccount);

        await context.SaveChangesAsync();
    }

}
