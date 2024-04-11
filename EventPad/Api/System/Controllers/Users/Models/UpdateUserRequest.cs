using AutoMapper;
using EventPad.Api.Service.Users;
using EventPad.Common.Files;

namespace EventPad.Api.Controllers.Users;

public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public float? Rating { get; set; }
    public string? Email { get; set; }
    public FilePayload Image { get; set; }
}


public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserModel>();
    }
}