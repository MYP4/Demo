using System.Net.Http.Json;

namespace EventPad.Web.Pages.Profiles;

public class ProfileService(HttpClient httpClient) : IProfileService
{
    public async Task<ProfileModel> Me()
    {
        var response = await httpClient.GetAsync($"v1/profile");

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            //throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<ProfileModel>() ?? new();
    }
}
