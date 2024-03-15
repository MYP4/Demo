namespace EventPad.Pay.Services.UserAccounts;

public interface IUserAccountService
{
    Task<IEnumerable<UserAccountModel>> GetUserAccounts();
    Task<UserAccountModel> GetUserAccountById(Guid id);
    Task<UserAccountModel> Create(CreateUserAccountModel model);
    Task<UserAccountModel> Update(Guid id, UpdateUserAccountModel model);
    Task Delete(Guid id);
}
