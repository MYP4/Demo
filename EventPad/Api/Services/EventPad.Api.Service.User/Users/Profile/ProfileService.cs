using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Common;
using EventPad.Services.Actions;
using Microsoft.AspNetCore.Identity;

namespace EventPad.Api.Service.Users;
public class ProfileService : IProfileService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IAction action;

    public ProfileService(
        IMapper mapper,
        UserManager<User> userManager,
        IAction action)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.action = action;
    }

    public async Task<UserProfileModel> GetProfile(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());


        return mapper.Map<UserProfileModel>(user);
    }
}
