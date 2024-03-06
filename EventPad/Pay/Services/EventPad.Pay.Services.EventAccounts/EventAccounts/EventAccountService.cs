
using AutoMapper;
using EventPad.Common.Exceptions;
using EventPad.Common.Validator;
using EventPad.Context;
using EventPad.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Services.EventAccounts;

public class EventAccountService : IEventAccountService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateEventAccountModel> createModelValidator;
    private readonly IModelValidator<UpdateEventAccountModel> updateModelValidator;

    public EventAccountService(IDbContextFactory<MainDbContext> dbContextFactory,
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

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Event (ID = {id}) not found.");

        var eventAccount = await context.EventAccounts.FirstOrDefaultAsync(x => x.EventId == _event.Id);

        var result = mapper.Map<EventAccountModel>(eventAccount);

        return result;
    }

    public async Task<EventAccountModel> Create(CreateEventAccountModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var eventAccount = mapper.Map<EventAccount>(model);

        await context.EventAccounts.AddAsync(eventAccount);

        return mapper.Map<EventAccountModel>(eventAccount);
    }

    public async Task<EventAccount> Create()
    {
        var eventAccount = new EventAccount()
        {
            AccountNumber = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString().PadLeft(16, '0'),
            Balance = 0
        };

        using var context = await dbContextFactory.CreateDbContextAsync();

        await context.EventAccounts.AddAsync(eventAccount);

        return eventAccount;
    }

    public async Task<EventAccountModel> Update(Guid id, UpdateEventAccountModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Event (ID = {id}) not found.");

        var eventAccount = await context.EventAccounts.FirstOrDefaultAsync(x => x.EventId == _event.Id);

        eventAccount = mapper.Map(model, eventAccount);

        context.EventAccounts.Update(eventAccount);

        await context.SaveChangesAsync();

        return mapper.Map<EventAccountModel>(eventAccount);
    }
}
