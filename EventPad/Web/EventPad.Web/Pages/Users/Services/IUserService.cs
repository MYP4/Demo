namespace EventPad.Web.Pages.Users;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetUsers();
    Task<UserModel> GetUser(Guid userId);
    Task EditUser(Guid userId, UpdateModel model);
    Task DeleteUser(Guid userId);
}
