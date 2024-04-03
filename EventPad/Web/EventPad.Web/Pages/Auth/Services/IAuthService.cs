using EventPad.Web.Pages.Profiles;

namespace EventPad.Web.Pages.Auth;

public interface IAuthService
{
    Task<AuthResult> Login(LoginModel loginModel);
    Task Register(RegisterModel registerModel);
    Task Logout();
}
