using EventPad.Web.Pages.Profiles.Models;
using System.Text.Json;
using System.Text;

namespace EventPad.Web.Pages.Profiles.Services;

public class ProfileService(HttpClient httpClient) : IProfileService
{
    private readonly HttpClient httpClient;


    public async Task<ProfileResult> Register(SignUpModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/profiles";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<string> GetPasswordRecoveryToken()
    {
        var url = $"{Settings.ApiRoot}/v1/profiles/password-recovery-token";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<string>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? string.Empty;
    }

    public async Task<ProfileResult> SendPasswordRecoveryLink(SendEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/profiles/forgot-password";

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
        var url = $"{Settings.ApiRoot}/v1/profiles/change-password";

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

    public async Task<UserModel> GetUserData()
    {
        var url = $"{Settings.ApiRoot}/v1/profiles/user";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<UserModel>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new UserModel();

        return data;
    }

    public async Task<ProfileResult> ConfirmEmail(ConfirmEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/profiles/confirm-email";

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
        var url = $"{Settings.ApiRoot}/v1/profiles/confirmation-email";

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
}
