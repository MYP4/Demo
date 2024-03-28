namespace EventPad.Web.Pages.Users.Models;

using EventPad.Api.Context.Entities;

public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }
}

