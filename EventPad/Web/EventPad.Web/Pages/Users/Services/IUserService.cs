using EventPad.Web.Pages.Users.Models;

namespace EventPad.Web.Pages.Users.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetUsers();
    Task<UserModel> GetUser(Guid userId);
    Task AddUser(CreateModel model);
    Task EditUser(Guid userId, UpdateModel model);
    Task DeleteUser(Guid userId);
}
