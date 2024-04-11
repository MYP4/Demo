using EventPad.Web.Pages.Profiles;
using System.Net.Http.Json;

namespace EventPad.Web.Pages.Users;

public class UserService(HttpClient httpClient) : IUserService
{
    public async Task DeleteUser(Guid userId)
    {
        var response = await httpClient.DeleteAsync($"v1/user/{userId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditUser(Guid userId, UpdateProfileModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/user/{userId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<UserModel> GetUser(Guid userId)
    {
        var response = await httpClient.GetAsync($"v1/user/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<UserModel>() ?? new();
    }

    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        var response = await httpClient.GetAsync("/v1/user");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>() ?? new List<UserModel>();
    }
}
