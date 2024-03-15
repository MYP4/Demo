using AutoMapper;
using EventPad.Common;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Pay.Services.EventAccounts;

public class EventAccountService : IEventAccountService
{
    private readonly IDbContextFactory<PayDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateEventAccountModel> createModelValidator;
    private readonly IModelValidator<UpdateEventAccountModel> updateModelValidator;

    public EventAccountService(IDbContextFactory<PayDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateEventAccountModel> createModelValidator,
        IModelValidator<UpdateEventAccountModel> updateModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }


    public async Task<IEnumerable<EventAccountModel>> GetEventAccounts()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var eventAccounts = context.EventAccounts.AsQueryable();

        var eventAccountList = await eventAccounts.ToListAsync();

        var result = mapper.Map<IEnumerable<EventAccountModel>>(eventAccountList);

        return result;
    }

    public async Task<EventAccountModel> GetEventAccountById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var eventAccount = await context.EventAccounts.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<EventAccountModel>(eventAccount);

        return result;
    }

    public async Task<EventAccountModel> Create(CreateEventAccountModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var eventAccount = new EventAccount()
        {
            Uid = model.EventId,
            AccountNumber = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString().PadLeft(16, '0'),
            Balance = 0
        };

        await context.EventAccounts.AddAsync(eventAccount);

        await context.SaveChangesAsync();

        return mapper.Map<EventAccountModel>(eventAccount);
    }

    public async Task<EventAccountModel> Update(Guid id, UpdateEventAccountModel model, PayDbContext context = null)
    {
        await updateModelValidator.CheckAsync(model);

        using var localContext = await dbContextFactory.CreateDbContextAsync();

        var ctx = context ?? localContext;

        var eventAccount = await ctx.EventAccounts.FirstOrDefaultAsync(x => x.Uid == id);

        if (eventAccount == null)
            throw new ProcessException($"UserAccount (ID = {id}) not found.");

        eventAccount.Balance += model.Amount;

        ctx.EventAccounts.Update(eventAccount);

        if (context == null)
            await ctx.SaveChangesAsync();

        return mapper.Map<EventAccountModel>(eventAccount);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var eventAccount = await context.EventAccounts.FirstOrDefaultAsync(x => x.Uid == id);

        if (eventAccount == null)
            throw new ProcessException($"UserAccount (ID = {id}) not found.");

        context.EventAccounts.Remove(eventAccount);

        await context.SaveChangesAsync();
    }
}
