using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Services.UserAccounts;

public interface IUserAccountService
{
    Task<IEnumerable<UserAccountModel>> GetEventAccounts();
    Task<UserAccountModel> GetEventAccountById(Guid id);
    Task<UserAccountModel> Create(CreateUserAccountModel model);
    Task<UserAccount> Create();
    Task<UserAccountModel> Update(Guid id, UpdateUserAccountModel model);
    Task Delete(Guid id);
}
