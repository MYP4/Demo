namespace EventPad.Api.Context;

using Microsoft.EntityFrameworkCore;


public static class DbContextOptionsFactory
{
    private const string migrationProjectPrefix = "EventPad.Api.Context.Migrations.";

    public static DbContextOptions<ApiDbContext> Create(string connStr, DbType dbType, bool detailedLogging = false)
    {
        var builder = new DbContextOptionsBuilder<ApiDbContext>();

        Configure(connStr, dbType, detailedLogging).Invoke(builder);

        return builder.Options;
    }
    
    public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType dbType, bool detailedLogging = false)
    {
        return (builder) =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    builder.UseSqlServer(connStr,
                        options => options
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{migrationProjectPrefix}{DbType.MSSQL}")
                    );
                    break;

                case DbType.PgSql:
                    builder.UseNpgsql(connStr,
                        options => options
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{migrationProjectPrefix}{DbType.PgSql}")
                    );
                    break;             
            }

            if (detailedLogging)
            {
                builder.EnableSensitiveDataLogging();
            }

            // Attention!
            // It possible to use or LazyLoading or NoTracking at one time
            // Together this features don't work

            builder.UseLazyLoadingProxies(true);
            //builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        };
    }
}
