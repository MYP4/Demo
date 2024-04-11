using EventPad.Web.Common.Files;

namespace EventPad.Web.Pages.Profiles;

public class UpdateProfileModel
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public FilePayload? Image { get; set; }
}
