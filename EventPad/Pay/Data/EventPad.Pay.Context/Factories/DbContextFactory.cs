namespace EventPad.Pay.Context;

using Microsoft.EntityFrameworkCore;

public class DbContextFactory
{
    private readonly DbContextOptions<PayDbContext> options;

    public DbContextFactory(DbContextOptions<PayDbContext> options)
    {
        this.options = options;
    }

    public PayDbContext Create()
    {
        return new PayDbContext(options);
    }
}