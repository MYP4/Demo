using EventPad.Api.Context.Entities;

namespace EventPad.Api.Service.Users;

public interface IUserService
{
    Task<bool> IsEmpty();

    Task<IEnumerable<UserModel>> GetAllUsers(int page = 1, int pageSize = 10, UserModelFilter filter = null);
    Task<UserModel> GetById(Guid id);
    Task<UserModel> Create(RegiserUserModel model);
    Task<UserModel> Update(Guid id, UpdateUserModel model, Guid userId);
    Task Delete(Guid id, Guid userId);

    Task SetRights(Guid id, UserRole role, Guid userId);
}
