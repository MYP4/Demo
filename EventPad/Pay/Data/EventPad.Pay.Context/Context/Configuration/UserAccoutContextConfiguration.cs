using Microsoft.EntityFrameworkCore;
using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Context.Context;

public static class UserAccoutContextConfiguration
{
    public static void ConfigureUserAccounts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>().ToTable("user_accounts", PayDbContext.Schema);

        modelBuilder.Entity<UserAccount>().Property(x => x.AccountNumber).IsRequired();
        modelBuilder.Entity<UserAccount>().Property(x => x.AccountNumber).HasMaxLength(16);
        modelBuilder.Entity<UserAccount>().HasIndex(x => x.AccountNumber).IsUnique();
        modelBuilder.Entity<UserAccount>().Property(x => x.Balance).IsRequired();
    }
}
