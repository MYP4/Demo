namespace EventPad.Api.Services.Events;

public interface IEventService
{
    Task<IEnumerable<EventModel>> GetAllEvents(int page = 1, int pageSize = 10, EventModelFilter filter = null);
    Task<IEnumerable<EventModel>> GetUserEvents(Guid id, int page = 1, int pageSize = 10);
    Task<EventModel> GetById(Guid id);
    Task<EventModel> Create(CreateEventModel model);
    Task<EventModel> Update(Guid id, UpdateEventModel model, Guid userId);
    Task Delete(Guid id, Guid userId);
}
