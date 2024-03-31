using EventPad.Web.Pages.Profiles.Models;

namespace EventPad.Web.Pages.Profiles.Services;

public interface IProfileService
{
    Task<ProfileResult> Register(SignUpModel model);

    Task<string> GetPasswordRecoveryToken();

    Task<ProfileResult> SendPasswordRecoveryLink(SendEmailModel model);
    
    Task<ProfileResult> ChangePassword(ChangePasswordModel model);
    
    Task<UserModel> GetUserData();
    
    Task<ProfileResult> ConfirmEmail(ConfirmEmailModel model);
    
    Task<ProfileResult> SendConfirmationEmail(SendEmailModel model);
}
