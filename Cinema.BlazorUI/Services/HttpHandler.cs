using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Cinema.BlazorUI.Services;

public class CustomHttpHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;

    public CustomHttpHandler(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorageService.GetItemAsync<string>("accessToken", cancellationToken);
        
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine($"Setting Authorization header: Bearer {token}");
        }

        

        

        Console.WriteLine($"Token: {token}");
        Console.WriteLine($"Authorization header: {request.Headers.Authorization}");

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await _localStorageService.RemoveItemAsync("accessToken", cancellationToken);
        }

        return response;
    }
} 