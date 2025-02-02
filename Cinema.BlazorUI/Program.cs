using Cinema.BlazorUI;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cinema.BlazorUI.Services;
using Cinema.BlazorUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("AuthServiceUrl", opt => 
{
    opt.BaseAddress = new Uri(builder.Configuration["AuthServiceUrl"] ?? "http://localhost:5254/");
})
.AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddHttpClient("WebApiUrl", opt => 
{
    opt.BaseAddress = new Uri(builder.Configuration["WebApiUrl"] ?? "https://localhost:7005/");
})
.AddHttpMessageHandler<CustomHttpHandler>();

await builder.Build().RunAsync();
