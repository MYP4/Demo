namespace EventPad.Pay.Context;

using EventPad.Pay.Context.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class PayDbContext : DbContext
{
    public static readonly string Schema = "pay";

    public DbSet<EventAccount> EventAccounts { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public PayDbContext(DbContextOptions<PayDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUserAccounts();
        modelBuilder.ConfigureEventAccounts();
        modelBuilder.ConfigureTransactions();
    }
}
