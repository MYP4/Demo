namespace EventPad.Web.Pages.Profiles;

public interface IProfileService
{
    //Task<string> GetPasswordRecoveryToken();

    //Task<ProfileResult> SendPasswordRecoveryLink(SendEmailModel model);

    //Task<ProfileResult> ChangePassword(ChangePasswordModel model);

    //Task<ProfileResult> ConfirmEmail(ConfirmEmailModel model);

    //Task<ProfileResult> SendConfirmationEmail(SendEmailModel model);

    Task<ProfileModel> Me();
}
