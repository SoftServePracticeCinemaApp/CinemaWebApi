using Blazored.LocalStorage;
using Cinema.UI.Services;
using Cinema.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAccountManagement, AuthStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddHttpClient("AuthServiceUrl", opt => opt.BaseAddress =
new Uri(builder.Configuration["AuthServiceUrl"] ?? "https://localhost:0000"))
    .AddHttpMessageHandler<HttpHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
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
