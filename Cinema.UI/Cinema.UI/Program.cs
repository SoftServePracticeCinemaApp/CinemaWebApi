using Blazored.LocalStorage;
using Cinema.UI.Services;
using Cinema.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationCore();

// Add test authentication for development
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomHttpHandler>();
// builder.Services.AddScoped<IAccountManagement, AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAccountManagement>(sp => 
    (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddHttpClient("AuthServiceUrl", opt => 
{
    opt.BaseAddress = new Uri(builder.Configuration["AuthServiceUrl"] ?? "https://localhost:7006/api/auth/");
})
.AddHttpMessageHandler<CustomHttpHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
  // app.UseAuthentication();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<Cinema.UI.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Cinema.UI.Client._Imports).Assembly);

app.Run();
