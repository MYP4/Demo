using AutoMapper;
using EventPad.Api.Service.Users;
using EventPad.Common.Files;
using EventPad.Common;

namespace EventPad.Api.Controllers.Users;

public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public FilePayload Image { get; set; }
}


public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserModel>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ToFileData()));
    }
}