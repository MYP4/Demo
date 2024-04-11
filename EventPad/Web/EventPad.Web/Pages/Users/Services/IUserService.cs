using EventPad.Web.Pages.Profiles;

namespace EventPad.Web.Pages.Users;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetUsers();
    Task<UserModel> GetUser(Guid userId);
    Task EditUser(Guid userId, UpdateProfileModel model);
    Task DeleteUser(Guid userId);
}
