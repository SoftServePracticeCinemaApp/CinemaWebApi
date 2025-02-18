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
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Handle 401 Unauthorized responses
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // Clear the token if unauthorized
            await _localStorageService.RemoveItemAsync("accessToken", cancellationToken);
        }

        return response;
    }
} 