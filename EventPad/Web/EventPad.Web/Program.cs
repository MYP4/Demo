using Blazored.LocalStorage;
using EventPad.Web;
using EventPad.Web.Pages.Auth;
using EventPad.Web.Pages.Events;
using EventPad.Web.Pages.Profiles;
using EventPad.Web.Pages.SpecificEvents;
using EventPad.Web.Pages.Tickets;
using EventPad.Web.Pages.Users;
using EventPad.Web.Providers;
using EventPad.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISpecificService, SpecificService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
