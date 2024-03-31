using EventPad.Api.Context.Entities;

namespace EventPad.Web.Pages.Profiles.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
}
