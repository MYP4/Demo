using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Users;

namespace EventPad.Api.Controllers.Users;

public class UserFilterRequest
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public UserRole? Role { get; set; }
    public float? Rating { get; set; }
}

public class UserFilterProfile : Profile
{
    public UserFilterProfile()
    {
        CreateMap<UserFilterRequest, UserModelFilter>();
    }
}