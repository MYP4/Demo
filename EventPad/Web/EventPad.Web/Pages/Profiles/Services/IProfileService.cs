using EventPad.Web.Pages.Profiles.Models;

namespace EventPad.Web.Pages.Profiles.Services;

public interface IProfileService
{
    Task<string> GetPasswordRecoveryToken();

    Task<ProfileResult> SendPasswordRecoveryLink(SendEmailModel model);
    
    Task<ProfileResult> ChangePassword(ChangePasswordModel model);
    
    Task<ProfileResult> ConfirmEmail(ConfirmEmailModel model);
    
    Task<ProfileResult> SendConfirmationEmail(SendEmailModel model);

    Task<ProfileModel> Me();
}
