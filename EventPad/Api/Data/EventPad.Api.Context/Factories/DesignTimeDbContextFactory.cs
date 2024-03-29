﻿namespace EventPad.Api.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
{
    public ApiDbContext CreateDbContext(string[] args)
    {
        var provider = (args?[0] ?? $"{DbType.MSSQL}").ToLower();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.context.json"), false)
            .Build();

        var connStr = configuration.GetConnectionString(provider);

        DbType dbType;
        if (provider.Equals($"{DbType.MSSQL}".ToLower()))
            dbType = DbType.MSSQL;
        else if (provider.Equals($"{DbType.PgSql}".ToLower()))
            dbType = DbType.PgSql;
        else
            throw new Exception($"Unsupported provider: {provider}");

        var options = DbContextOptionsFactory.Create(connStr, dbType, true);
        var factory = new DbContextFactory(options);
        var context = factory.Create();

        return context;
    }
}
