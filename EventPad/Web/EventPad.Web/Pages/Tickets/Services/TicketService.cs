using System.Net.Http.Json;

namespace EventPad.Web.Pages.Tickets;

public class TicketService(HttpClient httpClient) : ITicketService
{
    public async Task<IEnumerable<TicketModel>> GetAllTickets(int page = 1, int pageSize = 10, TicketModelFilter filter = null)
    {
        var response = await httpClient.GetAsync("v1/ticket");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<TicketModel>>() ?? new List<TicketModel>();
    }

    public async Task<IEnumerable<TicketModel>> GetUserTickets(int page = 1, int pageSize = 10)
    {
        var response = await httpClient.GetAsync("v1/ticket/user");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<TicketModel>>() ?? new List<TicketModel>();
    }

    public async Task<IEnumerable<TicketModel>> GetSpecificTickets(Guid specificId, int page = 1, int pageSize = 10)
    {
        var response = await httpClient.GetAsync($"v1/ticket/specific/{specificId}");

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<TicketModel>>() ?? new List<TicketModel>();
    }

    public async Task<TicketModel> GetById(Guid id)
    {
        var response = await httpClient.GetAsync($"v1/ticket/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<TicketModel>() ?? new();
    }

    public async Task AddTicket(CreateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/ticket", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditTicket(Guid id, UpdateModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/ticket/{id}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteTicket(Guid id)
    {
        var response = await httpClient.DeleteAsync($"v1/ticket/{id}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
