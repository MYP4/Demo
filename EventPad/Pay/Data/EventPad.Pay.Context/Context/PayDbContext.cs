namespace EventPad.Pay.Context;

using EventPad.Pay.Context.Context;
using Microsoft.EntityFrameworkCore;

public class PayDbContext : DbContext
{
    public static readonly string Schema = "pay";

    public PayDbContext(DbContextOptions<PayDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUserAccounts();
        modelBuilder.ConfigureEventAccounts();
        modelBuilder.ConfigureTransactions();
    }
}
