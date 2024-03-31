
using EventPad.Api.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventPad.Api.Service.Users;
public class RightsService : IRightsService
{
    private readonly UserManager<User> userManager;

    public RightsService(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<bool> IsAdmin(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user.Role == UserRole.Administrator || user.Role == UserRole.Moderator)
            return true;

        return false;
    }

    public async Task SetRights(Guid id, UserRole role)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        user.Role = role;

        await userManager.UpdateAsync(user);
    }
}
