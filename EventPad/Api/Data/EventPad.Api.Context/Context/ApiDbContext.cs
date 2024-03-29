﻿namespace EventPad.Api.Context;

using EventPad.Api.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public static readonly string Schema = "main";

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventPhoto> EventPhotos { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<SpecificEvent> SpecificEvents { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUsers();
        modelBuilder.ConfigureEvents();
        modelBuilder.ConfigureEventPhotos();
        modelBuilder.ConfigureSpecificEvents();
        modelBuilder.ConfigureTickets();
    }
}
