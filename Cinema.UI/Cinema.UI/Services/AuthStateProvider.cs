using Cinema.UI.Services.Interfaces;
using Cinema.UI.Model;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Cinema.UI.Services;

namespace Cinema.UI.Services;
public class AuthStateProvider : AuthenticationStateProvider, IAccountManagement

{
  private bool _authenticated = false;

  private readonly ClaimsPrincipal Unauthenticated =
     new(new ClaimsIdentity());

  private readonly HttpClient _httpClient;


  private readonly JsonSerializerOptions jsonSerializerOptions =
    new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
  private readonly ILocalStorageService _localStorageService;

  public AuthStateProvider(IHttpClientFactory httpClientFactory,
      ILocalStorageService localStorageService)
  {
    _httpClient = httpClientFactory.CreateClient("AuthServiceUrl");
    _localStorageService = localStorageService;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    _authenticated = false;
    var user = Unauthenticated;

    try
    {
        var userResponse = await _httpClient.GetAsync("manage/info");
        userResponse.EnsureSuccessStatusCode();

        var userJson = await userResponse.Content.ReadAsStringAsync();
        var userInfo = JsonSerializer.Deserialize<UserInfo>(userJson, jsonSerializerOptions);

        if (userInfo?.Email != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userInfo.Email),
                new(ClaimTypes.Email, userInfo.Email)
            };

            if (userInfo.Claims != null)
            {
                claims.AddRange(
                    userInfo.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email)
                        .Select(c => new Claim(c.Key, c.Value)));
            }

            var rolesResponse = await _httpClient.GetAsync($"Role/GetuserRole?userEmail={userInfo.Email}");
            rolesResponse.EnsureSuccessStatusCode();

            var rolesJson = await rolesResponse.Content.ReadAsStringAsync();
            var roles = JsonSerializer.Deserialize<string[]>(rolesJson, jsonSerializerOptions);
            
            if (roles != null && roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    claims.Add(new(ClaimTypes.Role, role));
                }
            }

            var id = new ClaimsIdentity(claims, nameof(AuthStateProvider));
            user = new ClaimsPrincipal(id);
            _authenticated = true;
        }
    }
    catch (Exception)
    {
        _authenticated = false;
        user = Unauthenticated;
    }

    return new AuthenticationState(user);
  }

  public async Task<FormResult> RegisterAsync(string email, string password, string phone, string name)
  {
    string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

    try
    {
        var result = await _httpClient.PostAsJsonAsync("register",
              new { email, password, phone, name });
        if (result.IsSuccessStatusCode)
        {
            return new FormResult { Succeeded = true };
        }

        var details = await result.Content.ReadAsStringAsync();
        var problemDetails = JsonDocument.Parse(details);
        var errors = new List<string>();
        var errorList = problemDetails.RootElement.GetProperty("errors");

        foreach (var errorEntry in errorList.EnumerateObject())
        {
            if (errorEntry.Value.ValueKind == JsonValueKind.String)
            {
                errors.Add(errorEntry.Value.GetString()!);
            }
            else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
            {
                errors.AddRange(
                    errorEntry.Value.EnumerateArray().Select(
                        e => e.GetString() ?? string.Empty)
                    .Where(e => !string.IsNullOrEmpty(e)));
            }
        }
        return new FormResult
        {
            Succeeded = false,
            ErrorList = problemDetails == null ? defaultDetail : [.. errors]
        };
    }
    catch (Exception)
    {
        return new FormResult
        {
            Succeeded = false,
            ErrorList = defaultDetail
        };
    }
  }

  public async Task<FormResult> LoginAsync(string email, string password)
  {
    try
    {
        var result = await _httpClient.PostAsJsonAsync(
            "login", new
            {
                email,
                password
            });

        if (result.IsSuccessStatusCode)
        {
            var tokenResponse = await result.Content.ReadAsStringAsync();
            var tokenInfo = JsonSerializer.Deserialize<TokenInfo>(tokenResponse, jsonSerializerOptions);
            if (tokenInfo?.AccessToken != null)
            {
                await _localStorageService.SetItemAsync("accessToken", tokenInfo.AccessToken);

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return new FormResult { Succeeded = true };
            }
        }
    }
    catch (Exception)
    {
        //Log service error
    }

    return new FormResult
    {
        Succeeded = false,
        ErrorList = ["Invalid email and/or password."]
    };
  }

  public async Task LogoutAsync()
  {
    const string Empty = "{}";
    var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");

    var result = await _httpClient.PostAsync("logout", emptyContent);
    if (result.IsSuccessStatusCode)
    {
      await _localStorageService.RemoveItemAsync("accessToken");

      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
  }

  public async Task<bool> CheckAuthenticatedAsync()
  {
    await GetAuthenticationStateAsync();
    return _authenticated;
  }
}