using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Common.Files;
using EventPad.Settings;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Service.Users;

public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }

    public string Image { get; set; }
}


public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<User, UserModel>()
            .BeforeMap<UserModelActions>();
    }
}


public class UserModelActions : IMappingAction<User, UserModel>
{

    public readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly MainSettings mainSettings;

    public UserModelActions(IDbContextFactory<ApiDbContext> dbContextFactory, MainSettings mainSettings)
    {
        this.dbContextFactory = dbContextFactory;
        this.mainSettings = mainSettings;
    }

    public void Process(User source, UserModel dest, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.Image))
            dest.Image = Path.Combine(mainSettings.FileDir, source.Image);
    }
}