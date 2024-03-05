namespace EventPad.Api.Context;

using Microsoft.EntityFrameworkCore;

public class DbContextFactory
{
    private readonly DbContextOptions<ApiDbContext> options;

    public DbContextFactory(DbContextOptions<ApiDbContext> options)
    {
        this.options = options;
    }

    public ApiDbContext Create()
    {
        return new ApiDbContext(options);
    }
}