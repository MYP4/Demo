using AutoMapper;
using EventPad.Api.Service.Users;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EventPad.Api.Controllers.UserProfile;
public class UserProfileResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
    public string AccountNumber { get; set; }
    public float Balance { get; set; }
}

public class UserProfileResponseProfile : Profile
{
    public UserProfileResponseProfile()
    {
        CreateMap<UserProfileModel, UserProfileResponse>();
    }
}