namespace EventPad.Web.Pages.Profiles;

public interface IProfileService
{
    Task<ProfileModel> Me();
}
