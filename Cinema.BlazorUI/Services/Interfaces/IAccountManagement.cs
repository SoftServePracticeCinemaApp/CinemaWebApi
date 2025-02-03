using Cinema.BlazorUI.Model;

namespace Cinema.BlazorUI.Services.Interfaces;

public interface IAccountManagement
{
  Task<FormResult> RegisterAsync(string email, string password, string phone, string name, string lastName);
  Task<FormResult> LoginAsync(string email, string password);
  Task LogoutAsync();
  Task<bool> CheckAuthenticatedAsync();
  UserInfo GetUserInfo();
}
