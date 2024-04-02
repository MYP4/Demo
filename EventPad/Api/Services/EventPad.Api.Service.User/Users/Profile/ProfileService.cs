using AutoMapper;
using EventPad.Actions;
using EventPad.Actions.Actions.Models;
using EventPad.Api.Context.Entities;
using EventPad.Redis;
using EventPad.Services.Actions;
using Microsoft.AspNetCore.Identity;

namespace EventPad.Api.Service.Users;
public class ProfileService : IProfileService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IAction action;
    private readonly IRedisService redisService;

    public ProfileService(
        IMapper mapper,
        UserManager<User> userManager,
        IAction action,
        IRedisService redisService)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.action = action;
        this.redisService = redisService;
    }


    public async Task<UserProfileModel> GetProfile(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        var requestId = redisService.KeyGenerate();

        await action.GetUserAccount(new GetUserAccountModel()
        {
            RequestId = requestId,
            UserId = user.Id
        });

        AccountModel result = new AccountModel();
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(500);
            try
            {
                result = await redisService.Get<AccountModel>(requestId);

                break;
            }
            catch (Exception ex)
            {
                
            }
        }

        var model = mapper.Map<UserProfileModel>(user);

        model.Balance = result.Balance;
        model.AccountNumber = result.AccountNumber;

        return model;
    }
}
