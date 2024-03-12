using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Actions;
using EventPad.Common;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Events;

public class EventService : IEventService
{
    private readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IAction action;
    private readonly IModelValidator<CreateEventModel> createModelValidator;
    private readonly IModelValidator<UpdateEventModel> updateModelValidator;

    public EventService(IDbContextFactory<ApiDbContext> dbContextFactory,
        IMapper mapper,
        IAction action,
        IModelValidator<CreateEventModel> createModelValidator,
        IModelValidator<UpdateEventModel> updateModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.action = action;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
    }


    public async Task<IEnumerable<EventModel>> GetAllEvents(int page = 1, int pageSize = 10, EventModelFilter filter = null)
    {
        var name = filter?.Name;
        var minPrice = filter?.MinPrice;
        var maxPrice = filter?.MaxPrice;
        var type = filter?.Type;

        using var context = await dbContextFactory.CreateDbContextAsync();

        var events = context.Events.AsQueryable();

        if (name != null)
        {
            events = events.Where(e => e.Name.ToLower().Contains(name.ToLower()));
        }

        if (maxPrice != null)
        {
            events = events.Where(e => e.Price <= maxPrice);
        }

        if (minPrice != null)
        {
            events = events.Where(e => e.Price >= minPrice);
        }


        if (type != null)
        {
            events = events.Where(e => e.Type == type);
        }

        events = events.Skip((page - 1) * pageSize).Take(pageSize);

        var eventList = await events.ToListAsync();

        var result = mapper.Map<IEnumerable<EventModel>>(eventList);

        return result;
    }

    public async Task<IEnumerable<EventModel>> GetUserEvents(Guid id, int page = 1, int pageSize = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var events = context.Events.AsQueryable();

       

        events = events.Skip((page - 1) * pageSize).Take(pageSize);

        var eventList = await events.ToListAsync();

        var result = mapper.Map<IEnumerable<EventModel>>(eventList);

        return result;
    }

    public async Task<EventModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<EventModel>(_event);

        return result;
    }

    public async Task<EventModel> Create(CreateEventModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = mapper.Map<Event>(model);

        _event.Uid = Guid.NewGuid();

        await context.Events.AddAsync(_event);

        await context.SaveChangesAsync();

        await action.CreateEventAccount(new CreateEventAccount()
        {
            Id =  _event.Uid,
        });

        return mapper.Map<EventModel>(_event);
    }

    public async Task<EventModel> Update(Guid id, UpdateEventModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        _event = mapper.Map(model, _event);

        context.Events.Update(_event);

        await context.SaveChangesAsync();

        return mapper.Map<EventModel>(_event);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Event (ID = {id}) not found.");

        //var eventAccount = await context.EventAccounts.FirstOrDefaultAsync(x => x.EventId == _event.Id);

        //if (_event == null)
        //  throw new ProcessException($"EventAccount (ID = {id}) not found.");

        context.Events.Remove(_event);
        //context.EventAccounts.Remove(eventAccount);

        await context.SaveChangesAsync();
    }
}
