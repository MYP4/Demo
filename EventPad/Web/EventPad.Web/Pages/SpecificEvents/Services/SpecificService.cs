
using System.Net.Http.Json;

namespace EventPad.Web.Pages.SpecificEvents;

public class SpecificService(HttpClient httpClient) : ISpecificService
{
    public async Task<IEnumerable<SpecificModel>> GetSpecifics()
    {
        var response = await httpClient.GetAsync("v1/specific-event");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<SpecificModel>>() ?? new List<SpecificModel>();
    }


    public async Task<IEnumerable<SpecificModel>> GetEventSpecifics()
    {
        var response = await httpClient.GetAsync("v1/specific-event/event");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<SpecificModel>>() ?? new List<SpecificModel>();
    }

    public async Task<SpecificModel> GetSpecific(Guid specificId)
    {
        var response = await httpClient.GetAsync($"v1/specific-event/{specificId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<SpecificModel>() ?? new();
    }

    public async Task<SpecificModel> GetSpecificByCode(string code)
    {
        var response = await httpClient.GetAsync($"v1/specific-event/{code}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<SpecificModel>() ?? new();
    }

    public async Task AddSpecific(CreateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/specific-event", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditSpecific(Guid specificId, UpdateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/specific-event/{specificId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteSpecific(Guid specificId)
    {
        var response = await httpClient.DeleteAsync($"v1/specific-event/{specificId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
