namespace EventPad.Api.Context;

using EventPad.Api.Context.Entities;
using Microsoft.EntityFrameworkCore;


public static class SpecificEventContextConfiguration
{
    public static void ConfigureSpecificEvents(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SpecificEvent>().ToTable("specific_events");

        modelBuilder.Entity<SpecificEvent>().Property(x => x.Time).IsRequired();
        modelBuilder.Entity<SpecificEvent>().Property(x => x.Code).IsRequired();
        modelBuilder.Entity<SpecificEvent>().Property(x => x.Code).HasMaxLength(50);
        modelBuilder.Entity<SpecificEvent>().Property(x => x.Rating).IsRequired();

        modelBuilder.Entity<SpecificEvent>().HasOne(x => x.Event).WithMany(x => x.SpecificEvents).HasForeignKey(x => x.EventId);
    }
}
