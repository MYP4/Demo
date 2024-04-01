using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using EventPad.Web.Pages.Auth.Models;
using EventPad.Web.Providers;
using EventPad.Web.Pages.Profiles.Models;
using System.Net.Http.Json;
using System.Net.Http;

namespace EventPad.Web.Pages.Auth.Services;

public class AuthService : IAuthService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";

    private readonly HttpClient httpClient;
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly ILocalStorageService localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider = authenticationStateProvider;
        this.localStorage = localStorage;
    }


    //public async Task<AuthResult> Register(RegisterModel registerModel)
    //{
    //    var requestContent = JsonContent.Create(registerModel);
    //    var response = await httpClient.PostAsync($"{Settings.ApiRoot}v1/User", requestContent);

    //    var content = await response.Content.ReadAsStringAsync();

    //    var result = JsonSerializer.Deserialize<ProfileResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ProfileResult();
    //    result.IsSuccessful = response.IsSuccessStatusCode;

    //    return result;
    //}


    public async Task<AuthResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.ApiRoot}/connect/token";

        var request_body = new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Email!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(request_body);

        var response = await httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<AuthResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AuthResult();
        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        return loginResult;
    }


    public async Task Logout()
    {
        await localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();

        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
