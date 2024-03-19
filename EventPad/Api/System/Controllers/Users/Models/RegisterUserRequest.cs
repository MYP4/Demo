using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Users;

namespace EventPad.Api.Controllers.Users;

public class RegisterUserRequest
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}


public class RegisterUserProfile : Profile
{
    public RegisterUserProfile()
    {
        CreateMap<RegisterUserRequest, RegiserUserModel>();
    }
}