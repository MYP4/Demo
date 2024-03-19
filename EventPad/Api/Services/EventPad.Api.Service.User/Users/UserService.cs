
using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegiserUserModel> registerUserModelValidator;

    public UserService(
        IMapper mapper,
        UserManager<User> userManager,
        IModelValidator<RegiserUserModel> registerUserModelValidator
    )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserModelValidator = registerUserModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers(int page = 1, int pageSize = 10, UserModelFilter filter = null)
    {
        var users = await userManager.Users
            .ToListAsync();

        return mapper.Map<IEnumerable<UserModel>>(users);
    }

    public async Task<UserModel> GetById(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new ProcessException($"User {id} not found");

        return mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> Create(RegiserUserModel model)
    {
        registerUserModelValidator.Check(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        user = new User()
        {
            FirstName = model.FirstName,
            SecondName = model.SecondName,
            Role = model.Role,
            Rating = 0,

            UserName = model.Email,  
            Email = model.Email,
            EmailConfirmed = true, 
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> Update(Guid id, UpdateUserModel model)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new ProcessException($"User {id} not found");

        user = mapper.Map(model, user);

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new ProcessException($"Updating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<UserModel>(user);
    }

    public async Task Delete(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new ProcessException($"User {id} not found");

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new ProcessException($"Deleting user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
    }
}
