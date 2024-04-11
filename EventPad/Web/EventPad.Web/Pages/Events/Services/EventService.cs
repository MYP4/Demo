using System.Net.Http.Json;

namespace EventPad.Web.Pages.Events;

public class EventService(HttpClient httpClient) : IEventService
{
    public async Task<IEnumerable<EventModel>> GetEvents()
    {
        var response = await httpClient.GetAsync("v1/event");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<EventModel>>() ?? new List<EventModel>();
    }

    public async Task<IEnumerable<EventModel>> GetUserEvents()
    {
        var response = await httpClient.GetAsync("v1/event/user");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<EventModel>>() ?? new List<EventModel>();
    }

    public async Task<EventModel> GetEvent(Guid eventId)
    {
        var response = await httpClient.GetAsync($"v1/event/{eventId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<EventModel>() ?? new();
    }

    public async Task<EventModel> GetEventByCode(string code)
    {
        var response = await httpClient.GetAsync($"v1/event/{code}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<EventModel>() ?? new();
    }

    public async Task AddEvent(CreateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/event", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditEvent(Guid eventId, UpdateModel model)
    {
        var requestContent = JsonContent.Create(model);

        var response = await httpClient.PutAsync($"v1/event/{eventId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteEvent(Guid eventId)
    {
        var response = await httpClient.DeleteAsync($"v1/event/{eventId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
