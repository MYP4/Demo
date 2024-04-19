using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Settings;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Service.Users;
public class UserProfileModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
    public string AccountNumber { get; set; }
    public float Balance { get; set; }

    public string Image { get; set; }
}


public class UserProfileModelProfile : Profile
{
    public UserProfileModelProfile()
    {
        CreateMap<User, UserProfileModel>()
            .AfterMap<UserProfileModelActions>();
    }
}


public class UserProfileModelActions : IMappingAction<User, UserProfileModel>
{

    public readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly MainSettings mainSettings;

    public UserProfileModelActions(IDbContextFactory<ApiDbContext> dbContextFactory, MainSettings mainSettings)
    {
        this.dbContextFactory = dbContextFactory;
        this.mainSettings = mainSettings;
    }

    public void Process(User source, UserProfileModel dest, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.Image))
            dest.Image = Path.Combine(mainSettings.FileDir, source.Image);
    }
}
