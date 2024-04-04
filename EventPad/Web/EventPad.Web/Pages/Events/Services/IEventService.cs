namespace EventPad.Web.Pages.Events;

public interface IEventService
{
    Task<IEnumerable<EventModel>> GetEvents();
    Task<IEnumerable<EventModel>> GetUserEvents();
    Task<EventModel> GetEvent(Guid eventId);
    Task<EventModel> GetEventByCode(string code);
    Task AddEvent(CreateModel model);
    Task EditEvent(Guid eventId, UpdateModel model);
    Task DeleteEvent(Guid eventId);
}
