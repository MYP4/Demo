using EventPad.Api.Context.Entities;

namespace EventPad.Web.Pages.Profiles;

public class ProfileModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }
    public string Email { get; set; }

    public string AccountNumber { get; set; }
    public float Balance { get; set; }

    public string Image {  get; set; }
}
