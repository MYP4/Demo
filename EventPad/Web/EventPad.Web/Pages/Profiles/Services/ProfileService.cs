using EventPad.Web.Pages.Profiles.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace EventPad.Web.Pages.Profiles.Services;

public class ProfileService(HttpClient httpClient) : IProfileService
{
    public async Task<string> GetPasswordRecoveryToken()
    {
        var url = $"{Settings.ApiRoot}/v1/User/password-recovery-token";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<string>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? string.Empty;
    }

    public async Task<ProfileResult> SendPasswordRecoveryLink(SendEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/User/forgot-password";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        ProfileResult result;
        try
        {
            result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
        }
        catch
        {
            result = new ProfileResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<ProfileResult> ChangePassword(ChangePasswordModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/User/change-password";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        ProfileResult result;
        try
        {
            result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
        }
        catch
        {
            result = new ProfileResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<ProfileResult> ConfirmEmail(ConfirmEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/user/confirm-email";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        ProfileResult result;
        try
        {
            result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
        }
        catch
        {
            result = new ProfileResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<ProfileResult> SendConfirmationEmail(SendEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/user/confirmation-email";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        ProfileResult result;
        try
        {
            result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
        }
        catch
        {
            result = new ProfileResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<ProfileModel> Me()
    {
        var response = await httpClient.GetAsync($"v1/profile");

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<ProfileModel>() ?? new();
    }
}
