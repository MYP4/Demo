using AutoMapper;
using EventPad.Api.Context.Entities;

namespace EventPad.Api.Services.Users;

public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
}


public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.SecondName, o => o.MapFrom(s => s.SecondName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
}

public class SpecificModelActions : IMappingAction<User, UserModel>
{

    public void Process(User source, UserModel dest, ResolutionContext context)
    {

    }
}