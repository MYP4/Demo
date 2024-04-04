using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Api.Service.Users;
using EventPad.Api.Services.Events;
using EventPad.Common;
using EventPad.Services.Actions;
using Microsoft.EntityFrameworkCore;


namespace EventPad.Api.Services.Specific;

public class SpecificEventService : ISpecificEventService
{
    private readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateSpecificModel> createModelValidator;
    private readonly IModelValidator<UpdateSpecificEventModel> updateModelValidator;
    private readonly IAction action;
    private readonly IRightsService rightsService;
    private readonly IEventService eventService;

    public SpecificEventService(IDbContextFactory<ApiDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateSpecificModel> createModelValidator,
        IModelValidator<UpdateSpecificEventModel> updateModelValidator,
        IAction action,
        IRightsService rightsService,
        IEventService eventService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this.action = action;
        this.rightsService = rightsService;
        this.eventService = eventService;
    }


    public async Task<IEnumerable<SpecificEventModel>> GetAllSpecificEvents(Guid userId, int page = 1, int pageSize = 10, SpecificEventModelFilter filter = null)
    {
        var price = filter?.Price;
        var address = filter?.Address;
        var day = filter?.DayOfWeek;
        var date = filter?.Date;
        var time = filter?.Time;
        var _private = filter?.Private;

        using var context = await dbContextFactory.CreateDbContextAsync();

        var events = context.SpecificEvents.AsQueryable();


        if (!await rightsService.IsAdmin(userId))
        {
            events = events.Where(x => x.Private == false);
        }
        if (price != null)
        {
            events = events.Where(x => x.Price == price);
        }
        if (address != null)
        {
            events = events.Where(x => x.Address == address);
        }
        if (day != null)
        {
            events = events.Where(x => x.DayOfWeek == day);
        }
        if (date != null)
        {
            events = events.Where(x => x.Date == date);
        }
        if (_private != null)
        {
            events = events.Where(x => x.Private == _private);
        }
        if (time != null)
        {
            events = events.Where(x => x.Time == time);
        }

        events = events.Skip((page - 1) * pageSize).Take(pageSize);

        var eventList = await events.ToListAsync();

        var result = mapper.Map<IEnumerable<SpecificEventModel>>(eventList);

        return result;
    }

    public async Task<IEnumerable<SpecificEventModel>> GetCurrentSpecificEvents(Guid id, int page = 1, int pageSize = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var events = context.SpecificEvents.AsQueryable().Where(e => e.Event.Uid == id);

        events = events.Skip((page - 1) * pageSize).Take(pageSize);

        var eventList = await events.ToListAsync();

        var result = mapper.Map<IEnumerable<SpecificEventModel>>(eventList);

        return result;
    }

    public async Task<SpecificEventModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.SpecificEvents.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<SpecificEventModel>(_event);

        return result;
    }

    public async Task<SpecificEventModel> GetByCode(string code)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.SpecificEvents.FirstOrDefaultAsync(x => x.Code == code);

        var result = mapper.Map<SpecificEventModel>(_event);

        return result;
    }

    public async Task<SpecificEventModel> Create(CreateSpecificModel model, Guid userId)
    {
        var eventId = model.EventId;

        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await eventService.GetById(eventId);

        if (_event.AdminId != userId)
            throw new ProcessException($"You don't have access to this feature");

        var specEvent = mapper.Map<SpecificEvent>(model);

        await context.SpecificEvents.AddAsync(specEvent);

        await action.CreateEventAccount(new CreateEventAccount()
        {
            Id = specEvent.Uid,
        });

        await context.SaveChangesAsync();

        return mapper.Map<SpecificEventModel>(specEvent);
    }

    public async Task<SpecificEventModel> Update(Guid id, UpdateSpecificEventModel model, Guid userId)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.SpecificEvents.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Specific Event (ID = {id}) not found.");

        if (!await rightsService.IsAdmin(userId))
        {
            if (_event.Event.Admin.Id != userId)
                throw new ProcessException($"You don't have access to this feature");
        }

        _event = mapper.Map(model, _event);

        context.SpecificEvents.Update(_event);

        await context.SaveChangesAsync();

        return mapper.Map<SpecificEventModel>(_event);
    }

    public async Task Delete(Guid id, Guid userId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.SpecificEvents.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Specific Event (ID = {id}) not found.");

        if (!await rightsService.IsAdmin(userId))
        {
            if (_event.Event.Admin.Id != userId)
                throw new ProcessException($"You don't have access to this feature");
        }

        await action.DeleteEventAccount(_event.Uid);

        context.SpecificEvents.Remove(_event);

        await context.SaveChangesAsync();
    }
}
