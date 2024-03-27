using EventPad.Web.Pages.Auth.Models;

namespace EventPad.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}
