using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Services.UserAccounts;

public class UserAccountService : IUserAccountService
{
    public Task<UserAccountModel> Create(CreateUserAccountModel model)
    {
        throw new NotImplementedException();
    }

    public Task<UserAccount> Create()
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserAccountModel> GetEventAccountById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserAccountModel>> GetEventAccounts()
    {
        throw new NotImplementedException();
    }

    public Task<UserAccountModel> Update(Guid id, UpdateUserAccountModel model)
    {
        throw new NotImplementedException();
    }
}
