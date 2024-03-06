using Microsoft.EntityFrameworkCore;
using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Context.Context;

public static class EventAccoutContextConfiguration
{
    public static void ConfigureEventAccounts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventAccount>().ToTable("event_accounts", PayDbContext.Schema);

        modelBuilder.Entity<EventAccount>().Property(x => x.AccountNumber).IsRequired();
        modelBuilder.Entity<EventAccount>().Property(x => x.AccountNumber).HasMaxLength(16);
        modelBuilder.Entity<EventAccount>().HasIndex(x => x.AccountNumber).IsUnique();
        modelBuilder.Entity<EventAccount>().Property(x => x.Balance).IsRequired();
    }
}

