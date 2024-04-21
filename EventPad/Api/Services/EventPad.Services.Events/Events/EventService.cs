using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Api.Service.Users;
using EventPad.Common;
using EventPad.Settings;
using EventPad.Services.Actions;
using Microsoft.EntityFrameworkCore;
using EventPad.Redis;
using StackExchange.Redis;

namespace EventPad.Api.Services.Events;

public class EventService : IEventService
{
    private readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateEventModel> createModelValidator;
    private readonly IModelValidator<UpdateEventModel> updateModelValidator;
    private readonly IRightsService rightsService;
    private readonly MainSettings mainSettings;
    private readonly IRedisService redisService;

    public EventService(IDbContextFactory<ApiDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateEventModel> createModelValidator,
        IModelValidator<UpdateEventModel> updateModelValidator,
        IRightsService rightsService,
        MainSettings mainSettings,
        IRedisService redisService)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this.rightsService = rightsService;
        this.mainSettings = mainSettings;
        this.redisService = redisService;
    }


    public async Task<IEnumerable<EventModel>> GetAllEvents(int page = 1, int pageSize = 10, EventModelFilter filter = null)
    {
        var redisKey = $"AllEvents";
        var redisData = await redisService.Get<IEnumerable<EventModel>>(redisKey);

        if (filter == null)
        {
            if (redisData != null)
            {
                return redisData;
            }
        }

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

        await redisService.Put(redisKey, result);

        return result;
    }

    public async Task<IEnumerable<EventModel>> GetUserEvents(Guid id, int page = 1, int pageSize = 10)
    {
        var redisKey = $"User{id}Events";
        var redisData = await redisService.Get<IEnumerable<EventModel>>(redisKey);

        if (redisData != null)
        {
            return redisData;
        }

        using var context = await dbContextFactory.CreateDbContextAsync();

        var events = context.Events.AsQueryable().Where(e => e.Admin.Id == id);

        events = events.Skip((page - 1) * pageSize).Take(pageSize);

        var eventList = await events.ToListAsync();

        var result = mapper.Map<IEnumerable<EventModel>>(eventList);

        await redisService.Put(redisKey, result);

        return result;
    }

    public async Task<EventModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Event (ID = {id}) not found.");

        var result = mapper.Map<EventModel>(_event);

        return result;
    }

    public async Task<EventModel> Create(CreateEventModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = mapper.Map<Event>(model);

        var fileName = await model.Image.SaveToFile(Path.Combine(mainSettings.RootDir, mainSettings.FileDir));

        _event.Uid = Guid.NewGuid();
        _event.Image = fileName;

        await context.Events.AddAsync(_event);

        await context.SaveChangesAsync();

        return mapper.Map<EventModel>(_event);
    }

    public async Task<EventModel> Update(Guid id, UpdateEventModel model, Guid userId)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Event (ID = {id}) not found.");

        if (!await rightsService.IsAdmin(userId))
        {
            if (_event.Admin.Id != userId)
                throw new ProcessException($"You don't have access to this feature");
        }

        _event = mapper.Map(model, _event);

        var fileName = await model.Image.SaveToFile(Path.Combine(mainSettings.RootDir, mainSettings.FileDir));

        _event.Image = fileName;

        context.Events.Update(_event);

        await context.SaveChangesAsync();

        return mapper.Map<EventModel>(_event);
    }

    public async Task Delete(Guid id, Guid userId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var _event = await context.Events.FirstOrDefaultAsync(x => x.Uid == id);

        if (_event == null)
            throw new ProcessException($"Event (ID = {id}) not found.");

        if (!await rightsService.IsAdmin(userId))
        {
            if (_event.Admin.Id != userId)
                throw new ProcessException($"You don't have access to this feature");
        }

        context.Events.Remove(_event);

        await context.SaveChangesAsync();
    }
}
