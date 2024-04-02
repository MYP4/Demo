using AutoMapper;
using EventPad.Api.Context.Entities;

namespace EventPad.Api.Service.Users;
public class UserProfileModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
    public string AccountNumber { get; set; }
    public float Balance { get; set; }
}


public class UserProfileModelProfile : Profile
{
    public UserProfileModelProfile()
    {
        CreateMap<User, UserProfileModel>();
    }
}

