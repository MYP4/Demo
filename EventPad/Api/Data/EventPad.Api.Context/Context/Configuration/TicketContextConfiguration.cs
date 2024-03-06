namespace EventPad.Api.Context;

using Microsoft.EntityFrameworkCore;
using EventPad.Api.Context.Entities;

public static class TicketContextConfiguration
{
    public static void ConfigureTickets(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>().ToTable("event_tickets", ApiDbContext.Schema);

        modelBuilder.Entity<Ticket>().Property(x => x.Status).IsRequired();

        modelBuilder.Entity<Ticket>().HasOne(x => x.User).WithMany(x => x.Tickets).HasForeignKey(x => x.UserId);
        modelBuilder.Entity<Ticket>().HasOne(x => x.SpecificEvent).WithMany(x => x.Tickets).HasForeignKey(x => x.SpecificEventId);
    }
}
