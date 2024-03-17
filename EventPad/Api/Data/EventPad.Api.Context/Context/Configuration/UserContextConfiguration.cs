namespace EventPad.Api.Context;

using EventPad.Api.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


public static class UserContextConfiguration
{
    public static void ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users", ApiDbContext.Schema);

        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles", ApiDbContext.Schema);
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens", ApiDbContext.Schema);
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners", ApiDbContext.Schema);
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims", ApiDbContext.Schema);
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins", ApiDbContext.Schema);
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims", ApiDbContext.Schema);
    }
}
