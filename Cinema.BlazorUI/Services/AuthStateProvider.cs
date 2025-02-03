using Cinema.BlazorUI.Services.Interfaces;
using Cinema.BlazorUI.Model;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Cinema.BlazorUI.Services;

namespace Cinema.BlazorUI.Services;
public class AuthStateProvider : AuthenticationStateProvider, IAccountManagement
{

    private readonly UserInfo _testUser = new()
    {
        Email = "test@example.com",
        Claims = new Dictionary<string, string>
        {
            { "Name", "Test User" },
            { "Phone", "1234567890" },
            { "Role", "Admin"},
            { "Email", "test@example.com" }
        }
    };

    private readonly string[] _testRoles = new[] { "Admin", "User" };

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
        /*
        
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Email, _testUser.Email),
                };

                // Add custom claims
                foreach (var claim in _testUser.Claims)
                {
                    claims.Add(new Claim(claim.Key, claim.Value));
                }

                // Add test roles
                foreach (var role in _testRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var id = new ClaimsIdentity(claims, nameof(AuthStateProvider));
                var user = new ClaimsPrincipal(id);
                _authenticated = true;

                return new AuthenticationState(user);
        */

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
                   new(ClaimTypes.Name, userInfo.Name),
                   new(ClaimTypes.Email, userInfo.Email),
                   new(ClaimTypes.Role, userInfo.Claims["Role"]),
                   new("Phone", userInfo.PhoneNumber)
               };

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

    public UserInfo GetUserInfo()
    {
        var state = GetAuthenticationStateAsync().Result;
        var user = state.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            return new UserInfo();
        }

        var userInfo = new UserInfo
        {
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
            Name = user.FindFirst("Name")?.Value ?? string.Empty,
            PhoneNumber = user.FindFirst("Phone")?.Value ?? string.Empty,
            Claims = new Dictionary<string, string>()
        };

        foreach (var claim in user.Claims)
        {
            if (claim.Type != ClaimTypes.Email)
            {
                userInfo.Claims[claim.Type] = claim.Value;
            }
        }

        return userInfo;
    }
}