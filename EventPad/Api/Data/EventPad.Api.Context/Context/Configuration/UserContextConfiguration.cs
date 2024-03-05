namespace EventPad.Api.Context;

using EventPad.Api.Context.Entities;
using Microsoft.EntityFrameworkCore;


public static class UserContextConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
    }
}
