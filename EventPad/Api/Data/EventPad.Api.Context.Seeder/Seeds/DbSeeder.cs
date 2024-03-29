﻿namespace EventPad.Api.Context.Seeder;

using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
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

    private static ApiDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<ApiDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
        {
            await AddAdministrator(serviceProvider);
            await AddDemoData(serviceProvider);
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

        if (await context.Events.AnyAsync())
            return;

        //await context.Events.AddRangeAsync(new DemoHelper().GetEvents);

        await context.SaveChangesAsync();
    }

    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        //var settings = scope.ServiceProvider.GetService<DbSettings>();
        //if (!(settings.Init?.AddAdministrator ?? false))
        //    return;

        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

        if ((await userManager.Users.AnyAsync()))
            return;

        var adminId = Guid.Parse("5152F065-E458-4869-B4D0-4C11F72DEECA");

        var admin = new User()
        {
            Id = adminId,

            FirstName = "Admin",
            SecondName = "Admin",
            Role = UserRole.Administrator,
            Rating = 0,
            Account = adminId,

            UserName = "Admin@adm.com",  
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        await userManager.CreateAsync(admin);
        await userManager.AddPasswordAsync(admin, "123456");


    }
}
