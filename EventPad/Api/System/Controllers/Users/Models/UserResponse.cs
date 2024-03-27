using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Users;

namespace EventPad.Api.Controllers.Users;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
}


public class UserResponceProfile : Profile
{
    public UserResponceProfile()
    {
        CreateMap<UserModel, UserResponse>();
    }
}