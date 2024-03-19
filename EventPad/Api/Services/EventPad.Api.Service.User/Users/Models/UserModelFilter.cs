using EventPad.Api.Context.Entities;

namespace EventPad.Api.Services.Users;

public class UserModelFilter
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public UserRole? Role { get; set; }
    public float? Rating { get; set; }
}
