using AutoMapper;
using EventPad.Common;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Pay.Services.UserAccounts;

public class UserAccountService : IUserAccountService
{
    private readonly IDbContextFactory<PayDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateUserAccountModel> createModelValidator;
    private readonly IModelValidator<UpdateUserAccountModel> updateModelValidator;

    public UserAccountService(IDbContextFactory<PayDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateUserAccountModel> createModelValidator,
        IModelValidator<UpdateUserAccountModel> updateModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }


    public async Task<IEnumerable<UserAccountModel>> GetUserAccounts()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var userAccounts = context.UserAccounts.AsQueryable();

        var userAccountList = await userAccounts.ToListAsync();

        var result = mapper.Map<IEnumerable<UserAccountModel>>(userAccountList);

        return result;
    }

    public async Task<UserAccountModel> GetUserAccountById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var userAccount = await context.UserAccounts.FirstOrDefaultAsync(x => x.Uid == id);

        if (userAccount == null)
            throw new ProcessException($"UserAccount (ID = {id}) not found.");

        var result = mapper.Map<UserAccountModel>(userAccount);

        return result;
    }

    public async Task<UserAccountModel> Create(CreateUserAccountModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var userAccount = new UserAccount()
        {
            Uid = model.UserId,
            AccountNumber = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString().PadLeft(16, '0'),
            Balance = 0
        };

        await context.UserAccounts.AddAsync(userAccount);

        return mapper.Map<UserAccountModel>(userAccount);
    }


    public async Task<UserAccountModel> Update(Guid id, UpdateUserAccountModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var userAccount = await context.UserAccounts.FirstOrDefaultAsync(x => x.Uid == id);

        if (userAccount == null)
            throw new ProcessException($"UserAccount (ID = {id}) not found.");

        userAccount.Balance += model.Amount;

        context.UserAccounts.Update(userAccount);

        await context.SaveChangesAsync();

        return mapper.Map<UserAccountModel>(userAccount);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var userAccount = await context.UserAccounts.FirstOrDefaultAsync(x => x.Uid == id);

        if (userAccount == null)
            throw new ProcessException($"UserAccount (ID = {id}) not found.");

        context.Remove(userAccount);
    }
}
