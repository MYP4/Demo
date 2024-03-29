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
}


public class UserProfileModelProfile : Profile
{
    public UserProfileModelProfile()
    {
        CreateMap<User, UserProfileModel>();
        //.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
        //.ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
        //.ForMember(d => d.SecondName, o => o.MapFrom(s => s.SecondName))
        //.ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
        //;
    }
}