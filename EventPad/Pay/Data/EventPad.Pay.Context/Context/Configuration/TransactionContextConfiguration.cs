using Microsoft.EntityFrameworkCore;
using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Context.Context;

public static class TransactionContextConfiguration
{
    public static void ConfigureTransactions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().ToTable("transactions", PayDbContext.Schema);

        modelBuilder.Entity<Transaction>().Property(x => x.Type).IsRequired();
        modelBuilder.Entity<Transaction>().Property(x => x.Ticket).HasMaxLength(16);
        modelBuilder.Entity<Transaction>().Property(x => x.Date).IsRequired();
        modelBuilder.Entity<Transaction>().Property(x => x.Amount).IsRequired();


        modelBuilder.Entity<Transaction>().HasOne(x => x.EventAccount).WithMany(x => x.Transactions).HasForeignKey(x => x.EventAccountId);
        modelBuilder.Entity<Transaction>().HasOne(x => x.UserAccount).WithMany(x => x.Transactions).HasForeignKey(x => x.UserAccountId);
    }
}
