using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Service.Users;

namespace EventPad.Api.Controllers.Users;

public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public UserRole? Role { get; set; }
    public float? Rating { get; set; }
    public string? Email { get; set; }
}


public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserModel>();
    }
}