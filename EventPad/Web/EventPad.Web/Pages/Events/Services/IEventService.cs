using EventPad.Web.Pages.Events.Models;

namespace EventPad.Web.Pages.Events.Services;

public interface IEventService
{
    Task<IEnumerable<EventModel>> GetEvents();
    Task<EventModel> GetEvent(Guid eventId);
    Task<EventModel> GetEventByCode(string code);
    Task AddEvent(CreateModel model);
    Task EditEvent(Guid eventId, UpdateModel model);
    Task DeleteEvent(Guid eventId);
}
