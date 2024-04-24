using AutoMapper;
using EventPad.Api.Service.Users;
using EventPad.Common.Files;

namespace EventPad.Api.Controllers.Users;

public class RegisterUserRequest
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}


public class RegisterUserProfile : Profile
{
    public RegisterUserProfile()
    {
        CreateMap<RegisterUserRequest, RegiserUserModel>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
    }
}