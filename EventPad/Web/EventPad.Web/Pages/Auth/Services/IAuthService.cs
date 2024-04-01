using EventPad.Web.Pages.Auth.Models;
using EventPad.Web.Pages.Profiles.Models;

namespace EventPad.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<AuthResult> Login(LoginModel loginModel);
    Task Register(RegisterModel registerModel);
    Task Logout();
}
