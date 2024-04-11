using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Common.Files;

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
        CreateMap<User, UserModel>();
    }
}
