namespace EventPad.Api.Context;

using Microsoft.EntityFrameworkCore;
using EventPad.Api.Context.Entities;

public static class EventContextConfiguration
{
    public static void ConfigureEvents(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().ToTable("events", ApiDbContext.Schema);

        modelBuilder.Entity<Event>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Event>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<Event>().Property(x => x.Price).IsRequired();
        modelBuilder.Entity<Event>().Property(x => x.Address).IsRequired();
        modelBuilder.Entity<Event>().Property(x => x.Address).HasMaxLength(100);
        modelBuilder.Entity<Event>().Property(x => x.Type).IsRequired();
        modelBuilder.Entity<Event>().Property(x => x.Rating).IsRequired();

        modelBuilder.Entity<Event>().HasOne(x => x.Admin).WithMany(x => x.Events).HasForeignKey(x => x.AdminId);
    }
}
